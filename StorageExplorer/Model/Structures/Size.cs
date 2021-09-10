using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StorageExplorer.Model.Structures
{
    [DataContract(Namespace = "StorageExplorer.Model")]
    public struct Size
    {
        [DataMember]
        private int width;
        [DataMember]
        private int height;

        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public Size(int _width, int _height)
        {
            this.width = _width;
            this.height = _height;
        }
    }
}
