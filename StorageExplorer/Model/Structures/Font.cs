using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StorageExplorer.Model.Structures
{
    [DataContract(Namespace = "StorageExplorer.Model")]
    public struct Font
    {
        [DataMember]
        private int size;
        [DataMember]
        private string fontFamily;
        [DataMember]
        private Color fontColor;

        public int Size
        {
            get { return this.size; }
            set { this.size = value; }
        }
        public string FontFamily
        {
            get { return this.fontFamily; }
            set { this.fontFamily = value; }
        }
        public Color FontColor
        {
            get { return this.fontColor; }
            set { this.fontColor = value; }
        }

        public Font(string _fontFamily, int _size, Color _fontColor)
        {
            this.fontFamily = _fontFamily;
            this.size = _size;
            this.fontColor = _fontColor;
        }

        public Font(System.Drawing.Font _font, System.Drawing.Color _color)
        {
            this.fontFamily = _font.FontFamily.Name;
            this.size = (int)_font.Size;
            this.fontColor = new Color(_color);
        }
        
    }
}
