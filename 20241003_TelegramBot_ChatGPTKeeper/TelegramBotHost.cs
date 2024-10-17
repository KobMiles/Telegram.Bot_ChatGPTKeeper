﻿using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class TelegramBotHost
    {
        public readonly TelegramBotClient Bot;
        public readonly ChatSession ChatSession;
        public readonly BotResponse BotResponse;

        public TelegramBotHost(string apiKey)
        {
            Bot = new TelegramBotClient(apiKey);
            ChatSession = new ChatSession(this);
            BotResponse = new BotResponse(this);
        }

        public async Task Start()
        {
            await Bot.DropPendingUpdatesAsync();

            Bot.OnUpdate += OnUpdate;
            Bot.OnMessage += OnMessage;
            Bot.OnError += OnError;

            Console.WriteLine("Bot start. ID: " + Bot.BotId);

            await Task.Delay(-1);
        }

        private async Task OnUpdate(Update update)
        {
            Console.WriteLine("Start UpdateHandler()");

            await BotResponse.OnCallbackQueryMessage(update);

            Console.WriteLine("End UpdateHandler()");
        }

        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine("Start OnError() in Host");

            await BotResponse.OnErrorConsoleMessage(exception);

            Console.WriteLine("Stop OnError() in Host");
        }

        async Task OnMessage(Message message, UpdateType type)
        {
            Console.WriteLine("Start OnMessage() in Host");

            await BotResponse.OnMessageConsoleMessage(message: message);

            await BotResponse.OnCommandStartMessage(message: message);

            Console.WriteLine("End OnMessage() in Host");
        }
    }
}