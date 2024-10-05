using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;

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
            // Запускаем Polling для получения обновлений
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Получаем все типы обновлений
            };

            Bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions
            );

            Console.WriteLine("Bot is running. Press any key to exit...");
            Console.ReadLine(); // Ожидание завершения работы
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                Console.WriteLine($"Received message from {update.Message.Chat.Username}: {update.Message.Text}");

                if (update.Message.Text == "/start")
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: "Welcome to the bot!",
                        cancellationToken: cancellationToken
                    );
                }
            }
            else if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery?.Data != null)
            {
                // Обрабатываем нажатие кнопок
                var query = update.CallbackQuery;
                if (query.Data == BotMessages.OccupyChatGptButtonText)
                {
                    await ChatSession.StartSession(query);
                }
                else if (query.Data == BotMessages.ReleaseChatGptButtonText)
                {
                    await ChatSession.StopSession(query);
                }
            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
