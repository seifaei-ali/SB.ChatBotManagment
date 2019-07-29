using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SB.ChatBotManagment.BotTools.Models
{
    public class RecivedData
    {
        public string Message { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }

        public int Sex { get; set; }

        public Dictionary<string, string> Data { get; private set; }

        public RecivedData(string message, string jsonData, string userId, string name, int sex)
        {
            this.Message = message;
            this.Name = name;
            this.Sex = sex;
            this.UserId = userId;
            this.Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
        }
    }
}
