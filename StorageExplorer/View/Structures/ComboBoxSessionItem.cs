using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageExplorer.View.Structures
{
    public class ComboBoxSessionItem
    {
        public string Name { get; set; }
        public long ID { get; set; }

        public ComboBoxSessionItem(long _id, string _name)
        {
            this.Name = _name;
            this.ID = _id;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
