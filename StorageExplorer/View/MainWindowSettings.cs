using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageExplorer.View
{
    public partial class MainWindowSettings : Form
    {

        private Controller.Controller controller;
        private Dictionary<string, string> settingList;

        public MainWindowSettings(Controller.Controller _controller, Dictionary<string, string> _settingList, Structures.ComboBoxLanguageItem[] _languageList)
        {
            InitializeComponent();

            this.controller = _controller;

            LoadLanguage();

            cbLanguage.Items.AddRange(_languageList);

            settingList = _settingList;
            chbAutosave.Checked = settingList.ContainsKey("autosave") ? settingList["autosave"].ToLower() == "true" : false;
            chbHiddenFiles.Checked = settingList.ContainsKey("show_hidden_files") ? settingList["show_hidden_files"].ToLower() == "true" : false;

            string selectedLanguage = settingList.ContainsKey("language") ? settingList["language"] : "";
            int i = 0;
            while (i < cbLanguage.Items.Count  && ((Structures.ComboBoxLanguageItem)cbLanguage.Items[i]).ID != selectedLanguage) i++;
            if (i < cbLanguage.Items.Count) cbLanguage.SelectedIndex = i;
        }

        private void LoadLanguage()
        {
            this.gbGlobal.Text = controller.GetTranslation("global_settings");
            this.gbSessions.Text = controller.GetTranslation("session_settings");
            this.gbFileExplorer.Text = controller.GetTranslation("file_explorer_settings");
            this.chbAutosave.Text = controller.GetTranslation("enable_autosave");
            this.chbHiddenFiles.Text = controller.GetTranslation("show_hidden_files");
            this.lbLanguage.Text = controller.GetTranslation("language")+":";
            this.btOk.Text = controller.GetTranslation("save");
            this.btCancel.Text = controller.GetTranslation("cancel");
            this.Text = controller.GetTranslation("settings");
        }

        public Dictionary<string, string> GetSettings()
        {
            return settingList;
        }

        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            settingList["language"] = ((Structures.ComboBoxLanguageItem)cbLanguage.SelectedItem).ID;
        }

        private void ChbAutosave_CheckedChanged(object sender, EventArgs e)
        {
            settingList["autosave"] = chbAutosave.Checked.ToString();
        }

        private void ChbHiddenFiles_CheckedChanged(object sender, EventArgs e)
        {
            settingList["show_hidden_files"] = chbHiddenFiles.Checked.ToString();
        }
    }
}
