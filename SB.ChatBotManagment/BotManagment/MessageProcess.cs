using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools;

namespace SB.ChatBotManagment.BotManagment
{
    public class MessageProcess
    {
        public IBotInfo BotInfo { get; private set; }
        public MessageProcess(IBotInfo botInfo)
        {
            this.BotInfo = botInfo;
        }

        public  Dictionary<string, BotNode> OnlineUsers = new Dictionary<string, BotNode>();
        public  BotNode TextMessageProcess(string recivedMessage, string userId, string name, int sex)
        {
            if (!OnlineUsers.ContainsKey(userId))
            {
                OnlineUsers.Add(userId, BotInfo.StartNode);
            }
            else
            {
                BotTools.Models.RecivedData recivedData = new BotTools.Models.RecivedData(recivedMessage, string.Empty, userId, name, sex);
                BotNode botNode = OnlineUsers[userId];
                OnlineUsers[userId] = botNode.Process(recivedData);
            }

            return OnlineUsers[userId];
        }

        public  BotNode BankMessageProcess(string recivedMessage, string jsonData, string userId)
        {
            if (!OnlineUsers.ContainsKey(userId))
            {
                OnlineUsers.Add(userId, BotInfo.StartNode);
            }
            else
            {
                BotTools.Models.RecivedData recivedData = new BotTools.Models.RecivedData(recivedMessage, jsonData, userId, String.Empty, 0);
                BotNode botNode = OnlineUsers[userId];
                OnlineUsers[userId] = botNode.Process(recivedData);
            }
            return OnlineUsers[userId];
        }
    }
}
