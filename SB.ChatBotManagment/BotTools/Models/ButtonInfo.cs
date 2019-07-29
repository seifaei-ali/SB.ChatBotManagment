namespace SB.ChatBotManagment.BotTools.Models
{
    public class ButtonInfo
    {
        public string Text { get; set; }

        public int Action { get; set; }

        //چنانچه بتوان در هر ردیف چند دکمه قرار داد این عدد شماره ردیف دکمه را مشخص می کند
        public int RowNumber { get; set; }

        public ButtonInfo(string text)
        {
            this.Text = text;
            this.Action = 0;
            this.RowNumber = -1;
        }

        public ButtonInfo(string text, int rowNumber)
        {
            this.Text = text;
            this.Action = 0;
            this.RowNumber = rowNumber;
        }


        public ButtonInfo(string text, int rowNumber, int action)
        {
            this.Text = text;
            this.Action = action;
            this.RowNumber = rowNumber;
        }

    }
}
