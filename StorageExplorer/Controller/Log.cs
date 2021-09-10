using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StorageExplorer.Controller
{
    class Log
    {
        public static readonly string LOG_FILE_PATH = "log.log";
        public static readonly string DEBUG_FILE_PATH = "debug.log";

        public static void Error(int errorCode, string message)
        {
            using (StreamWriter sw = new StreamWriter(LOG_FILE_PATH, true))
            {
                if(message.Substring(message.Length - 2, 2) == "\r\n")
                {
                    message = message.Substring(0, message.Length - 2);
                }
                sw.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH.mm") + "|" + errorCode + "|" + message);
            }
        }

        public static void Debug(string message)
        {
            using (StreamWriter sw = new StreamWriter(DEBUG_FILE_PATH, true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy.MM.dd HH.mm") + ": " + message);
            }
        }
    }
}
