using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StorageExplorer.Model.Structures
{
    struct FolderContent
    {
        public FolderContentItem[] Items { get; }
        public ImageList IconList { get; }

        public FolderContent(FolderContentItem[] _items, ImageList _iconList)
        {
            this.Items = _items;
            this.IconList = _iconList;
        }
    }
}
