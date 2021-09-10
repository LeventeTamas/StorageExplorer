using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StorageExplorer.Model.Structures
{
    [DataContract(Namespace = "StorageExplorer.Model")]
    public struct Color
    {
        [DataMember]
        public int Red { get; set; }
        [DataMember]
        public int Green { get; set; }
        [DataMember]
        public int Blue { get; set; }

        public Color(int _r, int _g, int _b)
        {
            this.Red = _r;
            this.Green = _g;
            this.Blue = _b;
        }

        public Color(System.Drawing.Color _color)
        {
            this.Red = _color.R;
            this.Green = _color.G;
            this.Blue = _color.B;
        }
    }
}
