using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools;
using SB.ChatBotManagment.BotTools.Models;

namespace SB.ChatBotManagment.Test.Nodes.Route_About
{
    public class NodeAbout : BotNode
    {
        public override string Text => "Example for use SB.ChatBotManagment" + Environment.NewLine + "By Ali Seifaei";
        public override ResponseType ResponseType => ResponseType.TemplateSendMessage;

        public NodeAbout()
        {
            this.Buttons.Add(new ButtonInfo(Texts.Ago));
        }

        public override BotNode Process(RecivedData recivedData)
        {
            if (recivedData.Message == Texts.Ago)
            {
                return new StartNode(String.Empty);
            }
            else
            {
                this.StartMessageText = "Please Select correct item";
            }
            return this;
        }
    }
}
