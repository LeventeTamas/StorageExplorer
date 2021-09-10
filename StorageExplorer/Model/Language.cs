using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Data;

namespace StorageExplorer.Model
{

    class Language
    {
        //osztály szintű mezők
        private static readonly Dictionary<string, string> DEFAULT_DICTIONARY = new Dictionary<string, string>
        {
            { "MainWindow_title", "StorageExplorer" },
            { "options", "Options" },
            { "settings", "Settings"},
            { "session",  "Session" },
            { "new_session", "New session" },
            { "new_window", "New window" },
            { "rename_session", "Rename session" },
            { "delete_session", "Delete session" },
            { "session_name", "Session name"},
            { "save_session", "Save session" },
            { "error_session_name_empty", "The session name cannot be empty!" },
            { "default_session_name", "(new)" },
            { "layout", "Layout"},
            { "manual", "Manual"},
            { "vertical", "Vertical" },
            { "horizontal", "Horizontal" },
            { "reset_layout", "Reset layout" },
            { "error", "Error" },
            { "error_path_not_valid", "The path is not valid!" },
            { "error_file_not_associated", "This file type is not associated with any programs!" },
            { "error_file_not_found", "File or directory not found!" },
            { "error_directory_not_found", "Directory not found!"},
            { "error_read_write_error", "An error occured while reading/writing a file or directory!" },
            { "error_unauthorized_access", "Access denied!"},
            { "error_device_not_ready", "Device is not ready to use!"},
            { "error_unknown_error", "An unknown error has occured!" },
            { "error_file_pattern_mismatch", "The filename contains invalid characters!" },
            { "error_folder_pattern_mismatch", "The folder name contains invalid characters!" },
            { "error_file_already_exists", "The file is already exists!" },
            { "error_folder_already_exists", "The folder is already exists!" },
            { "global_settings", "Global settings"},
            { "session_settings", "Sessions"},
            { "file_explorer_settings", "File explorer windows"},
            { "enable_autosave", "Enable autosave"},
            { "show_hidden_files", "Show hidden files"},
            { "language", "Language"},
            { "save", "Save"},
            { "cancel", "Cancel"},
            { "location", "Location" },
            { "window_settings", "Explorer window settings" },
            { "font_family", "Font family" },
            { "font_size", "Font size" },
            { "font_color", "Font color" },
            { "background_color", "Background color" },
            { "name", "Name" },
            { "extension", "Extension" },
            { "size", "Size" },
            { "date", "Date" },
            { "delete_files_confirm", "Do you really want to delete the selected item(s)?" },
            { "delete_files_confirm_title", "Delete files" },
            { "rename_session_title", "Rename session" },
            { "new_session_name", "Name of the session" },
            { "new_file", "New file" },
            { "new_folder", "New folder" },
            { "copy", "Copy" },
            { "cut", "Cut" },
            { "paste", "Paste" },
            { "delete", "Delete" },
            { "properties", "Properties" },
            { "rename", "Rename" },
            { "new_folder_name", "The name of the new folder" },
            { "new_file_name", "The name of the new file" },
            { "rename_file", "Rename file" },
            { "rename_folder", "Rename folder" },
            { "folder_new_name", "New name" },
            { "file_new_name", "New name" },
            { "destination", "Destination" },
            { "item", "Item" },
            { "total_progress", "Total progress" },
            { "moving", "Moving" },
            { "copying", "Copying" },
            { "deleting", "Deleting" },
            { "ok", "OK" },
            { "num_of_files", "Number of files" },
            { "num_of_folders", "Number of folders" },
            { "total_size", "Total size" },
            { "bytes", "bytes" },
            { "delete_session_confirm_text", "Are you sure you want to delete the selected session?" },
            { "navigate", "Jump to location"}
        };

        private static List<Language> languageList = new List<Language>();
        private static int selectedLanguageIndex = 0;
        public static string GetSelectedLanguageID()
        {
            return selectedLanguageIndex < languageList.Count ? languageList[selectedLanguageIndex].id : "default";
        }

        // példány szintű mezők    
        private string id;
        private string name;
        private Dictionary<string, string> dictionary = new Dictionary<string, string>();

        // getter-setter
        public string GetID()
        {
            return this.id;
        }
        public string GetName()
        {
            return this.name;
        }

        public Language(string _id, string _name, Dictionary<string, string> _dictionary)
        {
            this.id = _id;
            this.name = _name;
            this.dictionary = _dictionary;
        }

        // osztály szintű metódusok
        public static void Load()
        {
            string langDir = Path.Combine(Application.StartupPath, Settings.AppSettings.Get("languages_folder"));
            
            if (Directory.Exists(langDir)) 
            {
                string[] fileList = new string[0];
                try
                {
                    fileList = Directory.GetFiles(langDir);
                }
                catch (IOException ex)
                {
                    Controller.Log.Error(1010104, "Error while reading the directory: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Controller.Log.Error(1010101, ex.Message);
                }

                foreach (string fileName in fileList)
                {
                    try
                    {
                        string newLangID = "default";
                        string newLangName = "default";
                        Dictionary<string, string> newLangDictionary = new Dictionary<string, string>();
                        using (XmlReader reader = XmlReader.Create(fileName))
                        {
                            string key = string.Empty;
                            string value = string.Empty;
                            while (reader.Read())
                            {
                                switch (reader.NodeType)
                                {
                                    case XmlNodeType.Element:
                                        {
                                            if (reader.Name == "Language")
                                            {
                                                newLangID = reader.GetAttribute("id");
                                                newLangName = reader.GetAttribute("name");
                                            }
                                            else if (reader.Name == "Word")
                                            {
                                                key = reader.GetAttribute("key");
                                            }
                                            break;
                                        }
                                    case XmlNodeType.Text:
                                        {
                                            if (key != string.Empty)
                                            {
                                                value = reader.Value;
                                                if(!newLangDictionary.ContainsKey(key)) newLangDictionary.Add(key, value);
                                                key = string.Empty;
                                                value = string.Empty;
                                            }
                                            break;
                                        }
                                }
                            }
                        }
                        Language newLanguage = new Language(newLangID, newLangName, newLangDictionary);
                        languageList.Add(newLanguage);

                    }
                    catch (XmlException ex)
                    {
                        Controller.Log.Error(1010102, "Incorrect xml format: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Controller.Log.Error(1010101, ex.Message);
                    }
                }
            }
            else
            {
                Controller.Log.Error(1010103, "The directory '" + langDir +"' is not exists");
                try
                {
                    Directory.CreateDirectory(langDir);
                }
                catch
                {
                }
            }
        }

        public static string GetTranslation(string _key)
        {
            return  selectedLanguageIndex < languageList.Count && languageList[selectedLanguageIndex].dictionary.ContainsKey(_key) ? languageList[selectedLanguageIndex].dictionary[_key] : 
                    DEFAULT_DICTIONARY.ContainsKey(_key) ? DEFAULT_DICTIONARY[_key] : 
                    "";
        }

        public static void SelectLanguage(string _id)
        {
            int i = 0;
            while (i < languageList.Count && languageList[i].id != _id) i++;
            if (i < languageList.Count) selectedLanguageIndex = i;
        }

        public static string[] GetLanguageList()
        {
            string[] languages = new string[languageList.Count];
            for (int i = 0; i < languages.Length; i++)
            {
                languages[i] = languageList[i].id + ";" + languageList[i].name;
            }
            return languages;
        }
    }
}
