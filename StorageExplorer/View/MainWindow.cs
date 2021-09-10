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

namespace StorageExplorer.View
{
    public partial class MainWindow : Form
    {
        private Controller.Controller controller;
        private FormWindowState lastWindowState = FormWindowState.Normal;

        public MainWindow(Controller.Controller _controller)
        {
            this.controller = _controller;

            InitializeComponent();
            this.Size = new Size((int)(Screen.PrimaryScreen.Bounds.Width * 0.8), (int)(Screen.PrimaryScreen.Bounds.Height * 0.8));
        }

        public void UpdateLanguage()
        {
            this.Text = controller.GetTranslation("MainWindow_title");
            tsmi_options.Text = controller.GetTranslation("options");
            tsmi_settings.Text = controller.GetTranslation("settings");
            tsmi_session.Text = controller.GetTranslation("session");
            tsmi_newSession.Text = controller.GetTranslation("new_session");
            tsmi_renameSession.Text = controller.GetTranslation("rename_session") + "..";
            tsmi_deleteSession.Text = controller.GetTranslation("delete_session") + "..";
            tsmi_saveSession.Text = controller.GetTranslation("save_session");
            tsmi_newWindow.Text = controller.GetTranslation("new_window");
            tsl_session.Text = controller.GetTranslation("session") + ":";
            tsl_layout.Text = controller.GetTranslation("layout") + ":";
            tsb_newWindow.Text = controller.GetTranslation("new_window");
            tsb_newWindow.ToolTipText = controller.GetTranslation("new_window");
            tsb_deleteSession.ToolTipText = controller.GetTranslation("delete_session") + "..";
            tsb_saveSession.ToolTipText = controller.GetTranslation("save_session") + "..";
            tsb_renameSession.ToolTipText = controller.GetTranslation("rename_session") + "..";
            tsb_resetLayout.ToolTipText = controller.GetTranslation("reset_layout");
            tscb_layout.Items[0] = controller.GetTranslation("manual");
            tscb_layout.Items[1] = controller.GetTranslation("vertical");
            tscb_layout.Items[2] = controller.GetTranslation("horizontal");

            if (tscb_session.Items.Count > 0 && tscb_session.Items[0] is Structures.ComboBoxSessionItem)
            {
                int selectedIndex = tscb_session.SelectedIndex;
                Structures.ComboBoxSessionItem item = (Structures.ComboBoxSessionItem)tscb_session.Items[0];
                item.Name = controller.GetTranslation("default_session_name");
                tscb_session.Items.RemoveAt(0);
                tscb_session.Items.Insert(0, item);
                tscb_session.SelectedIndex = selectedIndex;
            }

            foreach (ExplorerWindow window in MdiChildren)
            {
                window.UpdateLanguage();
            }
        }

        public void AddWindow(Structures.ExplorerWindowModel _windowModel)
        {
            ExplorerWindow explorerWindow = new ExplorerWindow(controller, _windowModel);
            explorerWindow.MdiParent = this;
            explorerWindow.Show();
            explorerWindow.UpdateLanguage();
        }

        public void SetLayout(Structures.ESessionLayout _layout)
        {
            if ((int)_layout != tscb_layout.SelectedIndex) tscb_layout.SelectedIndex = (int)_layout;
            switch (_layout)
            {
                case Structures.ESessionLayout.Manual:
                    {
                        this.LayoutMdi(MdiLayout.ArrangeIcons);
                        tsb_resetLayout.Enabled = false;
                        break;
                    }
                case Structures.ESessionLayout.Vertical:
                    {
                        if (this.MdiChildren.Length > 1)
                        {
                            Dictionary<int, int> indexPositionPair = new Dictionary<int, int>();
                            for (int i = 0; i < this.MdiChildren.Length; i++)
                            {
                                indexPositionPair.Add(i, this.MdiChildren[i].Location.X);
                            }

                            foreach (int index in (from pair in indexPositionPair orderby pair.Value descending select pair.Key))
                            {
                                this.MdiChildren[index].Focus();
                            }
                        }

                        this.LayoutMdi(MdiLayout.TileVertical);
                        tsb_resetLayout.Enabled = true;
                        break;
                    }
                case Structures.ESessionLayout.Horizontal:
                    {
                        if (this.MdiChildren.Length > 1)
                        {
                            Dictionary<int, int> indexPositionPair = new Dictionary<int, int>();
                            for (int i = 0; i < this.MdiChildren.Length; i++)
                            {
                                indexPositionPair.Add(i, this.MdiChildren[i].Location.Y);
                            }

                            foreach (int index in (from pair in indexPositionPair orderby pair.Value descending select pair.Key))
                            {
                                MdiChildren[index].Focus();
                            }
                        }
                        this.LayoutMdi(MdiLayout.TileHorizontal);
                        tsb_resetLayout.Enabled = true;
                        break;
                    }
            }
        }

        private ExplorerWindow GetExplorerWindowByID(int _windowID)
        {
            int i = 0;
            while (i < MdiChildren.Length && ((ExplorerWindow)MdiChildren[i]).GetID() != _windowID) i++;
            return (ExplorerWindow)MdiChildren[i];
        }

        #region Session

        public void SetSessionNameList(List<Structures.ComboBoxSessionItem> _list)
        {
            tscb_session.Items.Clear();
            foreach (Structures.ComboBoxSessionItem item in _list)
            {
                tscb_session.Items.Add(item);
            }
        }
        public void AddSessionName(Structures.ComboBoxSessionItem _comboBoxSessionItem)
        {
            tscb_session.SelectedItem = tscb_session.Items.Add(_comboBoxSessionItem);
        }
        public void SetDefaultSessionID(long _newID)
        {
            ((Structures.ComboBoxSessionItem)tscb_session.Items[0]).ID =_newID;
        }
        public void SetSelectedSessionComboBox(long _sessionID)
        {
            int i = 0;
            while (i < tscb_session.Items.Count && ((Structures.ComboBoxSessionItem)tscb_session.Items[i]).ID != _sessionID) i++;
            tscb_session.SelectedIndex = i;
        }
        public void CreateSessionExpWindows(Structures.ESessionLayout _layout, List<Structures.ExplorerWindowModel> _windowList)
        {
            foreach (Structures.ExplorerWindowModel window in _windowList)
            {
                AddWindow(window);
            }
        }
        public void ClearSessionExpWindows()
        {
            foreach (ExplorerWindow window in MdiChildren)
            {
                window.Tag = "closingByApplication";
                window.Close(); 
            }
        }
        public void SetSessionDeleteButtonEnabled(bool _isEnabled)
        {
            tsb_deleteSession.Enabled = _isEnabled;
        }
        public void DeleteSession(Structures.ComboBoxSessionItem _cbsItem)
        {
            tscb_session.Items.Remove(_cbsItem);
        }
        public void SetSessionName(string _name)
        {
            Structures.ComboBoxSessionItem item = ((Structures.ComboBoxSessionItem)tscb_session.SelectedItem);
            item.Name = _name;
            int index = tscb_session.SelectedIndex;
            tscb_session.Items.RemoveAt(index);
            tscb_session.Items.Insert(index, item);
            tscb_session.SelectedIndex = index;
        }

        #endregion

        #region Controller --> ExplorerWindow
        public void StartDragDrop(int _windowID, DataObject _dataObject, DragDropEffects _effect)
        {
            ExplorerWindow explorerWindow = GetExplorerWindowByID(_windowID);
            explorerWindow.DoDragDrop(_dataObject, _effect);
        }
        public void SetExplorerWindowPath(int _windowID, string _path, ListViewItem[] _listViewItemList, ImageList _iconList, bool _resetScrollPosition)
        {
            GetExplorerWindowByID(_windowID).Navigate(_path, _listViewItemList, _iconList, _resetScrollPosition);
        }
        public void SetExplorerWindowModel(int _windowID, Structures.ExplorerWindowModel _windowModel)
        {
            GetExplorerWindowByID(_windowID).SetWindowModel(_windowModel);
        }
        public void SetDriveList(ToolStripButton[] _driveButtonList)
        {
            foreach (ExplorerWindow window in MdiChildren)
            {
                window.SetDriveButtonList(_driveButtonList);
            }
        }
        #endregion

        #region events
        private void Tsb_newWindow_Click(object sender, EventArgs e)
        {
            controller.NewWindow();
        }
        private void Tsb_saveSession_Click(object sender, EventArgs e)
        {
            controller.SaveSession();
        }
        private void Tscb_session_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.SelectSession(((Structures.ComboBoxSessionItem)tscb_session.SelectedItem).ID);
        }

        private void Tsmi_renameSession_Click(object sender, EventArgs e)
        {
            controller.RenameSession();
        }
        private void Tscb_layout_SelectedIndexChanged(object sender, EventArgs e)
        {
            controller.SetLayout((Structures.ESessionLayout)tscb_layout.SelectedIndex);
        }
        private void tsb_ResetLayout_Click(object sender, EventArgs e)
        {
            controller.ResetLayout();
        }
        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            controller.ResetLayout();
        }
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (this.WindowState != lastWindowState && (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Normal))
            {
                lastWindowState = this.WindowState;
                controller.ResetLayout();
            }
        }
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void tsb_deleteSession_Click(object sender, EventArgs e)
        {
            controller.DeleteSession((Structures.ComboBoxSessionItem)tscb_session.SelectedItem);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (controller.GetUserSettingBool("autosave")) controller.SaveAll();
            ClearSessionExpWindows();
        }

        private void tsmi_settings_Click(object sender, EventArgs e)
        {
            controller.ShowSettings();
        }
        
        private void tsmi_newSession_Click(object sender, EventArgs e)
        {
            controller.SelectSession(0);
        }

        private void tsmi_saveSession_Click(object sender, EventArgs e)
        {
            controller.SaveSession();
        }

        private void tsmi_deleteSession_Click(object sender, EventArgs e)
        {
            controller.DeleteSession((Structures.ComboBoxSessionItem)tscb_session.SelectedItem);
        }

        private void tsmi_newWindow_Click(object sender, EventArgs e)
        {
            controller.NewWindow();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            controller.RenameSession();
        }
        #endregion
    }
}
