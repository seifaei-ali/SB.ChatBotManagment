using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools;
using SB.ChatBotManagment.Test.Nodes;

namespace SB.ChatBotManagment.Test
{
    public class BotInfo : IBotInfo
    {
        public event BotTools.Tools.Helper.DelegateJob SendNotification;
        internal void SendNotificationMessage(string msg)
        {
            this.SendNotification?.Invoke(msg, !string.IsNullOrEmpty(Token_Telegram), !string.IsNullOrEmpty(Token_Bale), !string.IsNullOrEmpty(Token_Gap));
        }

        public string Id => "SB_ChatBotmanagmentTestBot";
        public string Token_Bale => "142747604:4d64759af60f5aeb42413dc2404fd36d603b8267";
        public string Token_Telegram => "955860275:AAEsNaYe2cPJSCLT0CEty0NQDLslqDZ47fU";
        public string Token_Gap => "";
        public BotNode StartNode => new StartNode("Hello dear");
        public void ListenRun()
        {
           
        }
    }
}
