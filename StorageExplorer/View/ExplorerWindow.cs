using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace StorageExplorer.View
{
    
    public partial class ExplorerWindow : Form
    {
        private int id;
        private string path;
        private Controller.Controller controller;
        private bool isFormClosing = false;
        private FormWindowState prevWindowState;

        public ExplorerWindow(Controller.Controller _controller, Structures.ExplorerWindowModel _windowModel)
        {
            InitializeComponent();

            this.controller = _controller;
            this.id = _windowModel.ID;
            this.path = _windowModel.Path;
            this.Size = _windowModel.WindowSize;
            this.Location = _windowModel.WindowLocation;
            this.listView1.Font = _windowModel.FileListFont;
            this.listView1.ForeColor = _windowModel.FileListFontColor;
            this.listView1.BackColor = _windowModel.BackgroundColor;
            this.prevWindowState = FormWindowState.Normal;

            if (_windowModel.IsMaximized) 
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        public void UpdateLanguage()
        {
            tslbLocation.Text = controller.GetTranslation("location") + ":";
            listView1.Columns[0].Text = controller.GetTranslation("name");
            listView1.Columns[1].Text = controller.GetTranslation("extension");
            listView1.Columns[2].Text = controller.GetTranslation("size");
            listView1.Columns[3].Text = controller.GetTranslation("date");
            tsmiNewFile.Text = controller.GetTranslation("new_file");
            tsmiNewFolder.Text = controller.GetTranslation("new_folder");
            tsmiCopy.Text = controller.GetTranslation("copy");
            tsmiCut.Text = controller.GetTranslation("cut");
            tsmiPaste.Text = controller.GetTranslation("paste");
            tsmiRename.Text = controller.GetTranslation("rename");
            tsmiDelete.Text = controller.GetTranslation("delete");
            tsmiProperties.Text = controller.GetTranslation("properties");
            tsbt_Navigate.ToolTipText = controller.GetTranslation("navigate");
            tsbt_Settings.ToolTipText = controller.GetTranslation("window_settings");

            ExplorerWindow_Resize(this, null);
        }

        public long GetID()
        {
            return this.id;
        }

        public void SetWindowModel(Structures.ExplorerWindowModel _windowModel)
        {
            this.listView1.Font = _windowModel.FileListFont;
            this.listView1.ForeColor = _windowModel.FileListFontColor;
            this.listView1.BackColor = _windowModel.BackgroundColor;
        }

        public void Navigate(string _path, ListViewItem[] _itemList, ImageList _iconList, bool _resetScrollPostition)
        {
            tstb_Path.Text = _path;
            this.path = _path;

            int scrollPosition = 0;
            if (listView1.TopItem != null)
            {
                scrollPosition = listView1.Items.IndexOf(listView1.TopItem);
            }

            if (this.listView1.SmallImageList != null) this.listView1.SmallImageList.Dispose();
            this.listView1.SmallImageList = _iconList;
            this.Text = (Path.GetFileName(_path) != "" ? Path.GetFileName(_path) : Path.GetPathRoot(_path));
            listView1.Items.Clear();
            listView1.Items.AddRange(_itemList);

            foreach (ToolStripButton button in toolStrip2.Items)
            {
                button.Checked = button.Text == Path.GetPathRoot(_path);
            }

            // görgetősáv visszaállítása az előző pozícióra, ha a _resetScrollPostition igaz
            if (_resetScrollPostition && listView1.Items.Count > 0 && scrollPosition >= 0)
            {
                listView1.TopItem = listView1.Items[scrollPosition];
            }
        }

        private void OpenFileOrDirectory()
        {
            try
            {
                string[] itemPathList = GetSelectedItemPathList();
                controller.OpenFileOrDirectory(this.id, itemPathList[0]);
            }
            catch(Exception ex)
            {
                controller.LogError(2010101, ex.Message);
            }
        }

        private void DeleteItems()
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.DeleteFiles(this.id, itemPathList);
        }

        private string[] GetSelectedItemPathList()
        {
            List<string> itemPathList = new List<string>();
            bool isBackButtonSelected = false;
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                if (((Structures.EFolderContentItemType)listView1.SelectedItems[i].Tag) != Structures.EFolderContentItemType.BackButton) 
                {
                    itemPathList.Add(Path.Combine(this.path, listView1.SelectedItems[i].SubItems[0].Text + listView1.SelectedItems[i].SubItems[1].Text));
                }
                else
                {
                    isBackButtonSelected = true;
                }
            }

            // ha nincs elem a listában, akkor vagy nincs kijelölve semmi, vagy a vissza gomb van kijelölve
            if(itemPathList.Count == 0)
            {
                if (isBackButtonSelected)
                {
                    itemPathList.Add(new DirectoryInfo(this.path).Parent.FullName);
                }
                else
                {
                    itemPathList.Add(this.path);
                }
            }
            return itemPathList.ToArray();
        }

        private void CopyToClipboard(bool _cut)
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.CopyToClipboard(itemPathList, _cut);
        }

        #region drives
        public void SetDriveButtonList(ToolStripButton[] _driveButtonList)
        {
            int i = 0;
            while (i < toolStrip2.Items.Count && !((ToolStripButton)toolStrip2.Items[i]).Checked) i++;
            string selectedDrive = i < toolStrip2.Items.Count ? toolStrip2.Items[i].Text : string.Empty;

            toolStrip2.Items.Clear();
            foreach (ToolStripButton button in _driveButtonList)
            {
                ToolStripButton tsb = new ToolStripButton
                {
                    Text = button.Text,
                    Image = button.Image,
                    CheckOnClick = false,
                    Checked = button.Text == selectedDrive
                };
                tsb.Click += DriveClick;
                toolStrip2.Items.Add(tsb);
            }
        }

        private void DriveClick(object sender, EventArgs e)
        {
            controller.OpenFileOrDirectory(this.id, ((ToolStripButton)sender).Text);
        }
        #endregion

        #region events
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] pathList = (string[])e.Data.GetData(DataFormats.FileDrop);
            controller.DragDropPaste(this.id, pathList);
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.DragDropCopy(this.id, itemPathList);
        }
        
        private void ExplorerWindow_Enter(object sender, EventArgs e)
        {
            if(!isFormClosing) controller.RefreshWindow(this.id);
        }

        private void ExplorerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;
            this.Hide();
            this.MdiParent = null;
        }

        private void tsbt_Settings_Click(object sender, EventArgs e)
        {
            controller.ShowExplorerWindowSettings(this.id);
        }

        private void ListView1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        OpenFileOrDirectory();
                        break;
                    }
            }
        }

        private void TsmiDelete_Click(object sender, EventArgs e)
        {
            DeleteItems();
        }

        private void tsmiTulajdonsagok_Click(object sender, EventArgs e)
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.ShowItemProperties(itemPathList);
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileOrDirectory();
        }

        private void ExplorerWindow_Resize(object sender, EventArgs e)
        {
            // listView oszlopainak átméretezése
            listView1.Columns[0].Width = listView1.Width - 360;
            tstb_Path.Width = toolStrip1.Width - tslbLocation.Width - 70;

            if (WindowState != prevWindowState)
            {
                prevWindowState = WindowState;
                controller.SetWindowSize(this.id, this.Size);
            }
        }

        private void ExplorerWindow_ResizeEnd(object sender, EventArgs e)
        {
            controller.SetWindowSize(this.id, this.Size);
            controller.ResetLayout();
        }

        private void ExplorerWindow_LocationChanged(object sender, EventArgs e)
        {
            controller.SetWindowLocation(this.id, this.Location);
        }

        private void ExplorerWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            // csak akkor kell törölni a modellből az ablakot, ha a felhasználó zárta be
            if (this.Tag == null)
            {
                controller.CloseWindow(this.id);
            }
        }

        private void tbPath_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                controller.OpenFileOrDirectory(this.id, tstb_Path.Text);
            }
        }

        private void tsbt_Navigate_Click(object sender, EventArgs e)
        {
            controller.OpenFileOrDirectory(this.id, tstb_Path.Text);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            string columnTag = listView1.Columns[e.Column].Tag.ToString();
            controller.ChangeFileListOrder(this.id, columnTag);
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            CopyToClipboard(false);
        }

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            CopyToClipboard(true);
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.PasteFromClipboard(this.id, itemPathList[0]);
        }

        private void tsmiNewFolder_Click(object sender, EventArgs e)
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.CreateNewFolder(this.id, itemPathList[0]);
        }

        private void newFile_Click(object sender, EventArgs e)
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.CreateNewFile(this.id, itemPathList[0]);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // akkor lehet új fájlt, vagy mappát létrehozni, ha nincs kijelölt elem, vagy ha egy darab mappa van kijelölve
            bool enableNewItem =
                listView1.SelectedItems.Count == 0 ||
                (
                    listView1.SelectedItems.Count == 1 &&
                    ((Structures.EFolderContentItemType)listView1.SelectedItems[0].Tag) == Structures.EFolderContentItemType.Folder
                );
            tsmiNewFile.Enabled = enableNewItem;
            tsmiNewFolder.Enabled = enableNewItem;

            tsmiCopy.Enabled = listView1.SelectedItems.Count > 0;
            tsmiCut.Enabled = listView1.SelectedItems.Count > 0;
            tsmiDelete.Enabled = listView1.SelectedItems.Count > 0;
            tsmiRename.Enabled = listView1.SelectedItems.Count == 1;
            tsmiPaste.Enabled = Clipboard.ContainsFileDropList();
        }

        private void tsmiRename_Click(object sender, EventArgs e)
        {
            string[] itemPathList = GetSelectedItemPathList();
            controller.RenameItem(this.id, itemPathList[0]);
        }

        // WindowState változás event
        // forrás: https://stackoverflow.com/questions/1295999/event-when-a-window-gets-maximized-un-maximized
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                // Check your window state here
                if (m.WParam == new IntPtr(0xF030)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    controller.SetIsMaximized(this.id, true);
                }
                else if (m.WParam == new IntPtr(0xF120)) // Restore event - SC_RESTORE
                {
                    controller.SetIsMaximized(this.id, false);
                }
            }
            base.WndProc(ref m);
        }

        #endregion  
    }
}
