using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class HostBot
    {
        public readonly TelegramBotClient Bot;

        public readonly ChatSession ChatSession;

        public HostBot(string apikey)
        {
            Bot = new TelegramBotClient(apikey);
            ChatSession = new ChatSession(Bot);
        }

        public void Start()
        {
            Bot.OnUpdate += OnUpdate;
            Bot.OnMessage += OnMessage;
            Bot.OnError += OnError;

            Console.WriteLine("Bot start. ID: " + Bot.BotId);
        }

        private async Task OnUpdate(Update update)
        {

            Console.WriteLine("Start UpdateHandler()");

            if (update is { CallbackQuery: { } query })
            {
                await Bot.AnswerCallbackQueryAsync(query.Id, $"You picked {query.Data}");

                Console.WriteLine($"\n\tUser {query.From} clicked on {query.Data}\n");

                if (query.Data == BotMessages.OccupyChatGptButtonText)
                {
                    await ChatSession.StartSession(query);
                }

                else if (query.Data == BotMessages.ReleaseChatGptButtonText)
                {
                    await ChatSession.StopSession(query);
                }
            }
            await Task.CompletedTask;
            Console.WriteLine("End UpdateHandler()");
        }

        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine("Start OnError() in Host");

            Console.WriteLine("Error:" + exception.Message);

            await Task.CompletedTask;
            Console.WriteLine("Stop OnError() in Host");
        }
        async Task OnMessage(Message msg, UpdateType type)
        {
            Console.WriteLine("Start OnMessage() in Host");

            Console.WriteLine($"\n \t New Message from {msg?.From?.Username ?? "Unknown user"}:" +
                              $" {msg?.Text ?? "Not a text."}\n");

            if (msg?.Text == "/start")
            {
                await Bot.SendTextMessageAsync(msg.Chat, $"{BotMessages.StartMessage(currentUser: msg.From!.ToString())}{ChatSession.IsGptFree()}",
                    replyMarkup: BotMessages.OccupyGptButton,
                    parseMode: ParseMode.Html,
                    protectContent: true,
                    replyParameters: msg.MessageId);
                await Bot.DeleteMessageAsync(msg.Chat, msg.MessageId);
            }
            
            Console.WriteLine("End OnMessage() in Host");
            await Task.CompletedTask;
        }
    }
}