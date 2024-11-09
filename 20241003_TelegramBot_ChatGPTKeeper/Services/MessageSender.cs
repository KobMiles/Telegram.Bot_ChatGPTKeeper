using _20241003_TelegramBot_ChatGPTKeeper.Core;
using _20241003_TelegramBot_ChatGPTKeeper.Messages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace _20241003_TelegramBot_ChatGPTKeeper.Services
{
    internal static class MessageSender
    {
        public static async Task SendCommandStartMessage(TelegramBotClient telegramBotClient, ChatSession chatSession, Message message)
        {
            await telegramBotClient.SendMessage(message.Chat,
            $"{ChatBotMessages.StartMessage(currentUser: message.From!.ToString())}" +
            $"{chatSession.IsGptFree()}",
            replyMarkup: ChatBotMessages.OccupyButtonMarkup,
            parseMode: ParseMode.Html,
            protectContent: true,
            replyParameters: message.MessageId);

            await telegramBotClient.DeleteMessage(message.Chat, message.MessageId);
        }

        public static async Task SendChatResetMessage(TelegramBotClient telegramBotClient ,Message message)
        {
            await telegramBotClient.SendPhoto(message.Chat,
                ChatBotMessages.GreenChatRealeseImageUrl,
                caption: ChatBotMessages.ChatGptResetMessage,
                replyMarkup: ChatBotMessages.OccupyButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true);
        }

        //public static async Task NotifyCannotReleaseByOtherUser(TelegramBotClient telegramBotClient, ChatSession chatSession, CallbackQuery query)
        //{
        //    await telegramBotClient.AnswerCallbackQuery(query.Id,
        //        $"It is already occupied by {chatSession.ActiveUser}");
        //}
    }
}
