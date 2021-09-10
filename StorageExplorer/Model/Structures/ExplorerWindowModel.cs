using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StorageExplorer.Model.Structures
{
    [DataContract(Name = "Window", Namespace = "StorageExplorer.Model")]
    public class ExplorerWindowModel
    {
        // osztály szintű mezők
        [DataMember]
        private int id;
        [DataMember]
        private string path;
        [DataMember]
        private Location windowLocation;
        [DataMember]
        private Size windowSize;
        [DataMember]
        private Font fileListFont;
        [DataMember]
        private Color backgroundColor;
        [DataMember]
        private bool isMaximized;
        [DataMember]
        private EFileListOrder fileListOrder;

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
        public Location WindowLocation
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

        public bool IsMaximized
        {
            get { return this.isMaximized; }
            set { this.isMaximized = value; }
        }

        public EFileListOrder FileListOrder
        {
            get { return this.fileListOrder; }
            set { this.fileListOrder = value; }
        }

        // konstruktor
        public ExplorerWindowModel()
        {
            //ID
            int wID = Settings.AppSettings.GetInt32("windowIdCounter");
            Settings.AppSettings.Set("windowIdCounter", (wID + 1).ToString());
            this.id = wID;

            //ablak méret
            this.windowSize = new Size(600, 400);

            //ablak pozíció
            this.windowLocation = new Location(50, 50);

            //maximalizált állapot
            this.isMaximized = false;

            //útvonal
            this.path = "";

            //háttérszín
            this.backgroundColor = new Color(255, 255, 255);

            //font
            this.fileListFont = new Font("Times New Roman", 12, new Color(0, 0, 0));

            //file list order
            this.fileListOrder = EFileListOrder.NAME_ASC;
        }
    }
}
