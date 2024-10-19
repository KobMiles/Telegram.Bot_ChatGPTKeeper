using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class TelegramBotHost
    {
        private readonly TelegramBotClient _bot;
        private readonly ChatBotResponseHandler _responseHandler;

        public TelegramBotHost(string apiKey)
        {
            _bot = new TelegramBotClient(apiKey);
            _responseHandler = new ChatBotResponseHandler(_bot);
        }

        public async Task Start()
        {
            await _bot.DropPendingUpdatesAsync();

            _bot.OnUpdate += OnUpdate;
            _bot.OnMessage += OnMessage;
            _bot.OnError += OnError;

            Console.WriteLine("Bot start. ID: " + _bot.BotId);

            await Task.Delay(-1);
        }

        private async Task OnUpdate(Update update)
        {
            Console.WriteLine("Start UpdateHandler()");

            await _responseHandler.OnCallbackQueryMessage(update);

            Console.WriteLine("End UpdateHandler()");
        }

        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine("Start OnError() in Host");

            await _responseHandler.OnErrorConsoleMessage(exception);

            Console.WriteLine("Stop OnError() in Host");
        }

        private async Task OnMessage(Message message, UpdateType type)
        {
            Console.WriteLine("Start OnMessage() in Host");

            await _responseHandler.OnMessageConsoleMessage(message: message);

            await _responseHandler.OnCommandStartMessage(message: message);

            Console.WriteLine("End OnMessage() in Host");
        }
    }
}