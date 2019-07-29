using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotTools.Models;

namespace SB.ChatBotManagment.BotTools
{
    public abstract class BotNode
    {
        public DateTime SendDate { get; }
        public string StartMessageText { get; set; }

        public string FileExtension => System.IO.Path.GetExtension(this.FilePath).Replace(".", string.Empty);
        public string FileName => System.IO.Path.GetFileName(this.FilePath);

        public string FilePath { get; set; }
        public long? PayAmount { get; set; }
        public string PayCardNumber { get; set; }

        public FileType FileType { get; set; }

        protected List<Models.ButtonInfo> _buttons = new List<Models.ButtonInfo>();

        public List<Models.ButtonInfo> Buttons => _buttons;

        public abstract string Text { get; }

        public abstract  Models.ResponseType ResponseType { get; }

        public abstract BotNode Process(Models.RecivedData recivedData);



        public BotNode()
        {
            this.FilePath = string.Empty;
        }

        public override string ToString()
        {
            return
                StartMessageText + Environment.NewLine +
                Text + Environment.NewLine +
                ResponseType.ToString() +
                FileName + Environment.NewLine +
                FilePath + Environment.NewLine +
                _buttons.Count;
        }
    }
}
