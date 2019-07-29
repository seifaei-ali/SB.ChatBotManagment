using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SB.ChatBotManagment.BotManagment;
using SB.ChatBotManagment.Runner;

namespace SB.ChatBotManagment.Test
{
    public partial class Form1 : Form
    {
        private SB.ChatBotManagment.Runner.WindowsRunner tlRunner;
        private SB.ChatBotManagment.Runner.WindowsRunner baleRunner;

        public Form1()
        {
            InitializeComponent();

            tlRunner = new WindowsRunner(new BotInfo(), MessengerType.Telegram);
            baleRunner = new WindowsRunner(new BotInfo(), MessengerType.Bale);

            tlRunner.LogEvent += TlRunner_LogEvent;
            baleRunner.LogEvent += BaleRunner_LogEvent;
        }

        private void BaleRunner_LogEvent(Helper.LogEntity logEntity)
        {
            AddLog(this.logBale, logEntity);
        }

        private void TlRunner_LogEvent(Helper.LogEntity logEntity)
        {
            AddLog(this.logTelegram, logEntity);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tlRunner.Start();
            baleRunner.Start();

        }

        private void AddLog(ListBox listBox, Helper.LogEntity logEntity)
        {
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(new Action(() => listBox.Items.Insert(0, logEntity.Message)));
            }
            else
            {
                listBox.Items.Insert(0, logEntity.Message);
            }
        }

    }
}
