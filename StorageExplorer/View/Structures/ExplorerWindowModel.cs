using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StorageExplorer.View.Structures
{
    public class ExplorerWindowModel
    {
        // osztály szintű mezők
        private int id;
        private string path;
        private Point windowLocation;
        private Size windowSize;
        private Font fileListFont;
        private Color fileListFontColor;
        private Color backgroundColor;
        private bool isMaximized;
        

        // getter, setter
        public int ID
        {
            get { return this.id; }
        }
        public string Path
        {
            get { return this.path; }
            set { this.path = value; }
        }
        public Point WindowLocation
        {
            get { return this.windowLocation; }
            set { this.windowLocation = value; }
        }
        public Size WindowSize
        {
            get { return this.windowSize; }
            set { this.windowSize = value; }
        }
        public Color BackgroundColor
        {
            get { return this.backgroundColor; }
            set { this.backgroundColor = value; }
        }
        public Font FileListFont
        {
            get { return this.fileListFont; }
            set { this.fileListFont = value; }
        }
        public Color FileListFontColor
        {
            get { return this.fileListFontColor; }
            set { this.fileListFontColor = value; }
        }
        public bool IsMaximized
        {
            get { return this.isMaximized; }
            set { this.isMaximized = value; }
        }

        public ExplorerWindowModel(int _id, string _path, Point _windowLocation, Size _windowSize, bool _isMaximized, Font _fileListFont, Color _fileListFontColor, Color _backgroundColor)
        {
            this.id = _id;
            this.path = _path;
            this.windowLocation = _windowLocation;
            this.windowSize = _windowSize;
            this.fileListFont = _fileListFont;
            this.fileListFontColor = _fileListFontColor;
            this.backgroundColor = _backgroundColor;
            this.isMaximized = _isMaximized;
        }
    }
}
