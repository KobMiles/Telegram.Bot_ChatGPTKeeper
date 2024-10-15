using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class BotResponse
    {
        private readonly HostBot _hostBot;

        public BotResponse(HostBot hostBot)
        {
            _hostBot = hostBot;
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
                await _hostBot.Bot.SendTextMessageAsync(message.Chat,
                    $"{BotMessages.StartMessage(currentUser: message.From!.ToString())}" +
                    $"{_hostBot.ChatSession.IsGptFree()}",
                    replyMarkup: BotMessages.OccupyButtonMarkup,
                    parseMode: ParseMode.Html,
                    protectContent: true,
                    replyParameters: message.MessageId);

                await _hostBot.Bot.DeleteMessageAsync(message.Chat, message.MessageId);

                await Task.CompletedTask;
            }
        }

        public async Task OnCallbackQueryMessage(Update update)
        {
            if (update is { CallbackQuery: { } query })
            {
                Console.WriteLine($"\n\tUser {query.From} clicked on {query.Data}\n");

                if (query.Data == BotMessages.OccupyChatGptButtonText)
                {
                    await _hostBot.Bot.AnswerCallbackQueryAsync(query.Id, $"You picked {query.Data}");
                    await _hostBot.ChatSession.StartSession(query);
                }
                else if (query.Data == BotMessages.ReleaseChatGptButtonText)
                {
                    await _hostBot.ChatSession.StopSession(query);
                }
            }
        }

        public async Task SendBusyChatSessionNotification(CallbackQuery query, int timeGptOccupyInMinutes)
        {
            await _hostBot.Bot.SendTextMessageAsync(query.Message!.Chat,
                BotMessages.ChatGptBusyMessage(_hostBot.ChatSession.CurrentUser, timeGptOccupyInMinutes),
                replyMarkup: BotMessages.ReleaseButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task SendChatOccupiedMessage(CallbackQuery query)
        {
            await _hostBot.Bot.SendPhotoAsync(query.Message!.Chat,
                "https://i.ibb.co/LzNDhPc/red-chat.png",
                caption: BotMessages.ChatGptOccupiedMessage(_hostBot.ChatSession.CurrentUser),
                replyMarkup: BotMessages.ReleaseButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task SendChatReleaseNotification(CallbackQuery query, int timeGptOccupyInMinutes)
        {
            await _hostBot.Bot.SendPhotoAsync(query.Message!.Chat,
                "https://i.ibb.co/VNc5pfX/green-chat.png",
                caption: BotMessages.ChatGptReleasedMessage(_hostBot.ChatSession.CurrentUser, timeGptOccupyInMinutes),
                replyMarkup: BotMessages.OccupyButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task NotifyCannotReleaseByOtherUser(CallbackQuery query)
        {
            await _hostBot.Bot.AnswerCallbackQueryAsync(query.Id, $"It is already occupied by {_hostBot.ChatSession.CurrentUser}");
        }

        public async Task AcknowledgeCallbackSelection(CallbackQuery query)
        {
            await _hostBot.Bot.AnswerCallbackQueryAsync(query.Id, $"You picked {query.Data}");
        }
    }
}
