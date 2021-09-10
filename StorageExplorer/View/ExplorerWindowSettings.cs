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
    public partial class ExplorerWindowSettings : Form
    {
        private Structures.ExplorerWindowModel windowModel;
        private Controller.Controller controller;
        public ExplorerWindowSettings(Controller.Controller _controller, Structures.ExplorerWindowModel _windowModel)
        {
            InitializeComponent();

            this.windowModel = _windowModel;
            this.controller = _controller;

            LoadLanguage();

            foreach (FontFamily fontFamily in System.Drawing.FontFamily.Families)
            {
                int index = cbFontFamily.Items.Add(fontFamily.Name);
                cbFontFamily.AutoCompleteCustomSource.Add(fontFamily.Name);
            }

            cbFontFamily.SelectedItem = windowModel.FileListFont.FontFamily.Name;
            numFontSize.Value = (int)windowModel.FileListFont.Size;
            btnFontColor.BackColor = windowModel.FileListFontColor;
            btnBackColor.BackColor = windowModel.BackgroundColor;
        }

        private void LoadLanguage()
        {
            this.Text = controller.GetTranslation("window_settings");
            this.lbBackColor.Text = controller.GetTranslation("background_color") + ":";
            this.lbFontColor.Text = controller.GetTranslation("font_color") + ":";
            this.lbFontFamily.Text = controller.GetTranslation("font_family") + ":";
            this.lbFontSize.Text = controller.GetTranslation("font_size") + ":";
            this.btnSave.Text = controller.GetTranslation("save");
            this.btnCancel.Text = controller.GetTranslation("cancel");
        }

        private void ExplorerWindowSettings_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public Structures.ExplorerWindowModel GetWindowModel()
        {
            return this.windowModel;
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = btnFontColor.BackColor;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnFontColor.BackColor = colorDialog1.Color;
                btnFontColor.ForeColor = colorDialog1.Color;
                windowModel.FileListFontColor = colorDialog1.Color;
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = btnBackColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnBackColor.BackColor = colorDialog1.Color;
                btnBackColor.ForeColor = colorDialog1.Color;
                windowModel.BackgroundColor = colorDialog1.Color;
            }
        }

        private void cbFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            windowModel.FileListFont = new Font(
                cbFontFamily.SelectedItem.ToString(), 
                windowModel.FileListFont.Size
            );
        }

        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {
            windowModel.FileListFont = new Font(
                windowModel.FileListFont.FontFamily,
                (float)numFontSize.Value
            );
        }
    }
}
