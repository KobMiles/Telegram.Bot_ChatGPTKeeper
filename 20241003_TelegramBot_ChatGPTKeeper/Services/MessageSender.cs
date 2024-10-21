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
            await telegramBotClient.SendTextMessageAsync(message.Chat,
            $"{ChatBotMessages.StartMessage(currentUser: message.From!.ToString())}" +
            $"{chatSession.IsGptFree()}",
            replyMarkup: ChatBotMessages.OccupyButtonMarkup,
            parseMode: ParseMode.Html,
            protectContent: true,
            replyParameters: message.MessageId);

            await telegramBotClient.DeleteMessageAsync(message.Chat, message.MessageId);
        }

        public static async Task SendChatResetMessage(TelegramBotClient telegramBotClient ,Message message)
        {
            await telegramBotClient.SendPhotoAsync(message.Chat,
                "https://i.ibb.co/VNc5pfX/green-chat.png",
                caption: ChatBotMessages.ChatGptResetMessage,
                replyMarkup: ChatBotMessages.OccupyButtonMarkup,
                parseMode: ParseMode.Html,
                protectContent: true);
        }
    }
}
