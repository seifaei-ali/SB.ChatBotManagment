using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.ChatBotManagment.BotTools.Tools
{
    public static class Helper
    {
        public delegate void DelegateJob(string msg, bool tl, bool bale, bool gap);
        public static string StartupPath
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        public static string FileFolderPath
        {
            get
            {
                return StartupPath + "\\Files\\";
            }
        }

    }
}
