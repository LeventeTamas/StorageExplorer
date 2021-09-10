using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace StorageExplorer.Model
{
    static class IOManager
    {
        // Egy Fájl/mappa tulajdonságainak megjelenítéséhez szükséges
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref Structures.SHELLEXECUTEINFO lpExecInfo);
        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;

        public static Structures.DriveItem[] GetDriveList()
        {
            DriveInfo[] driveList = DriveInfo.GetDrives();
            Structures.DriveItem[] driveItemList = new Structures.DriveItem[driveList.Length];
            for(int i = 0; i < driveList.Length; i++)
            {
                Image icon = Properties.Resources.icon_hdd;
                switch (driveList[i].DriveType)
                {
                    case DriveType.Removable:
                        icon = Properties.Resources.icon_flash;
                        break;
                    case DriveType.Network:
                        icon = Properties.Resources.icon_network;
                        break;
                    case DriveType.CDRom:
                        icon = Properties.Resources.icon_cdrom;
                        break;
                    default:
                        break;
                }

                driveItemList[i] = new Structures.DriveItem(driveList[i].Name, icon);
            }
            return driveItemList;
        }

        public static Structures.FolderContent GetFolderContent(string _path, bool _showHiddenFiles, Structures.EFileListOrder _order)
        {
            // Ikon lista létrehozása
            ImageList iconList = new ImageList();
            iconList.ImageSize = new System.Drawing.Size(18, 18);
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            // Alap ikonok hozzáadása
            iconList.Images.Add("<DIR>", Properties.Resources.icon_folder);
            iconList.Images.Add("<DIR>.hidden", GetHiddenIcon(Icon.FromHandle(Properties.Resources.icon_folder.GetHicon())));
            iconList.Images.Add("[..]", Properties.Resources.icon_parent);
            

            // Mappa lista létrehozása
            List<Structures.FolderContentItem> folderList = new List<Structures.FolderContentItem>();
            foreach(string dir in Directory.GetDirectories(_path))
            {  
                if (_showHiddenFiles || !File.GetAttributes(dir).HasFlag(FileAttributes.Hidden))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(dir);
                    string iconKey = "<DIR>";
                    if (File.GetAttributes(dir).HasFlag(FileAttributes.Hidden)) iconKey = "<DIR>.hidden";
                    folderList.Add(new Structures.FolderContentItem(dirInfo.Name, "", 0, dirInfo.LastWriteTime, iconKey, Structures.EFolderContentItemType.Folder));
                }
            }

            // Fájl lista létrehozása
            List<Structures.FolderContentItem> fileList = new List<Structures.FolderContentItem>();
            foreach (string file in Directory.GetFiles(_path))
            {
                bool isHidden = File.GetAttributes(file).HasFlag(FileAttributes.Hidden);
                if (_showHiddenFiles || !isHidden)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    // minden fájlhoz tartozik egy ikon a listában (ugyan az az ikon többször is szerepelhet benne)
                    // iconKey: ezzel az azonosítóval fogja tudni a nézet a fájlhoz tartozó ikont kikérni az ikonlistából
                    string iconKey = fileInfo.FullName;
                    Icon icon = Icon.ExtractAssociatedIcon(file);
                    if (isHidden) icon = GetHiddenIcon(icon);
                    iconList.Images.Add(iconKey, icon);

                    // fájlnév és kiterjesztés szétválasztása
                    string fileNameType = fileInfo.Name;
                    string fileName = fileNameType;
                    string fileExtension = "";  
                    int lastIndex = fileNameType.LastIndexOf('.');
                    if (lastIndex > 0)
                    {
                        fileName = fileNameType.Substring(0, lastIndex);
                        fileExtension = fileNameType.Substring(lastIndex, fileNameType.Length - lastIndex);
                    }
                    
                    // fájl adatainak hozzáadása a listához
                    fileList.Add(new Structures.FolderContentItem(fileName, fileExtension, fileInfo.Length, fileInfo.LastWriteTime, iconKey, Structures.EFolderContentItemType.File));
                }
            }

            //Listák rendezése
            switch (_order)
            {
                case Structures.EFileListOrder.NAME_ASC:
                    {
                        folderList = folderList.OrderBy(o => o.Name).ToList();
                        fileList = fileList.OrderBy(o => o.Name).ToList();
                        break;
                    }
                case Structures.EFileListOrder.NAME_DESC:
                    {
                        folderList = folderList.OrderBy(o => o.Name).ToList();
                        folderList.Reverse();
                        fileList = fileList.OrderBy(o => o.Name).ToList();
                        fileList.Reverse();
                        break;
                    }
                case Structures.EFileListOrder.EXTENSION_ASC:
                    {
                        fileList = fileList.OrderBy(o => o.Extension).ToList();
                        break;
                    }
                case Structures.EFileListOrder.EXTENSION_DESC:
                    {
                        fileList = fileList.OrderBy(o => o.Extension).ToList();
                        fileList.Reverse();
                        break;
                    }
                case Structures.EFileListOrder.SIZE_ASC:
                    {
                        fileList = fileList.OrderBy(o => o.Size).ToList();
                        break;
                    }
                case Structures.EFileListOrder.SIZE_DESC:
                    {
                        fileList = fileList.OrderBy(o => o.Size).ToList();
                        fileList.Reverse();
                        break;
                    }
                case Structures.EFileListOrder.DATE_ASC:
                    {
                        folderList = folderList.OrderBy(o => o.CreationTime).ToList();
                        fileList = fileList.OrderBy(o => o.CreationTime).ToList();
                        break;
                    }
                case Structures.EFileListOrder.DATE_DESC:
                    {
                        folderList = folderList.OrderBy(o => o.CreationTime).ToList();
                        folderList.Reverse();
                        fileList = fileList.OrderBy(o => o.CreationTime).ToList();
                        fileList.Reverse();
                        break;
                    }
            }

            // Végleges lista létrehozása
            List<Structures.FolderContentItem> itemList = new List<Structures.FolderContentItem>();

            // visszalépés gomb hozzáadása a végleges listához
            DirectoryInfo pathInfo = new DirectoryInfo(_path);
            if (pathInfo.Parent != null)
            {
                itemList.Add(new Structures.FolderContentItem("[..]", "", 0, pathInfo.Parent.CreationTime, "[..]", Structures.EFolderContentItemType.BackButton));
            }

            // mappa és fájl lista hozzáfűzése a végleges listához
            itemList.AddRange(folderList);
            itemList.AddRange(fileList);

            return new Structures.FolderContent(itemList.ToArray(), iconList);
        }

        private static Icon GetHiddenIcon(Icon _icon)
        {
            Bitmap iconBitmap = new Bitmap(_icon.Width, _icon.Height);
            Graphics g = Graphics.FromImage(iconBitmap);
            ColorMatrix cm = new ColorMatrix();
            ImageAttributes ia = new ImageAttributes();
            Font font = new Font("Times New Roman", 14, FontStyle.Bold);
            string hiddenFileMark = "!";
            SizeF stringSize = Graphics.FromImage(new Bitmap(1, 1)).MeasureString(hiddenFileMark, font);

            cm.Matrix33 = 0.55f;
            ia.SetColorMatrix(cm);

            g.DrawImage(_icon.ToBitmap(), new Rectangle(0, 0, _icon.Width, _icon.Height), 0, 0, _icon.Width, _icon.Height, GraphicsUnit.Pixel, ia);
            g.DrawString("!", font, new SolidBrush(Color.Red), iconBitmap.Width / 2  - stringSize.Width / 2, iconBitmap.Height / 2 - stringSize.Height / 2);
            g.Dispose();

            return Icon.FromHandle(iconBitmap.GetHicon());
        }

        public static bool IsPathValid(string _path)
        {
            return Directory.Exists(_path);
        }

        public static bool IsDeviceReady(string _path)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            int i = 0;
            string root = _path.Split('\\')[0] + "\\";
            while (i < drives.Length && drives[i].Name != root) i++;
            return drives[i].IsReady;
        }

        public static void OpenFile(string _path)
        {
            Process.Start(_path);
        }

        public static string GetValidPath()
        {
            return DriveInfo.GetDrives()[0].Name;
        }

        public static bool IsFolder(string _itemPath)
        {
            return File.GetAttributes(_itemPath).HasFlag(FileAttributes.Directory);
        }

        public static string GetItemName(string _itemPath)
        {
            return Path.GetFileName(_itemPath);
        }

        public static void Rename(string _name, string _itemPath)
        {
            string itemNewPath = Path.Combine(Path.GetDirectoryName(_itemPath), _name);
            if (File.GetAttributes(_itemPath).HasFlag(FileAttributes.Directory))
            {
                Directory.Move(_itemPath, itemNewPath);
            }
            else
            {
                File.Move(_itemPath, itemNewPath);
            }
        }

        public static string CombinePath(string _directory, string _fileName)
        {
            return Path.Combine(_directory, _fileName);
        }

        public static void CreateFolder(string _path)
        {
            if (!Directory.Exists(_path)) 
            {
                Directory.CreateDirectory(_path);
            }
            else
            {
                throw new IOException("Folder is already exists!", unchecked((int)0x80070050));
            }
        }

        public static void CreateFile(string _filePath)
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
            }
            else
            {
                throw new IOException("File is already exists!", unchecked((int)0x80070050));
            }
        }

        public static void ShowItemProperties(string _itemPath)
        {
            Structures.SHELLEXECUTEINFO info = new Structures.SHELLEXECUTEINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = _itemPath;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            ShellExecuteEx(ref info);
        }


        private static ulong totalSize = 0;
        private static int numOfFiles = 0;
        private static int numOfFolders = 0;
        public static void GetItemListProperties(string[] _itemPathList, out int _numOfFiles, out int _numOfFolders, out ulong _totalSize)
        {
            totalSize = 0;
            numOfFiles = 0;
            numOfFolders = 0;
            GetItemListPropertiesRecursion(_itemPathList);
            _numOfFiles = numOfFiles;
            _numOfFolders = numOfFolders;
            _totalSize = totalSize;
        }

        private static void GetItemListPropertiesRecursion(string[] _itemPathList)
        {
            foreach (string itemPath in _itemPathList) {
                if (File.GetAttributes(itemPath).HasFlag(FileAttributes.Directory))
                {
                    numOfFolders++;
                    string[] subPathList = Directory.GetFileSystemEntries(itemPath);
                    GetItemListPropertiesRecursion(subPathList);
                }
                else
                {
                    numOfFiles++;
                    totalSize += (ulong)new FileInfo(itemPath).Length;
                }
            }
        }

    }
}
