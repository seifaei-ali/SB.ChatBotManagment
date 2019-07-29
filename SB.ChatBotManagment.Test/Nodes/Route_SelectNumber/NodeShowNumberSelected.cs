using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools;
using SB.ChatBotManagment.BotTools.Models;

namespace SB.ChatBotManagment.Test.Nodes.Route_SelectNumber
{
    public class NodeShowNumberSelected : BotNode
    {
        private NodeSelectNumber AgoNode;
        public NodeShowNumberSelected(int selectedNumber, NodeSelectNumber agoNode)
        {
            this._text = "you select " + selectedNumber;
            this.Buttons.Add(new ButtonInfo(Texts.Ago));
            this.AgoNode = agoNode;
        }

        public override BotNode Process(RecivedData recivedData)
        {
            if (recivedData.Message == Texts.Ago)
            {
                return this.AgoNode;
            }
            else
            {
                this._text = "Please Select correct item";
            }
            return this;
        }

        private string _text;
        public override string Text => _text;
        public override ResponseType ResponseType => ResponseType.TemplateSendMessage;
    }
}
