using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageExplorer.View.Structures
{
    public class ComboBoxLanguageItem
    {
        public string ID { get; }
        public string Name { get; }

        public ComboBoxLanguageItem(string _id, string _name)
        {
            this.ID = _id;
            this.Name = _name;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
