using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SB.ChatBotManagment.BotManagment;
using SB.ChatBotManagment.BotTools;
using SB.ChatBotManagment.BotTools.Models;
using SB.ChatBotManagment.Helper;
using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace SB.ChatBotManagment.Runner
{
    public class WindowsRunner
    {
        public TelegramBotClient _tlBotClient { get; private set; }

        public event LogEntity.LogEventDelegate LogEvent;
        public BotManagment.MessageProcess MessageProcess { get; set; }

        public MessengerType MessengerType { get; private set; }

        public WindowsRunner(IBotInfo botInfo, MessengerType messengerType)
        {
            try
            {
                this.MessengerType = messengerType;
                this.MessageProcess = new MessageProcess(botInfo); 
                switch (messengerType)
                {
                    case MessengerType.Bale:
                        _tlBotClient = new TelegramBotClient(botInfo.Token_Bale);
                        break;
                    case MessengerType.Telegram:
                        _tlBotClient = new TelegramBotClient(botInfo.Token_Telegram, baseUrl: "https://api.telegram.org/bot");
                        break;
                        default:
                        throw new NotImplementedException("WindowsRunner is writed only for telegram and bale");
                }
                _tlBotClient.OnUpdate += TlBotClient_OnUpdate;
            }
            catch (Exception e)
            {
                OnLogEvent(LogEntity.GetLogEntity(e));
            }

        }

        public void Start()
        {
            _tlBotClient.StartReceiving();
            OnLogEvent(LogEntity.GetLogEntity("Start Reciving"));

        }

        private async void TlBotClient_OnUpdate(object sender, Telegram.Bot.Args.UpdateEventArgs e)
        {
            try
            {
                string responseText;
                long chatId;
                string userName;
                int responseMessageId = 0;

                if (e.Update.CallbackQuery != null)
                {
                    responseText = e.Update.CallbackQuery.Data;
                    chatId = e.Update.CallbackQuery.From.Id;
                    userName = e.Update.CallbackQuery.From.Username;
                }
                else
                {
                    responseText = e.Update.Message.Text;
                    chatId = e.Update.Message.Chat.Id;
                    userName = e.Update.Message.Chat.Username;
                    responseMessageId = e.Update.Message.MessageId;
                }

                var response = this.MessageProcess.TextMessageProcess(responseText, chatId.ToString(), userName, 2, MessengerType);


                if (!string.IsNullOrEmpty(response.StartMessageText))
                {
                    await _tlBotClient.SendTextMessageAsync(chatId, response.StartMessageText);
                }



                if (!string.IsNullOrEmpty(response.FilePath))
                {
                    using (var fileStream = new System.IO.FileStream(response.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var file = new InputOnlineFile(fileStream, response.FileName);
                        switch (response.FileType)
                        {
                            case FileType.Audio:
                                await _tlBotClient.SendChatActionAsync(chatId, Telegram.Bot.Types.Enums.ChatAction.UploadAudio);
                                await _tlBotClient.SendAudioAsync(chatId, file);
                                break;
                            case FileType.Photo:
                                await _tlBotClient.SendChatActionAsync(chatId, Telegram.Bot.Types.Enums.ChatAction.UploadPhoto);
                                await _tlBotClient.SendPhotoAsync(chatId, file);
                                break;
                            case FileType.Video:
                                await _tlBotClient.SendChatActionAsync(chatId, Telegram.Bot.Types.Enums.ChatAction.UploadVideo);
                                await _tlBotClient.SendVideoAsync(chatId, file);
                                break;
                            default:
                                await _tlBotClient.SendChatActionAsync(chatId, Telegram.Bot.Types.Enums.ChatAction.UploadDocument);
                                await _tlBotClient.SendDocumentAsync(chatId, file);
                                break;
                        }
                    }
                }


                if (response.ResponseType == ResponseType.IsReply)
                {
                    await _tlBotClient.SendTextMessageAsync(chatId, response.Text, replyToMessageId: responseMessageId);
                }
                else if (response.ResponseType == ResponseType.TemplateSendMessage
                        && response.Buttons.Count > 0)
                {

                    if (this.MessengerType == MessengerType.Bale)
                    {
                        await _tlBotClient.SendTextMessageAsync(chatId, response.Text, replyMarkup: new ReplyKeyboardMarkup(GetButtons(response.Buttons)));
                    }
                    else
                    {
                        await _tlBotClient.SendTextMessageAsync(chatId, response.Text, replyMarkup: new InlineKeyboardMarkup(GetInlineButtons(response.Buttons)));
                    }
                }
                else
                {
                    await _tlBotClient.SendTextMessageAsync(chatId, response.Text);
                }

                OnLogEvent(LogEntity.GetLogEntity(userName + " responsed" + "    " +
                                        response.Text + "    " +
                                        response.ResponseType + "    " +
                                        response.Buttons.Count.ToString() ?? "0"));

            }
            catch (Exception ex)
            {
                OnLogEvent(LogEntity.GetLogEntity(ex));
            }

        }


        private static IEnumerable<IEnumerable<KeyboardButton>> GetButtons(List<ButtonInfo> lstButtons)
        {
            lstButtons = lstButtons.OrderBy(p => p.RowNumber).ToList();
            List<List<KeyboardButton>> re = new List<List<KeyboardButton>>();

            int lastRowNumber = -1;
            foreach (ButtonInfo button in lstButtons)
            {
                if (button.RowNumber == -1
                    || button.RowNumber != lastRowNumber)
                {
                    List<KeyboardButton> lst = new List<KeyboardButton>();
                    lst.Add(new KeyboardButton(button.Text));
                    re.Add(lst);
                }
                else
                {
                    re[re.Count - 1].Add(new KeyboardButton(button.Text));
                }
                lastRowNumber = button.RowNumber;
            }

            return re;
        }

        private static IEnumerable<IEnumerable<InlineKeyboardButton>> GetInlineButtons(List<ButtonInfo> lstButtons)
        {
            lstButtons = lstButtons.OrderBy(p => p.RowNumber).ToList();
            List<List<InlineKeyboardButton>> re = new List<List<InlineKeyboardButton>>();

            int lastRowNumber = -1;
            foreach (ButtonInfo button in lstButtons)
            {
                if (button.RowNumber == -1
                    || button.RowNumber != lastRowNumber)
                {
                    List<InlineKeyboardButton> lst = new List<InlineKeyboardButton>();
                    lst.Add(new InlineKeyboardButton() { Text = button.Text, CallbackData = button.Text });
                    re.Add(lst);
                }
                else
                {
                    re[re.Count - 1].Add(new InlineKeyboardButton() { Text = button.Text, CallbackData = button.Text });
                }
                lastRowNumber = button.RowNumber;
            }

            return re;
        }

        private void OnLogEvent(LogEntity logEntity)
        {
            LogEvent?.Invoke(logEntity);
        }

    }
}
