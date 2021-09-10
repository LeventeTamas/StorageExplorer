using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;
using System.IO;

namespace StorageExplorer.Controller
{
    public class Controller : ApplicationContext
    {
        private View.MainWindow mainWindow;
        private DispatcherTimer driveListUpdateTimer;

        // fájl másoló ablakok, és háttérfolyamatok
        private List<View.ProgressWindow> progressWindowList;
        private List<Model.FileMovingProcess> fileMovingProcessList;

        public Controller()
        {
            // Beállítások és nyelvek betöltése
            Model.Settings.LoadSettings();
            Model.Language.Load();
            

            // Fő ablak létrehozása
            mainWindow = new View.MainWindow(this);
            SetLanguage(Model.Settings.UserSettings.Get("language"));

            // munkamenetek betöltése
            Model.Session.LoadSessionList();
            SetSessionNameList();

            // fő ablak megjelenítése
            mainWindow.Show();

            // fájl másoló szálak és ablakok listája
            progressWindowList = new List<View.ProgressWindow>();
            fileMovingProcessList = new List<Model.FileMovingProcess>();

            // Kijelölt munkamenet betöltése
            SelectSession(Model.Settings.AppSettings.GetInt64("selected_session"));

            // Meghajtó lista frissítése időzítővel
            driveListUpdateTimer = new DispatcherTimer();
            driveListUpdateTimer.Interval = TimeSpan.FromMilliseconds(1000);
            driveListUpdateTimer.Tick += DriveRefreshTimer_Tick;
            driveListUpdateTimer.Start();
        }

        private void SetLanguage(string _id)
        {
            if (_id != Model.Language.GetSelectedLanguageID())
            {

                Model.Settings.UserSettings.Set("language", _id);

            }
            Model.Language.SelectLanguage(_id);
            mainWindow.UpdateLanguage();
        }

        #region Fájlkezelő ablakok
        public void NewWindow()
        {
            Model.Structures.ExplorerWindowModel modelEWM = Model.Session.GetSelectedSession().AddWindow();
            View.Structures.ExplorerWindowModel viewEWM = GetViewExplorerWindowModel(modelEWM);
            mainWindow.AddWindow(viewEWM);
            ResetLayout();
            UpdateDriveList();
            // A navigálás megtörténik az ablak létrehozásakor, mert fókuszba kerül, és ez meghívja az 'ExplorerWindow_Enter' eljárást
        }
        private void Navigate(int _windowID, string _newPath, bool _resetScrollPosition)
        {
            if (Model.IOManager.IsPathValid(_newPath))
            {
                Model.Structures.ExplorerWindowModel windowModel = Model.Session.GetSelectedSession().GetWindowModel(_windowID);
                // mappa tartalmának elkérése a modelltől
                Model.Structures.FolderContent folderContent = Model.IOManager.GetFolderContent(_newPath, Model.Settings.UserSettings.GetBoolean("show_hidden_files"), windowModel.FileListOrder);

                // lista elemek létrehozása
                Model.Structures.FolderContentItem[] folderContentItemList = folderContent.Items;
                ListViewItem[] listViewItemList = new ListViewItem[folderContentItemList.Length];
                for (int i = 0; i < folderContentItemList.Length; i++)
                {
                    listViewItemList[i] = new ListViewItem(new string[] { folderContentItemList[i].Name, folderContentItemList[i].Extension, folderContentItemList[i].Size + "", folderContentItemList[i].CreationTime.ToString("yyyy.MM.dd HH:mm:ss") }, folderContentItemList[i].IconID);

                    // elem típusa (fájl, mappa, vagy visszalépés gomb)
                    switch (folderContentItemList[i].ItemType)
                    {
                        case Model.Structures.EFolderContentItemType.File: { listViewItemList[i].Tag = View.Structures.EFolderContentItemType.File; break; }
                        case Model.Structures.EFolderContentItemType.Folder: { listViewItemList[i].Tag = View.Structures.EFolderContentItemType.Folder; break; }
                        case Model.Structures.EFolderContentItemType.BackButton: { listViewItemList[i].Tag = View.Structures.EFolderContentItemType.BackButton; break; }
                    }

                }
                // fájlkezelő ablak frissítése
                // lehet hogy másik szálról hívjuk meg (pl törlés vagy másolás után)
                mainWindow.Invoke((MethodInvoker)delegate
                {
                    mainWindow.SetExplorerWindowPath(_windowID, _newPath, listViewItemList, folderContent.IconList, _resetScrollPosition);
                });

                Model.Session.GetSelectedSession().GetWindowModel(_windowID).Path = _newPath;
            }
            else
            {
                Navigate(_windowID, Model.IOManager.GetValidPath(), false);
            }
        }

        public void OpenFileOrDirectory(int _windowID, string _itemPath)
        {
            try
            {
                if (File.GetAttributes(_itemPath).HasFlag(FileAttributes.Directory))
                {
                    Navigate(_windowID, _itemPath, false);
                }
                else
                {
                    Model.IOManager.OpenFile(_itemPath);
                }
            }
            catch (Win32Exception ex)
            {
                ShowErrorMessage(3010105, GetTranslation("error_file_not_associated"), ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                ShowErrorMessage(3010106, GetTranslation("error_file_not_found"), ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                ShowErrorMessage(3010103, GetTranslation("error_directory_not_found"), ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                ShowErrorMessage(3010107, GetTranslation("error_unauthorized_access"), ex.Message);
            }
            catch (IOException ex) when ((ex.HResult & 0xffff) == 21)
            {
                ShowErrorMessage(3010104, GetTranslation("error_device_not_ready"), ex.Message);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(3010101, GetTranslation("error_read_write_error"), ex.Message);
            }
        }
        public void DragDropCopy(int _windowID, string[] _pathList)
        {
            DataObject dataObject = new DataObject(DataFormats.FileDrop, _pathList);
            mainWindow.StartDragDrop(_windowID, dataObject, DragDropEffects.Copy);
        }
        public void DragDropPaste(int _windowID, string[] _pathList)
        {
            string currPath = Model.Session.GetSelectedSession().GetWindowModel(_windowID).Path;
            CopyFiles(_windowID, _pathList, currPath);
        }

        public void CopyToClipboard(string[] _pathList, bool _cut = false)
        {

            // beállítjuk hogy másolás vagy kivágás műveletet hajtunk végre
            DragDropEffects effect = _cut ? DragDropEffects.Move : DragDropEffects.Copy;

            //StringCollection típusra van szükség, így át kell alakítani a string tömböt
            StringCollection itemCollection = new System.Collections.Specialized.StringCollection();
            foreach (string path in _pathList)
            {
                itemCollection.Add(path);
            }

            // létrehozzuk a DataObject objektumot, amit majd átadunk a vágólapnak. Ez tartalmazza a fájl listát, és a műveletet
            DataObject data = new DataObject();
            data.SetFileDropList(itemCollection);
            data.SetData("Preferred DropEffect", new MemoryStream(BitConverter.GetBytes((int)effect)));

            // vágólapra helyezés
            Clipboard.Clear();
            Clipboard.SetDataObject(data);
        }

        public void PasteFromClipboard(int _windowID, string _destinationDir)
        {
            // a StringCollectionból egy string tömböt készítünk
            StringCollection itemCollection = Clipboard.GetFileDropList();
            string[] itemList = new string[itemCollection.Count];
            for (int i = 0; i < itemCollection.Count; i++)
            {
                itemList[i] = itemCollection[i];
            }

            // meghatározzuk, hogy másolás vagy kivágás műveletet kell végrehajtani
            // a vágólapon MemoryStream formájában van tárolva a DragDropEffect enumerátor, így azt vissza kell alakítani
            MemoryStream msEffect = (MemoryStream)Clipboard.GetData("Preferred DropEffect");
            byte[] baEffect = msEffect.ToArray();
            DragDropEffects effect = (DragDropEffects)BitConverter.ToInt32(baEffect, 0);

            // átmásoljuk/áthelyezzük a fájlokat
            if (effect == DragDropEffects.Move)
            {
                MoveFiles(_windowID, itemList, _destinationDir);
            }
            else
            {
                CopyFiles(_windowID, itemList, _destinationDir);
            }
        }

        private void MoveFiles(int _windowID, string[] _itemList, string _destinationPath)
        {
            int index = CreateFileMovingProcess(_windowID, _itemList, _destinationPath, GetTranslation("moving")+"..");
            fileMovingProcessList[index].StartMove();
            progressWindowList[index].Show(mainWindow);
        }

        private void CopyFiles(int _windowID, string[] _itemList, string _destinationPath)
        {
            int index = CreateFileMovingProcess(_windowID, _itemList, _destinationPath, GetTranslation("copying")+"..");
            fileMovingProcessList[index].StartCopy();
            progressWindowList[index].Show(mainWindow);
        }
        
        public void DeleteFiles(int _windowID, string[] _itemPathList)
        {
            // Törlési szándék megerősítése
            if (MessageBox.Show(GetTranslation("delete_files_confirm"), GetTranslation("delete_files_confirm_title"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int index = CreateFileMovingProcess(_windowID, _itemPathList, null, GetTranslation("deleting")+"..");
                fileMovingProcessList[index].StartDelete();
                progressWindowList[index].Show(mainWindow);
            }
        }

        // Hibakód: 30103xx
        public void CreateNewFolder(int _windowID, string _path)
        {
            View.InputBox inputBox = new View.InputBox(this, GetTranslation("new_folder"), GetTranslation("new_folder_name")+":", View.Structures.EItemNameTestType.Path);
            if (inputBox.ShowDialog(mainWindow) == DialogResult.OK)
            {
                string name = inputBox.GetInputBoxValue();
                string path = Path.Combine(_path, name);
                try
                {
                    Model.IOManager.CreateFolder(path);
                }
                catch (IOException ex) when ((ex.HResult & 0xffff) == 80)
                {
                    ShowErrorMessage(3010304, GetTranslation("error_folder_already_exists"), ex.Message);
                }
                catch (IOException ex)
                {
                    ShowErrorMessage(3010304, GetTranslation("error_read_write_error"), ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    ShowErrorMessage(3010307, GetTranslation("error_unauthorized_access"), ex.Message);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(3010301, GetTranslation("error_unknown_error"), ex.Message);
                }
                RefreshWindow(_windowID);
            }
        }

        // Hibakód: 30102xx
        public void CreateNewFile(int _windowID, string _path)
        {

            View.InputBox inputBox = new View.InputBox(this, GetTranslation("new_file"), GetTranslation("new_file_name")+":", View.Structures.EItemNameTestType.Filename);
            if (inputBox.ShowDialog() == DialogResult.OK)
            {
                string name = inputBox.GetInputBoxValue();
                string filePath = Path.Combine(_path, name);
                try
                {
                    Model.IOManager.CreateFile(filePath);
                }
                catch (UnauthorizedAccessException ex)
                {
                    ShowErrorMessage(3010207, GetTranslation("error_unauthorized_access"), ex.Message);
                }
                catch (IOException ex) when ((ex.HResult & 0xffff) == 80)
                {
                    ShowErrorMessage(3010204, GetTranslation("error_file_already_exists"), ex.Message);
                }
                catch (IOException ex)
                {
                    ShowErrorMessage(3010204, GetTranslation("error_read_write_error"), ex.Message);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(3010201, GetTranslation("error_unknown_error"), ex.Message);
                }
                RefreshWindow(_windowID);
            }
        }

        // Hibakód: 30104xx
        public void RenameItem(int _windowID, string _itemPath)
        {
            string name = Model.IOManager.GetItemName(_itemPath);
            bool isFolder = Model.IOManager.IsFolder(_itemPath);
            View.InputBox inputBox;

            if (isFolder) 
            {
                inputBox = new View.InputBox(this, GetTranslation("rename_folder"), GetTranslation("folder_new_name") + ":", View.Structures.EItemNameTestType.Path, name);
            }
            else
            {
                inputBox = new View.InputBox(this, GetTranslation("rename_file"), GetTranslation("file_new_name") + ":", View.Structures.EItemNameTestType.Filename, name);
            }

            if(inputBox.ShowDialog(mainWindow) == DialogResult.OK)
            {
                name = inputBox.GetInputBoxValue();
                try
                {
                    Model.IOManager.Rename(name, _itemPath);
                }
                catch (UnauthorizedAccessException ex)
                {
                    ShowErrorMessage(3010407, GetTranslation("error_unauthorized_access"), ex.Message);
                }
                catch (IOException ex)
                {
                    ShowErrorMessage(3010404, GetTranslation("error_read_write_error"), ex.Message);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(3010401, GetTranslation("error_unknown_error"), ex.Message);
                }

                RefreshWindow(_windowID);
            }
        }

        public void RefreshWindow(int _windowID)
        {
            Model.Structures.ExplorerWindowModel windowModel = Model.Session.GetSelectedSession().GetWindowModel(_windowID);
            Navigate(_windowID, windowModel.Path, true);
        }
        public void CloseWindow(int _windowID)
        {
            Model.Session.GetSelectedSession().CloseWindow(_windowID);
            ResetLayout();
        }
        public void ShowItemProperties(string[] _itemPathList)
        {
            if (_itemPathList.Length == 1)
            {
                Model.IOManager.ShowItemProperties(_itemPathList[0]);
            }
            else
            {
                ulong totalSize = 0;
                int numOfFiles = 0;
                int numOfFolders = 0;
                Model.IOManager.GetItemListProperties(_itemPathList, out numOfFiles, out numOfFolders, out totalSize);
                new View.PropertiesWindow(this, numOfFiles, numOfFolders, totalSize).Show(mainWindow);
            }
        }
        public void ShowExplorerWindowSettings(int _windowID)
        {
            Model.Structures.ExplorerWindowModel modelWindowModel = Model.Session.GetSelectedSession().GetWindowModel(_windowID);
            View.Structures.ExplorerWindowModel viewWindowModel = GetViewExplorerWindowModel(modelWindowModel);
            View.ExplorerWindowSettings settingsWindow = new View.ExplorerWindowSettings(this, viewWindowModel);
            if (settingsWindow.ShowDialog(mainWindow) == DialogResult.OK)
            {
                modelWindowModel.BackgroundColor = new Model.Structures.Color(viewWindowModel.BackgroundColor);
                modelWindowModel.FileListFont = new Model.Structures.Font(viewWindowModel.FileListFont, viewWindowModel.FileListFontColor);
                mainWindow.SetExplorerWindowModel(_windowID, viewWindowModel);
            }
        }

        public void ChangeFileListOrder(int _windowID, string _columnTag)
        {
            Model.Structures.ExplorerWindowModel windowModel = Model.Session.GetSelectedSession().GetWindowModel(_windowID);
            Model.Structures.EFileListOrder order = windowModel.FileListOrder;
            switch (_columnTag)
            {
                case "col_name":
                    {
                        order = order == Model.Structures.EFileListOrder.NAME_ASC ? Model.Structures.EFileListOrder.NAME_DESC : Model.Structures.EFileListOrder.NAME_ASC;
                        break;
                    }
                case "col_extension":
                    {
                        order = order == Model.Structures.EFileListOrder.EXTENSION_ASC ? Model.Structures.EFileListOrder.EXTENSION_DESC : Model.Structures.EFileListOrder.EXTENSION_ASC;
                        break;
                    }
                case "col_size":
                    {
                        order = order == Model.Structures.EFileListOrder.SIZE_ASC ? Model.Structures.EFileListOrder.SIZE_DESC : Model.Structures.EFileListOrder.SIZE_ASC;
                        break;
                    }
                case "col_date":
                    {
                        order = order == Model.Structures.EFileListOrder.DATE_ASC ? Model.Structures.EFileListOrder.DATE_DESC : Model.Structures.EFileListOrder.DATE_ASC;
                        break;
                    }
            }
            windowModel.FileListOrder = order;
            RefreshWindow(_windowID);
        }
        #endregion

        #region Főablak
        public void ResetLayout()
        {
            View.Structures.ESessionLayout layout = (View.Structures.ESessionLayout)Model.Session.GetSelectedSession().GetLayout();
            mainWindow.SetLayout(layout);
        }
        public void SetLayout(View.Structures.ESessionLayout _layout)
        {
            Model.Session session = Model.Session.GetSelectedSession();
            Model.Structures.ESessionLayout modelSessionLayout = (Model.Structures.ESessionLayout)_layout;
            if (session.GetLayout() != modelSessionLayout)
            {
                mainWindow.SetLayout(_layout);
                session.SetLayout(modelSessionLayout);
            }
        }
        public void ShowSettings()
        {
            // nyelv lista
            string[] languages = Model.Language.GetLanguageList();
            View.Structures.ComboBoxLanguageItem[] languageList = new View.Structures.ComboBoxLanguageItem[languages.Length];
            for (int i = 0; i < languages.Length; i++)
            {
                string[] languageInfo = languages[i].Split(';');
                languageList[i] = new View.Structures.ComboBoxLanguageItem(languageInfo[0], languageInfo[1]);
            }

            // beálítás lista
            Dictionary<string, string> settingList = Model.Settings.UserSettings.GetSettingList();

            // Beállítások ablak megjelenítése
            View.MainWindowSettings settingsWindow = new View.MainWindowSettings(this, settingList, languageList);
            if (settingsWindow.ShowDialog(mainWindow) == DialogResult.OK)
            {
                Dictionary<string, string> newSettingList = settingsWindow.GetSettings();
                Model.Settings.UserSettings.SetMultiple(newSettingList);
                List<Model.Structures.ExplorerWindowModel> windowModelList = Model.Session.GetSelectedSession().GetExplorerWindowModelList();
                foreach (Model.Structures.ExplorerWindowModel windowModel in windowModelList)
                {
                    RefreshWindow(windowModel.ID);
                }
                SetLanguage(newSettingList["language"]);
            }
        }

        private void SetSessionNameList()
        {
            List<string> sessionNameListStr = Model.Session.GetSessionNameList();
            List<View.Structures.ComboBoxSessionItem> sessionNameList = new List<View.Structures.ComboBoxSessionItem>();
            foreach (string session in sessionNameListStr)
            {
                string[] adatok = session.Split(';');
                sessionNameList.Add(new View.Structures.ComboBoxSessionItem(Convert.ToInt64(adatok[0]), adatok[1]));
            }
            mainWindow.SetSessionNameList(sessionNameList);
        }
        #endregion

        #region Munkamenetek
        public void SaveSession()
        {
            Model.Session session = Model.Session.GetSelectedSession();
            if (Model.Session.GetDefaultSessionID() == session.GetID())
            {
                View.InputBox inputBox = new View.InputBox(this, Model.Language.GetTranslation("session_name"), Model.Language.GetTranslation("session_name") + ":", View.Structures.EItemNameTestType.None);
                if (inputBox.ShowDialog(mainWindow) == DialogResult.OK)
                {
                    // új munkamenet létrehozása
                    string name = inputBox.GetInputBoxValue();
                    Model.Session newSession = Model.Session.SaveDefaultSession(name);
                    
                    // alapértelmezett munkamenet alaphelyzetbe állítása
                    mainWindow.ClearSessionExpWindows();
                    Model.Session.ResetDefaultSession();
                    mainWindow.SetDefaultSessionID(Model.Session.GetDefaultSessionID());

                    // új munkamenet hozzáadása a nézet oldalhoz
                    mainWindow.AddSessionName(new View.Structures.ComboBoxSessionItem(newSession.GetID(), newSession.GetName())); 
                    // új munkamenet kiválasztása
                    SelectSession(newSession.GetID());
                }
            }
            else
            {
                session.Save();
            }
        }
        public void SaveAll()
        {
            Model.Session.SaveAll();
        }
        public void SelectSession(long _sessionID)
        {
            Model.Session session = Model.Session.GetSelectedSession();
            if (session.GetID() != _sessionID)
            {
                // fájlkezelő ablakok törlése a nézetben
                mainWindow.ClearSessionExpWindows();

                // a kiválasztott munkamenet beállítása
                Model.Session.SelectSession(_sessionID);
                session = Model.Session.GetSelectedSession();
                Model.Settings.AppSettings.Set("selected_session", session.GetID().ToString());

                // Ha az "új munkamenet" lett kiválasztva, akkor törölni kell belőle az összes ablakot, és létrehozni egy újat
                if (Model.Session.IsSelectedDefaultSession())
                {
                    List<Model.Structures.ExplorerWindowModel> windowModelList = session.GetExplorerWindowModelList();
                    while (windowModelList.Count > 0)
                    {
                        session.CloseWindow(windowModelList[0].ID);
                    }
                    session.AddWindow();
                    session.AddWindow();
                    session.SetLayout(Model.Structures.ESessionLayout.Vertical);
                }

                // fájlkezelő ablak modell lista konvertálása 
                List<View.Structures.ExplorerWindowModel> viewExplorerWindowModelList = new List<View.Structures.ExplorerWindowModel>();
                List<Model.Structures.ExplorerWindowModel> modelExplorerWindowModelList = session.GetExplorerWindowModelList();
                foreach (Model.Structures.ExplorerWindowModel modelEWM in modelExplorerWindowModelList)
                {
                    View.Structures.ExplorerWindowModel viewEWM = GetViewExplorerWindowModel(modelEWM);
                    viewExplorerWindowModelList.Add(viewEWM);
                }

                View.Structures.ESessionLayout layout = (View.Structures.ESessionLayout)session.GetLayout();

                // fájlkezelő ablakok létrehozása
                mainWindow.CreateSessionExpWindows(layout, viewExplorerWindowModelList);

                // meghajtó lista frissítése
                UpdateDriveList();
                // főablak frissítése a kijelölt munkamenet alapján
                mainWindow.SetSelectedSessionComboBox(session.GetID());
                mainWindow.SetSessionDeleteButtonEnabled(!Model.Session.IsSelectedDefaultSession());

                
                mainWindow.SetLayout(layout);
            }
        }
        public void DeleteSession(View.Structures.ComboBoxSessionItem _cbsItem)
        {
            if (MessageBox.Show(GetTranslation("delete_session_confirm_text"), GetTranslation("delete_session"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) 
            {
                SelectSession(0);
                Model.Session.DeleteSession(_cbsItem.ID);
                mainWindow.DeleteSession(_cbsItem);
            }
        }
        public void RenameSession()
        {
            string name = Model.Session.GetSelectedSession().GetName();
            View.InputBox inputBox = new View.InputBox(this, GetTranslation("rename_session_title"), GetTranslation("new_session_name") + ":", View.Structures.EItemNameTestType.None, name);
            if (inputBox.ShowDialog(mainWindow) == DialogResult.OK)
            {
                name = inputBox.GetInputBoxValue();
                Model.Session selectedSession = Model.Session.GetSelectedSession();
                selectedSession.SetName(name);
                mainWindow.SetSessionName(name);
            }
        }
        #endregion

        #region Meghajtó lista frissítése
        private void DriveRefreshTimer_Tick(object sender, EventArgs e)
        {
            UpdateDriveList();
        }
        private void UpdateDriveList()
        {
            Model.Structures.DriveItem[] driveItemList = Model.IOManager.GetDriveList();
            ToolStripButton[] driveButtonList = new ToolStripButton[driveItemList.Length];
            for (int i = 0; i < driveItemList.Length; i++)
            {
                driveButtonList[i] = new ToolStripButton
                {
                    Text = driveItemList[i].Name,
                    Image = driveItemList[i].Icon,
                    DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                    AutoSize = true
                };
            }
            mainWindow.SetDriveList(driveButtonList);
        }
        #endregion

        #region View --> Model
        public void SetWindowLocation(int _windowID, System.Drawing.Point _newLocation)
        {
            Model.Structures.Location location = new Model.Structures.Location(_newLocation.X, _newLocation.Y);
            Model.Session.GetSelectedSession().GetWindowModel(_windowID).WindowLocation = location;
        }
        public void SetWindowSize(int _windowID, System.Drawing.Size _newSize)
        {
            Model.Structures.Size size = new Model.Structures.Size(_newSize.Width, _newSize.Height);
            Model.Session.GetSelectedSession().GetWindowModel(_windowID).WindowSize = size;
        }
        public void SetIsMaximized(int _windowID, bool _isMaximized)
        {
            Model.Session.GetSelectedSession().GetWindowModel(_windowID).IsMaximized = _isMaximized;
        }
        #endregion

        #region Model --> View
        public string GetTranslation(string _key)
        {
            return Model.Language.GetTranslation(_key);
        }
        public bool GetUserSettingBool(string _key)
        {
            return Model.Settings.UserSettings.GetBoolean(_key);
        }
        #endregion

        #region segéd metódusok
        private View.Structures.ExplorerWindowModel GetViewExplorerWindowModel(Model.Structures.ExplorerWindowModel _modelEWM)
        {
            int id = _modelEWM.ID;
            string path = _modelEWM.Path;
            System.Drawing.Point location = new System.Drawing.Point(_modelEWM.WindowLocation.X, _modelEWM.WindowLocation.Y);
            System.Drawing.Size size = new System.Drawing.Size(_modelEWM.WindowSize.Width, _modelEWM.WindowSize.Height);
            System.Drawing.Font font = new System.Drawing.Font(_modelEWM.FileListFont.FontFamily, _modelEWM.FileListFont.Size);
            System.Drawing.Color fontColor = System.Drawing.Color.FromArgb(_modelEWM.FileListFont.FontColor.Red, _modelEWM.FileListFont.FontColor.Green, _modelEWM.FileListFont.FontColor.Blue);
            System.Drawing.Color backgroundColor = System.Drawing.Color.FromArgb(_modelEWM.BackgroundColor.Red, _modelEWM.BackgroundColor.Green, _modelEWM.BackgroundColor.Blue);
            bool isMaximized = _modelEWM.IsMaximized;
            return new View.Structures.ExplorerWindowModel(id, path, location, size, isMaximized, font, fontColor, backgroundColor);
        }
        #endregion

        #region Fájl mozgatás

        private int CreateFileMovingProcess(int _windowID, string[] _itemPathList, string _destinationDir, string _title)
        {
            int index = progressWindowList.Count;

            View.ProgressWindow progressWindow = new View.ProgressWindow(this, index, _title);
            progressWindow.FormClosing += ProgressWindow_FormClosing;
            progressWindowList.Add(progressWindow);

            Model.FileMovingProcess process = new Model.FileMovingProcess(index, _windowID, _itemPathList, _destinationDir, this);
            process.OnFileMovingProgressChanged += FileMovingProgressChanged;
            process.OnFileMovingEnded += FileMovingEnded;
            process.OnFileMovingError += FileMovingError;
            fileMovingProcessList.Add(process);
            return fileMovingProcessList.Count-1;
        }

        private void FileMovingProgressChanged(Model.Structures.FileMovingProgressEventArgs e)
        {
            if (progressWindowList[e.Index] != null)
            {
                progressWindowList[e.Index].SetProgress(e.Destination, e.CurrentItem, e.NumberOfMovedItems, e.NumberOfTotalItems, e.CurrentItemProgress, e.TotalProgress);
            }
        }

        private void ProgressWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            int index = ((View.ProgressWindow)sender).Index;
            if (progressWindowList[index].Tag == null)
            {
                e.Cancel = true;
                fileMovingProcessList[index].Abort();
            }
        }

        private void FileMovingEnded(int _index, int _windowID)
        {
            if (progressWindowList[_index] != null)
            {
                progressWindowList[_index].Invoke((MethodInvoker)delegate
                {
                    
                    progressWindowList[_index].Hide();

                    // ezt azért kell meghívni, mert csak a megszakítottnak jelölt folyamatok számítanak befejezettnek
                    fileMovingProcessList[_index].Abort();

                    // megnézzük, hogy minden folyamat véget ért-e, és ha igen , akkor töröljük a listák tartalmát
                    int i = 0;
                    while (i < fileMovingProcessList.Count && fileMovingProcessList[i].IsAborted) i++;
                    if (i == fileMovingProcessList.Count)
                    {
                        // fájlmozgatás folyamat jelző ablakok bezárása 
                        for (int j = 0; j < progressWindowList.Count; j++)
                        {
                            progressWindowList[j].Tag = "closedProgrammatically";
                            progressWindowList[j].Close();
                        }
                        // lista elemeinek törlése
                        while (fileMovingProcessList.Count > 0) 
                        {
                            progressWindowList.RemoveAt(0);
                            fileMovingProcessList.RemoveAt(0);
                        }
                    }
                });

                RefreshWindow(_windowID);
            }
        }
        private void FileMovingError(int _index, int _errorCode, string _errorMessage, Exception ex)
        {
            ShowErrorMessage(_errorCode, _errorMessage, ex.Message, progressWindowList[_index]);
        }
        #endregion

        #region Hibaüzenetek
        public void ShowErrorMessage(int _errorCode, string _message, string _exceptionMessage = "", Form _owner = null)
        {
            string errorStr = GetTranslation("error");
            LogError(_errorCode, _message + ";" + _exceptionMessage);

            if(_owner == null)
            {
                _owner = mainWindow;
            }
           _owner.Invoke(new Action(() => { MessageBox.Show(_owner, _message + "\n\n" + _exceptionMessage, errorStr, MessageBoxButtons.OK, MessageBoxIcon.Error); }));
            
        }
        public void LogError(int _errorCode, string _message)
        {
           Log.Error(_errorCode, _message);
        }
        #endregion
    }
}