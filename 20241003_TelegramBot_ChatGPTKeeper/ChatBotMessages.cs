using Telegram.Bot.Types.ReplyMarkups;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    public static class ChatBotMessages
    {
        private const string DeveloperInfo = "<b>Developer is:</b> @miles_ss.";
        private const string BusyIndicator = "\ud83d\udfe5";
        private const string FreeIndicator = "\ud83d\udfe9";
        public static string StartMessage(string currentUser) => $"Welcome {currentUser} in ChatGPT Keeper!\n<b>Developer is:</b> {DeveloperInfo}";
        public const string OccupyChatGptButtonText = $"{BusyIndicator} Occupy ChatGPT {BusyIndicator}";
        public const string ReleaseChatGptButtonText = $"{FreeIndicator} Release ChatGPT {FreeIndicator}";
        public static string ChatGptBusyMessage(string currentUser, int minutes) =>
            $"<b>ChatGPT is busy</b> with a nickname: {currentUser}.\nThe ChatGPT is {BusyIndicator}busy by: {minutes} minutes." +
            $"\n\n{BusyIndicator}<b>ChatGPT is BUSY!</b>";
        public static string ChatGptOccupiedMessage(string currentUser) =>
            $"User: {currentUser} {BusyIndicator}<b>occupy ChatGPT!</b>\n\n{BusyIndicator}<b>ChatGPT is busy</b>";
        public static string ChatGptReleasedMessage(string currentUser, int minutes) =>
            $"{FreeIndicator}<b>ChatGPT has been released!</b>\n<b>Statistic:</b>" +
            $"\nUser: {currentUser}\nTotal time in use: {minutes} minutes\n\n{FreeIndicator}<b>ChatGPT is FREE!</b>";

        public static string ChatGptResetMessage() =>
            $"{FreeIndicator}<b>ChatGPT has been reset!</b>\n<b>Statistic:</b>" +
            $"\n\n{FreeIndicator}<b>ChatGPT is FREE!</b>";

        public static IReplyMarkup OccupyButtonMarkup =>
            new InlineKeyboardMarkup().AddButtons(OccupyChatGptButtonText);

        public static IReplyMarkup ReleaseButtonMarkup =>
            new InlineKeyboardMarkup().AddButtons(ReleaseChatGptButtonText);
    }

}
