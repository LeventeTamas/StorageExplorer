using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace StorageExplorer.Model
{

    public delegate void FileMovingProgressChangedEventHandler(Structures.FileMovingProgressEventArgs e);
    public delegate void FileMovingEnded(int index, int windowID);
    public delegate void FileMovingError(int  index, int errorCode, string errorMessage, Exception ex);


    public class FileMovingProcess
    {
        private Controller.Controller controller;
        private int index;
        public event FileMovingProgressChangedEventHandler OnFileMovingProgressChanged;
        public event FileMovingEnded OnFileMovingEnded;
        public event FileMovingError OnFileMovingError;
        private bool isAborted;
        private ulong totalSize = 0;
        private int totalCount = 0;
        private ulong movedSize = 0;
        private int movedCount = 0;
        private string[] itemPathList;
        private string destinationDir;
        private int windowID;

        public bool IsAborted
        {
            get
            {
                return isAborted;
            }
        }

        public FileMovingProcess(int _index, int _windowID, string[] _itemPathList, string _destinationDir, Controller.Controller _controller)
        {
            this.controller = _controller;
            this.isAborted = false;
            this.itemPathList = _itemPathList;
            this.destinationDir = _destinationDir;
            this.index = _index;
            this.windowID = _windowID;
        }

        // hibakód: 10501xx
        public void StartCopy()
        {
            new Thread(
               new ThreadStart(() =>
               {
                   try
                   {
                       CollectInfo(itemPathList); // információt gyűjt a másolandó elemekről (totalSize, totalCount) 
                       Copy(itemPathList, destinationDir); // másolási folyamat   
                   }
                   catch (Exception ex)
                   {
                       OnFileMovingError(this.index, 1050101, controller.GetTranslation("error_unknown_error"), ex);
                   }
                   OnFileMovingEnded(this.index, this.windowID); // a másolás véget ért (vagy megszakította a felhasználó), kiküldjük az eseményt
               })
            ).Start();
        }

        // hibakód: 10503xx
        public void StartDelete()
        {
            new Thread(
              new ThreadStart(() =>
              {
                  try 
                  {
                       CollectInfo(itemPathList); 
                       Delete(itemPathList); 
                       
                  }
                  catch (Exception ex)
                  {
                      OnFileMovingError(this.index, 1050301, controller.GetTranslation("error_unknown_error"), ex);
                  }
                  OnFileMovingEnded(this.index, this.windowID);
              })
           ).Start();
        }

        private void CollectInfo(string[] _itemPathList)
        {
            for (int i = 0; i < _itemPathList.Length && !isAborted; i++)
            {
                string sourcePath = _itemPathList[i];
                if (File.GetAttributes(sourcePath).HasFlag(FileAttributes.Directory))
                {
                    string[] subPathList = Directory.GetFileSystemEntries(sourcePath);
                    CollectInfo(subPathList);
                }
                else
                {
                    totalSize += (ulong)new FileInfo(sourcePath).Length;
                }
                totalCount++;
            }
        }

        private void Copy(string[] _itemPathList, string _destinationDir)
        {
            for (int i = 0; i < _itemPathList.Length && !isAborted; i++)
            {
                string sourcePath = _itemPathList[i];
                if (File.GetAttributes(sourcePath).HasFlag(FileAttributes.Directory))
                {
                    string destinationPath = Path.Combine(_destinationDir, new DirectoryInfo(sourcePath).Name);
                    Directory.CreateDirectory(destinationPath);

                    string[] subPathList = Directory.GetFileSystemEntries(sourcePath);
                    Copy(subPathList, destinationPath);
                }
                else
                {
                    string destination = Path.Combine(_destinationDir, Path.GetFileName(sourcePath));
                    //if (sourcePath != destination)
                    //{
                        // másolás
                        using (FileStream reader = new FileStream(sourcePath, FileMode.Open))
                        {
                            using (FileStream writer = new FileStream(destination, FileMode.Create))
                            {
                                long length = reader.Length;
                                long count = 0;
                                while (count < length && !isAborted)
                                {
                                    writer.WriteByte((byte)reader.ReadByte());
                                    count++;
                                    movedSize++;
                                    // folyamat állapotának frissítése legfeljebb 32KB-onként, vagy amennyi másolandó adat van még
                                    if (count % (Math.Max(Math.Min(32 * 1024, length), 1)) == 0)
                                    {
                                        float itemProgress = count * 100F / length;
                                        float totalProgress = (float)movedSize / totalSize * 100F;
                                        OnFileMovingProgressChanged(new Structures.FileMovingProgressEventArgs(this.index, destinationDir, sourcePath, totalCount, movedCount, itemProgress, totalProgress));
                                    }
                                }
                            }
                        }
                    //}
                }
                movedCount++;
            }
        }

        // hibakód: 10502xx
        public void StartMove()
        {
            new Thread(
              new ThreadStart(() =>
              {
                  try
                  {
                      CollectInfo(itemPathList);

                      string itemsDrive = Path.GetPathRoot(itemPathList[0]);
                      string destDrive = Path.GetPathRoot(destinationDir);
                      if (itemsDrive == destDrive)
                      {
                          MoveLogically(itemPathList, destinationDir);
                      }
                      else
                      {
                          MovePhysically(itemPathList, destinationDir);
                      }

                      
                  }
                  catch (Exception ex)
                  {
                      OnFileMovingError(this.index, 1050201, controller.GetTranslation("error_unknown_error"), ex);
                  }
                  OnFileMovingEnded(this.index, this.windowID);
              })
           ).Start();
        }

        private void MoveLogically(string[] _itemPathList, string _destinationDir)
        {
            for (int i = 0; i < _itemPathList.Length && !isAborted; i++)
            {
                string sourcePath = _itemPathList[i];
                if (File.GetAttributes(sourcePath).HasFlag(FileAttributes.Directory))
                {
                    string destinationPath = Path.Combine(_destinationDir, new DirectoryInfo(sourcePath).Name);
                    string[] subPathList = Directory.GetFileSystemEntries(sourcePath);

                    Directory.CreateDirectory(destinationPath);
                    MoveLogically(subPathList, destinationPath);
                    Directory.Delete(sourcePath);
                    
                }
                else
                {
                    string destination = Path.Combine(_destinationDir, Path.GetFileName(sourcePath));
                    File.Move(sourcePath, destination);
                }

                movedCount++;
                float totalProgress = (float)movedCount / totalCount * 100;
                OnFileMovingProgressChanged(new Structures.FileMovingProgressEventArgs(this.index, destinationDir, sourcePath, totalCount, movedCount, 0, totalProgress));
            }
        }

        private void MovePhysically(string[] _itemPathList, string _destinationDir)
        {
            Copy(_itemPathList, _destinationDir);
            Delete(_itemPathList);
        }

        public void Delete(string[] _itemPathList)
        {
            for (int i = 0; i < _itemPathList.Length && !isAborted; i++)
            {
                string sourcePath = _itemPathList[i];
                if (File.GetAttributes(sourcePath).HasFlag(FileAttributes.Directory))
                {
                    string[] subPathList = Directory.GetFileSystemEntries(sourcePath);
                    Delete(subPathList);
                    Directory.Delete(sourcePath);
                }
                else
                {
                    File.Delete(sourcePath);
                }
                movedCount++;
                float totalProgress = (float)movedCount / totalCount * 100;
                OnFileMovingProgressChanged(new Structures.FileMovingProgressEventArgs(this.index, destinationDir, sourcePath, totalCount, movedCount, 0, totalProgress));
            }
        }

        public void Abort()
        {
            this.isAborted = true;
        }
    }
}
