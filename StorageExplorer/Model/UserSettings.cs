using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace StorageExplorer.Model
{
    class UserSettings : Settings
    {
        
        public UserSettings()
        {
            filePath = Path.Combine(Application.StartupPath, "userSettings.conf");
            Load();  
        }

        protected override void Load()
        {
            if (File.Exists(filePath)) 
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (line.Contains('='))
                            {
                                string[] keyValuePair = line.Split('=');
                                settingList.Add(keyValuePair[0], keyValuePair[1]);
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Controller.Log.Error(1040104, "Error while reading the file: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Controller.Log.Error(1040101, ex.Message);
                }
            }
        }

        public override void Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    foreach (KeyValuePair<string, string> line in settingList)
                    {
                        sw.WriteLine(line.Key + "=" + line.Value);
                    }
                }
            }
            catch (IOException ex)
            {
                Controller.Log.Error(1040204, "Error while reading the file: " + ex.Message);
            }
            catch (Exception ex)
            {
                Controller.Log.Error(1040201, ex.Message);
            }
        }
    }
}
