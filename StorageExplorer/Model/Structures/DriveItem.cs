using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StorageExplorer.Model.Structures
{
    public struct DriveItem
    {
        public string Name { get; }
        public Image Icon { get; }

        public DriveItem(string _name, Image _icon)
        {
            this.Name = _name;
            this.Icon = _icon;
        }
    }
}
