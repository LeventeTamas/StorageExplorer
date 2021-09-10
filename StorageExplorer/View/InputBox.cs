using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace StorageExplorer.View
{
    public partial class InputBox : Form
    {
        private Controller.Controller controller;
        private Structures.EItemNameTestType testType;

        public InputBox(Controller.Controller _controller, string _title, string _message, Structures.EItemNameTestType _testType, string _text = "")
        {
            InitializeComponent();

            this.testType = _testType;
            this.controller = _controller;
            this.Text = _title;
            this.label1.Text = _message;
            this.textBox1.Text = _text;
            this.button1.Text = controller.GetTranslation("ok");
            this.button2.Text = controller.GetTranslation("cancel");
        }

        public string GetInputBoxValue()
        {
            return this.textBox1.Text;
        }

        // cancel button
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // accept button
        private void button1_Click(object sender, EventArgs e)
        {
            switch (testType)
            {
                case Structures.EItemNameTestType.Filename:
                    {
                        if (this.textBox1.Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1)
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show(controller.GetTranslation("error_file_pattern_mismatch"));
                        }
                        break;
                    }
                case Structures.EItemNameTestType.Path:
                    {
                        if (this.textBox1.Text.IndexOfAny(System.IO.Path.GetInvalidPathChars()) == -1)
                        {
                            DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show(controller.GetTranslation("error_folder_pattern_mismatch"));
                        }
                        break;
                    }
                case Structures.EItemNameTestType.None:
                    {
                        DialogResult = DialogResult.OK;
                        break;
                    }
            }
            
        }
    }
}
