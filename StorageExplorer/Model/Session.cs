using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Collections;

[assembly: ContractNamespace("", ClrNamespace = "StorageExplorer.Model")]
namespace StorageExplorer.Model
{

    [DataContract (Name = "Session", Namespace = "StorageExplorer.Model")]
    public class Session
    {
        // osztály szintű mezők
        private static List<Session> sessionList = new List<Session>();
        private static int selectedIndex = 0;

        // példány szintű mezők
        private string fileName;
        [DataMember]
        private long id;
        [DataMember]
        private string name;
        [DataMember (Name = "WindowList")]
        private List<Structures.ExplorerWindowModel> expWinModelList = new List<Structures.ExplorerWindowModel>();
        [DataMember]
        private Structures.ESessionLayout layout = Structures.ESessionLayout.Manual;

        // getter-setter
        public long GetID()
        {
            return this.id;
        }
        public string GetName()
        {
            return this.name;
        }
        public void SetName(string _name)
        {
            this.name = _name;
        }
      
        public List<Structures.ExplorerWindowModel> GetExplorerWindowModelList()
        {
            return this.expWinModelList;
        }

        public Structures.ESessionLayout GetLayout()
        {
            return this.layout;
        }
        public void SetLayout(Structures.ESessionLayout _layout)
        {
            this.layout = _layout;
        }

        public static Session GetSelectedSession()
        {
            return selectedIndex < sessionList.Count ? sessionList[selectedIndex] : null;
        }
        public static long GetDefaultSessionID()
        {
            return sessionList[0].GetID();
        }

        public Session()
        {
            long sID = Settings.AppSettings.GetInt64("sessionIdCounter");
            Settings.AppSettings.Set("sessionIdCounter", (sID+1).ToString());
            this.id = sID;
        }



        // osztály szintű metódusok
        public static void DeleteSession(long _sessionID)
        {
            int index = GetSessionIndex(_sessionID);
            if (index != 0)
            {
                string sessionSaveFolder = Path.Combine(Application.StartupPath, Settings.AppSettings.Get("session_save_folder"));
                string fileName = sessionList[index].fileName;
                try
                {
                    System.IO.File.Delete(Path.Combine(sessionSaveFolder, fileName));
                }
                catch (Exception ex)
                {
                    Controller.Log.Error(1020101, ex.Message);
                }
                sessionList.RemoveAt(index);
            }
        }

        public static bool IsSelectedDefaultSession()
        {
            return selectedIndex == 0;
        }

        public static int GetSelectedIndex()
        {
            return selectedIndex;
        }

        public static Session SaveDefaultSession(string _name)
        {
            Session newSession = new Session
            {
                expWinModelList = sessionList[0].expWinModelList,
                name = _name,
                layout = sessionList[0].layout
            };
            sessionList.Add(newSession);
            newSession.Save();

            return newSession;
        }

        public static void ResetDefaultSession()
        {
            sessionList[0] = new Session
            {
                name = Language.GetTranslation("default_session_name")
            };
        }

        public static List<string> GetSessionNameList()
        {
            List<string> nameList = new List<string>();
            foreach (Session session in sessionList)
            {
                nameList.Add(session.id + ";" +session.name);
            }
            return nameList;
        }
        public static void LoadSessionList()
        {
            // az alapértelmezett munkamenet mindíg a lista első eleme
            sessionList.Add(new Session
            {
                name = Language.GetTranslation("default_session_name"),
            });

            // a mentett munkamenetek betöltése, és hozzáadása a munkamenet-listához
            string sessionSaveFolder = Path.Combine(Application.StartupPath, Settings.AppSettings.Get("session_save_folder"));
            if (Directory.Exists(sessionSaveFolder))
            {
                string[] files = new string[0];
                try
                {
                    files = Directory.GetFiles(sessionSaveFolder);
                }
                catch (IOException ex)
                {
                    Controller.Log.Error(1020204, "Error while reading the driectory: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Controller.Log.Error(1020201, ex.Message);
                }

                foreach (string filePath in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        if (fileInfo.Extension == ".xml")
                        {
                            DataContractSerializer dcs = new DataContractSerializer(typeof(Session));
                            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read)) 
                            {
                                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                                Session newSession = (Session)dcs.ReadObject(reader);
                                newSession.fileName = fileInfo.Name;
                                sessionList.Add(newSession);
                            }
                           
                        }
                    }
                    catch (IOException ex)
                    {
                        Controller.Log.Error(1020204, "Error while reading the file: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Controller.Log.Error(1020201, ex.Message);
                    }
                }
                
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(sessionSaveFolder);
                }
                catch
                {

                }
            }
        }
        public static void SaveAll()
        {
            for (int i = 1; i < sessionList.Count; i++)
            {
                sessionList[i].Save();
            }
        }

        public static void SelectSession(long _id)
        {
            int i = 0;
            while (i < sessionList.Count && sessionList[i].id != _id) i++;
            if (i < sessionList.Count) selectedIndex = i;
            else selectedIndex = 0;
        }
        public static void SelectSession(string _name)
        {
            int i = 0;
            while (i < sessionList.Count && sessionList[i].name != _name) i++;
            if (i < sessionList.Count) selectedIndex = i;
        }
        private static int GetSessionIndex(long _sessionID)
        {
            int i = 0;
            while (i < sessionList.Count && sessionList[i].id != _sessionID) i++;
            return i == sessionList.Count ? -1 : i;
        }

        // példány szintű metódusok
        public Structures.ExplorerWindowModel AddWindow()
        {
            Structures.ExplorerWindowModel model = new Structures.ExplorerWindowModel();
            expWinModelList.Add(model);
            return model;
        }
        public Structures.ExplorerWindowModel GetWindowModel(int _windowID)
        {
            int i = 0;
            while (i < expWinModelList.Count && expWinModelList[i].ID != _windowID) i++;
            return expWinModelList[i];
        }
        public void CloseWindow(int _windowID)
        {
            expWinModelList.Remove(GetWindowModel(_windowID));
        }
        public void Save()
        {
            // ha nincs még a munkamenetnek fájlneve, akkor generálunk egyet
            if(fileName == null)
            {
                fileName = "session_" + this.id + ".xml";
            }

            // ha nincs létrehozva a munkamenet mappa akkor hozzuk létre
            string save_folder = Path.Combine(Application.StartupPath, Settings.AppSettings.Get("session_save_folder"));
            if (!Directory.Exists(save_folder)) Directory.CreateDirectory(save_folder);

            // fájl létrehozása
            string filePath = Path.Combine(save_folder, this.fileName);
            DataContractSerializer ser = new DataContractSerializer(typeof(Session));
            using (XmlWriter fs = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
            {
                ser.WriteObject(fs, this);
            }
        }
    }
}
