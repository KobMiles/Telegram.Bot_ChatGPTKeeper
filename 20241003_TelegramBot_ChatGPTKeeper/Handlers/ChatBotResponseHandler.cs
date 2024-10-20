using _20241003_TelegramBot_ChatGPTKeeper.Core;
using _20241003_TelegramBot_ChatGPTKeeper.Messages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace _20241003_TelegramBot_ChatGPTKeeper.Handlers
{
    internal class ChatBotResponseHandler
    {
        private readonly ChatSession _chatSession;
        private readonly TelegramBotClient _telegramBotClient;

        public ChatBotResponseHandler(TelegramBotClient telegramBotClient)
        {
            _chatSession = new ChatSession(this);
            _telegramBotClient = telegramBotClient;
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
                await _telegramBotClient.SendTextMessageAsync(message.Chat,
                    $"{ChatBotMessages.StartMessage(currentUser: message.From!.ToString())}" +
                    $"{_chatSession.IsGptFree()}",
                    replyMarkup: ChatBotMessages.OccupyButtonMarkup,
                    parseMode: ParseMode.Html,
                    protectContent: true,
                    replyParameters: message.MessageId);

                await _telegramBotClient.DeleteMessageAsync(message.Chat, message.MessageId);

                await Task.CompletedTask;
            }

            else if (message?.Text == "/reset" || message?.Text == "/reset@chatgptkeeper_bot")
            {
                await _chatSession.ResetSession(message);
            }
        }

        public async Task OnCallbackQueryMessage(Update update)
        {
            if (update is { CallbackQuery: { } query })
            {
                Console.WriteLine($"\n\tUser {query.From} clicked on {query.Data}\n");

                if (query.Data == ChatBotMessages.OccupyChatGptButtonText)
                {
                    await _telegramBotClient.AnswerCallbackQueryAsync(query.Id, $"You picked {query.Data}");
                    await _chatSession.StartSession(query);
                }
                else if (query.Data == ChatBotMessages.ReleaseChatGptButtonText)
                {
                    await _chatSession.StopSession(query);
                }
            }
        }

        public async Task SendChatResetNotification(Message message)
        {
            await _telegramBotClient.SendTextMessageAsync(message!.Chat, ChatBotMessages.ChatGptResetMessage(),
                replyMarkup: ChatBotMessages.OccupyButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true);
        }

        public async Task SendBusyChatSessionNotification(CallbackQuery query, int timeGptOccupyInMinutes)
        {
            await _telegramBotClient.SendTextMessageAsync(query.Message!.Chat,
                ChatBotMessages.ChatGptBusyMessage(_chatSession.ActiveUser, timeGptOccupyInMinutes),
                replyMarkup: ChatBotMessages.ReleaseButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task SendChatOccupiedMessage(CallbackQuery query)
        {
            await _telegramBotClient.SendPhotoAsync(query.Message!.Chat,
                "https://i.ibb.co/LzNDhPc/red-chat.png",
                caption: ChatBotMessages.ChatGptOccupiedMessage(_chatSession.ActiveUser),
                replyMarkup: ChatBotMessages.ReleaseButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task SendChatReleaseNotification(CallbackQuery query, int timeGptOccupyInMinutes)
        {
            await _telegramBotClient.SendPhotoAsync(query.Message!.Chat,
                "https://i.ibb.co/VNc5pfX/green-chat.png",
                caption: ChatBotMessages.ChatGptReleasedMessage(_chatSession.ActiveUser, timeGptOccupyInMinutes),
                replyMarkup: ChatBotMessages.OccupyButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task NotifyCannotReleaseByOtherUser(CallbackQuery query)
        {
            await _telegramBotClient.AnswerCallbackQueryAsync(query.Id,
                $"It is already occupied by {_chatSession.ActiveUser}");
        }

        public async Task AcknowledgeCallbackSelection(CallbackQuery query)
        {
            await _telegramBotClient.AnswerCallbackQueryAsync(query.Id, $"You picked {query.Data}");
        }
    }
}
