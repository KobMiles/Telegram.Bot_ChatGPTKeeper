using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class BotResponse
    {
        private readonly TelegramBotHost _telegramBotHost;

        public BotResponse(TelegramBotHost telegramBotHost)
        {
            _telegramBotHost = telegramBotHost;
        }
        public async Task OnErrorConsoleMessage(Exception exception)
        {
            Console.WriteLine("Error:" + exception.Message);
            await Task.CompletedTask;
        }

        public async Task OnMessageConsoleMessage(Message message)
        {
            Console.WriteLine($"\n \t New Message from {message?.From?.Username ?? "Unknown user"}:" +
                              $" {message?.Text ?? "Not a text."}\n");
            await Task.CompletedTask;
        }

        public async Task OnCommandStartMessage(Message message)
        {
            if (message?.Text == "/start" || message?.Text == "/start@chatgptkeeper_bot")
            {
                await _telegramBotHost.Bot.SendTextMessageAsync(message.Chat,
                    $"{ ChatBotMessages.StartMessage(currentUser: message.From!.ToString()) }" +
                    $"{ _telegramBotHost.ChatSession.IsGptFree() }",
                    replyMarkup: ChatBotMessages.OccupyButtonMarkup,
                    parseMode: ParseMode.Html,
                    protectContent: true,
                    replyParameters: message.MessageId);

                await _telegramBotHost.Bot.DeleteMessageAsync(message.Chat, message.MessageId);

                await Task.CompletedTask;
            }
        }

        public async Task OnCallbackQueryMessage(Update update)
        {
            if (update is { CallbackQuery: { } query })
            {
                Console.WriteLine($"\n\tUser {query.From} clicked on {query.Data}\n");

                if (query.Data == ChatBotMessages.OccupyChatGptButtonText)
                {
                    await _telegramBotHost.Bot.AnswerCallbackQueryAsync(query.Id, $"You picked {query.Data}");
                    await _telegramBotHost.ChatSession.StartSession(query);
                }
                else if (query.Data == ChatBotMessages.ReleaseChatGptButtonText)
                {
                    await _telegramBotHost.ChatSession.StopSession(query);
                }
            }
        }

        public async Task SendBusyChatSessionNotification(CallbackQuery query, int timeGptOccupyInMinutes)
        {
            await _telegramBotHost.Bot.SendTextMessageAsync(query.Message!.Chat,
                ChatBotMessages.ChatGptBusyMessage(_telegramBotHost.ChatSession.ActiveUser, timeGptOccupyInMinutes),
                replyMarkup: ChatBotMessages.ReleaseButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task SendChatOccupiedMessage(CallbackQuery query)
        {
            await _telegramBotHost.Bot.SendPhotoAsync(query.Message!.Chat,
                "https://i.ibb.co/LzNDhPc/red-chat.png",
                caption: ChatBotMessages.ChatGptOccupiedMessage(_telegramBotHost.ChatSession.ActiveUser),
                replyMarkup: ChatBotMessages.ReleaseButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task SendChatReleaseNotification(CallbackQuery query, int timeGptOccupyInMinutes)
        {
            await _telegramBotHost.Bot.SendPhotoAsync(query.Message!.Chat,
                "https://i.ibb.co/VNc5pfX/green-chat.png",
                caption: ChatBotMessages.ChatGptReleasedMessage(_telegramBotHost.ChatSession.ActiveUser, timeGptOccupyInMinutes),
                replyMarkup: ChatBotMessages.OccupyButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task NotifyCannotReleaseByOtherUser(CallbackQuery query)
        {
            await _telegramBotHost.Bot.AnswerCallbackQueryAsync(query.Id,
                $"It is already occupied by { _telegramBotHost.ChatSession.ActiveUser }");
        }

        public async Task AcknowledgeCallbackSelection(CallbackQuery query)
        {
            await _telegramBotHost.Bot.AnswerCallbackQueryAsync(query.Id, $"You picked { query.Data }");
        }
    }
}
