using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;

namespace StorageExplorer.Model
{
    [Serializable]
    class AppSettings : Settings
    {
        public AppSettings()
        {
            filePath = Path.Combine(Application.StartupPath, "appSettings.dat");
            Load();
            if (!settingList.ContainsKey("sessionIdCounter")) settingList.Add("sessionIdCounter", "0");
            
        }

        protected override void Load()
        {
            // app settings (dat) read ...
            /*AppSettings appSet = new AppSettings();
            appSet.Set("session_save_folder", "Sessions");
            appSet.Set("languages_folder", "Languages");
            appSet.Set("selected_session", "proba1");
            return appSet;*/

            if (File.Exists(filePath))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    using (Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        this.settingList = (Dictionary<string, string>)bf.Deserialize(fs);
                    }
                }
                catch (IOException ex)
                {
                    Controller.Log.Error(1030104, "Error while reading the file: " + ex.Message);
                }
                catch (SerializationException ex)
                {
                    Controller.Log.Error(1030108, "Version mismatch or the file is corrupted! Error message: " + ex.Message);
                }
                catch(Exception ex)
                {
                    Controller.Log.Error(1030101, ex.Message);
                }
            }
        }
        public override void Save()
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (Stream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    bf.Serialize(fs, this.settingList);
                }
            }
            catch (IOException ex)
            {
                Controller.Log.Error(1030204, "Error while writing the file: " + ex.Message);
            }
            catch (SerializationException ex)
            {
                Controller.Log.Error(1030208, "Serialization error:" + ex.Message);
            }
            catch (Exception ex)
            {
                Controller.Log.Error(1030201, ex.Message);
            }
        }
    }
}
