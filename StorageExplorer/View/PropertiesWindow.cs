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
    public partial class PropertiesWindow : Form
    {
        Controller.Controller controller;
        public PropertiesWindow(Controller.Controller _controller, int _numOfFiles, int _numOfFolders, ulong _totalSize)
        {
            InitializeComponent();
            this.controller = _controller;
            this.Text = controller.GetTranslation("properties");
            this.lbFiles.Text = controller.GetTranslation("num_of_files")+": " + _numOfFiles;
            this.lbFolders.Text = controller.GetTranslation("num_of_folders")+": " + _numOfFolders;
            this.lbSize.Text = controller.GetTranslation("total_size")+": " + _totalSize + " " + controller.GetTranslation("bytes");
        }
    }
}
