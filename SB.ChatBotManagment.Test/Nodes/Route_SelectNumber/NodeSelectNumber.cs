using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools;
using SB.ChatBotManagment.BotTools.Models;

namespace SB.ChatBotManagment.Test.Nodes.Route_SelectNumber
{
    public class NodeSelectNumber : BotNode
    {
        public override string Text => "Select a item";
        public override ResponseType ResponseType => ResponseType.TemplateSendMessage;

        public NodeSelectNumber()
        {
            this.Buttons.Add(new ButtonInfo("1", 0));
            this.Buttons.Add(new ButtonInfo("2", 0));
            this.Buttons.Add(new ButtonInfo("3", 0));
            this.Buttons.Add(new ButtonInfo("4", 1));
            this.Buttons.Add(new ButtonInfo("5", 1));
            this.Buttons.Add(new ButtonInfo("6", 1));
            this.Buttons.Add(new ButtonInfo("7", 2));
            this.Buttons.Add(new ButtonInfo("8", 2));
            this.Buttons.Add(new ButtonInfo("9", 2));
            this.Buttons.Add(new ButtonInfo("0", 3));
            this.Buttons.Add(new ButtonInfo(Texts.Ago, 6));
        }

        public override BotNode Process(RecivedData recivedData)
        {
            int tmp;
            if (int.TryParse(recivedData.Message, out tmp))
            {
                return  new NodeShowNumberSelected(tmp, this);
            }
            else if (recivedData.Message == Texts.Ago)
            {
                return new StartNode(string.Empty);
            }
            else
            {
                this.StartMessageText = "Please Select correct item";
            }
            return this;
        }
    }
}
