using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StorageExplorer.Model.Structures
{
    [DataContract(Namespace = "StorageExplorer.Model")]
    public struct Location
    {
        [DataMember]
        private int x;
        [DataMember]
        private int y;

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public Location(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }
    }
}
