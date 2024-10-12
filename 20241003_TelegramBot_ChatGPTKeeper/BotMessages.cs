using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    public static class BotMessages
    {
        public static string StartMessage(string currentUser) => $"Welcome {currentUser} in ChatGPT Keeper!\n<b>Developer is:</b> @miles_ss.";
        public static string OccupyChatGptButtonText = "\ud83d\udfe5 Occupy ChatGPT \ud83d\udfe5";
        public static string ReleaseChatGptButtonText = "\ud83d\udfe9 Release ChatGPT \ud83d\udfe9";
        public static string ChatGptBusyMessage(string currentUser, int minutes) =>
            $"<b>ChatGPT is busy</b> with a nickname: {currentUser}.\nThe ChatGPT is \ud83d\udfe5busy by: {minutes} minutes.\n\n\ud83d\udfe5<b>ChatGPT is BUSY!</b>";
        public static string ChatGptOccupiedMessage(string currentUser) =>
            $"User: {currentUser} \ud83d\udfe5<b>occupy ChatGPT!</b>\n\n\ud83d\udfe5<b>ChatGPT is busy</b>";
        public static string ChatGptReleasedMessage(string currentUser, int minutes) =>
            $"\ud83d\udfe9<b>ChatGPT has been released!</b>\nStatistic:\nUser: {currentUser}\nTotal time in use: {minutes} minutes\n\n\ud83d\udfe9<b>ChatGPT is FREE!</b>";
        public static string CannotReleaseOtherUserMessage(string currentUser, int minutes) =>
            $"\n\ud83d\udfe5<b>Now ChatGPT taken by user:</b> {currentUser}.\nTotal time in use: {minutes} minutes";

        public static IReplyMarkup OccupyOrReleaseGptButton =
            new InlineKeyboardMarkup().AddButtons(BotMessages.OccupyChatGptButtonText,
                BotMessages.ReleaseChatGptButtonText);

        public static IReplyMarkup OccupyGptButton =
            new InlineKeyboardMarkup().AddButtons(BotMessages.OccupyChatGptButtonText);

        public static IReplyMarkup ReleaseGptButton =
            new InlineKeyboardMarkup().AddButtons(BotMessages.ReleaseChatGptButtonText);
    }

}
