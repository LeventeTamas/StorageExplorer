using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace StorageExplorer.Model
{
    [Serializable]
    abstract class Settings
    {
        // osztály szintű adattagok
        private static Settings userSettings;
        private static Settings appSettings;

        // osztály szintű getterek
        public static Settings UserSettings
        {
            get
            {
                return userSettings;
            }
        }
        public static Settings AppSettings
        {
            get
            {
                return appSettings;
            }
        }

        //példány szintű adattagok
        protected Dictionary<string, string> settingList = new Dictionary<string, string>();
        protected string filePath;

        // osztály szintű metódusok
        public static void LoadSettings()
        {
            userSettings = new UserSettings();
            appSettings = new AppSettings();
        }

        // példány szintű metódusok
        protected abstract void Load();
        public abstract void Save();
        public string Get(string _key)
        {
            return settingList.ContainsKey(_key) ? settingList[_key] : "";
        }
        public int GetInt32(string _key)
        {
            int value = 0;
            if (settingList.ContainsKey(_key)) {
                int.TryParse(settingList[_key], out value);
            }
            return value;
        }
        public long GetInt64(string _key)
        {
            long value = 0;
            if (settingList.ContainsKey(_key))
            {
                long.TryParse(settingList[_key], out value);
            }
            return value;
        }
        public bool GetBoolean(string _key)
        {
            bool value = false;
            if (settingList.ContainsKey(_key))
            {
                bool result = bool.TryParse(settingList[_key], out value);
                Console.WriteLine();
            }
            return value;
        }
        public void Set(string _key, string _value)
        {
            if (settingList.ContainsKey(_key))
            {
                if (settingList[_key] != _value)
                {
                    settingList[_key] = _value;
                    Save();
                }
            }
            else
            {
                settingList.Add(_key, _value);
                Save();
            }
        }

        public void SetMultiple(Dictionary<string, string> _newSettings)
        {
            foreach (KeyValuePair<string, string> item in _newSettings)
            {
                if (settingList.ContainsKey(item.Key))
                {
                    settingList[item.Key] = item.Value;
                }
            }
            Save();
        }

        public Dictionary<string, string> GetSettingList()
        {
            // Hogy ne lehessen közvetlenül módosítani a beállítás listát, bemásoljuk egy új listába és azt adjuk vissza
            Dictionary<string, string> settings = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> setting in settingList)
            {
                settings.Add(setting.Key, setting.Value);
            }
            return settings;
        }
    }
}
