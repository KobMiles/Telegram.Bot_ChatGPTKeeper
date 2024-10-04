using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class ChatSession(TelegramBotClient bot)
    {
        public string CurrentUser { get; private set; } = String.Empty;

        public bool IsFree { get; private set; } = true;

        public DateTime TimeGptTaken;
        public TimeSpan TimeGptOccupy;

        public async Task StartSession(CallbackQuery query)
        {
            if (!IsFree)
            {
                TimeGptOccupy = DateTime.Now - TimeGptTaken;
                await bot.SendTextMessageAsync(query.Message!.Chat,
                    $"Sorry, the ChatGPT is busy with a nickname: {CurrentUser}.\n" +
                    $"The ChatGPT is busy by: {TimeGptOccupy.Minutes} minutes");
                return;
            }
            TimeGptTaken = DateTime.Now;
            CurrentUser = query.From.ToString();
            IsFree = false;
            await bot.SendTextMessageAsync(query.Message!.Chat,
                $"Congratulations! You occupy ChatGPT!. User: {CurrentUser}." +
                $"\n\nPlease don't delay the ChatGPT, others want to use it too!");
        }

        public async Task StopSession(CallbackQuery query)
        {
            if (CurrentUser == query.From.ToString())
            {
                await bot.SendTextMessageAsync(query.Message!.Chat,
                    $"User: {CurrentUser} released ChatGPT. Total time in use: {TimeGptOccupy.Minutes} minutes" +
                    $"\nChatGPT if FREE!" +
                    $"\n\nPlease don't delay the ChatGPT, others want to use it too!");
                CurrentUser = string.Empty;
                IsFree = true;
            }

            else if (CurrentUser != query.From.ToString())
            {
                await bot.SendTextMessageAsync(query.Message!.Chat,
                    $"You can't free someone else's ChatGPT.\nNow ChatGPT taken by user: {CurrentUser}." +
                    $"\nTotal time in use: {TimeGptOccupy.Minutes} minutes");
            }
        }
    }
}
