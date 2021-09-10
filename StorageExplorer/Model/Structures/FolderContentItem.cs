using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageExplorer.Model.Structures
{
    public struct FolderContentItem
    {
        public string Name { get; }
        public string Extension { get; }
        public long Size { get; }
        public DateTime CreationTime { get; }
        public string IconID { get; }
        public EFolderContentItemType ItemType { get; }

        public FolderContentItem(string _name, string _extension, long _size, DateTime _creationTime, string _iconID, EFolderContentItemType _itemType)
        {
            this.Name = _name;
            this.Extension = _extension;
            this.Size = _size;
            this.CreationTime = _creationTime;
            this.IconID = _iconID;
            this.ItemType = _itemType;
        }
    }
}
