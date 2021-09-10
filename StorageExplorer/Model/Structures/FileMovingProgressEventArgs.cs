using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageExplorer.Model.Structures
{
    public class FileMovingProgressEventArgs
    {
        public string Destination { get; }
        public string CurrentItem { get; }
        public int NumberOfTotalItems { get; }
        public int NumberOfMovedItems { get; set; }
        public float CurrentItemProgress { get; }
        public float TotalProgress { get; }
        public int Index { get; }

        public FileMovingProgressEventArgs(int _index, string _destination, string _currentItem, int _numberOfItems, int _numberOfMovedItems, float _currentItemProgress, float _totalProgress)
        {
            this.Index = _index;
            this.Destination = _destination;
            this.CurrentItem = _currentItem;
            this.NumberOfTotalItems = _numberOfItems;
            this.NumberOfMovedItems = _numberOfMovedItems;
            this.CurrentItemProgress = _currentItemProgress;
            this.TotalProgress = _totalProgress;
        }
    }
}
