using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools;
using SB.ChatBotManagment.BotTools.Models;
using SB.ChatBotManagment.Test.Nodes.Route_About;
using SB.ChatBotManagment.Test.Nodes.Route_SelectNumber;

namespace SB.ChatBotManagment.Test.Nodes
{
    public class StartNode : BotNode
    {
        public override string Text => "Please select a item";
        public override ResponseType ResponseType => ResponseType.TemplateSendMessage;

        public StartNode(string startMessage)
        {
            this.StartMessageText = startMessage;
            this.Buttons.Add(new ButtonInfo(Texts.SelectNumber));
            this.Buttons.Add(new ButtonInfo(Texts.About));
        }

        public override BotNode Process(RecivedData recivedData)
        {

            if (recivedData.Message == Texts.SelectNumber)
            {
                return new NodeSelectNumber();
            }
            if (recivedData.Message == Texts.About)
            {
                return new NodeAbout();
            }
            else
            {
                this.StartMessageText = "Please Select correct item";
            }
            return this;
        }
    }
}
