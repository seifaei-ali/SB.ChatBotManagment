using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools.Tools;

namespace SB.ChatBotManagment.BotTools
{
    public interface IBotInfo
    {
        event Tools.Helper.DelegateJob SendNotification;
        string Id { get; }
        string Token_Bale { get; }
        string Token_Telegram { get; }

        string Token_Gap { get; }

        BotNode StartNode { get; }

        void ListenRun();
    }
}
