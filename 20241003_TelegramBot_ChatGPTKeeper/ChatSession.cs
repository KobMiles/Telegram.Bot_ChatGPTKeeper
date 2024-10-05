using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

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
            if (CurrentUser == query.From.ToString())
            {
                return;
            }
            if (!IsFree)
            {
                TimeGptOccupy = DateTime.Now - TimeGptTaken;
                await bot.SendTextMessageAsync(query.Message!.Chat,
                    BotMessages.ChatGptBusyMessage(CurrentUser, TimeGptOccupy.Minutes),
                    replyMarkup: BotMessages.ReleaseGptButton,
                    parseMode: ParseMode.Html,
                    protectContent: true,
                    replyParameters: query.Message.MessageId);
                return;
            }
            TimeGptTaken = DateTime.Now;
            CurrentUser = query.From.ToString();
            IsFree = false;
            await bot.SendTextMessageAsync(query.Message!.Chat,
                BotMessages.ChatGptOccupiedMessage(CurrentUser),
                replyMarkup: BotMessages.ReleaseGptButton,
                parseMode: ParseMode.Html,
                protectContent: true,
                replyParameters: query.Message.MessageId);
        }

        public async Task StopSession(CallbackQuery query)
        {
            if (CurrentUser == query.From.ToString())
            {
                await bot.SendTextMessageAsync(query.Message!.Chat,
                    BotMessages.ChatGptReleasedMessage(CurrentUser, TimeGptOccupy.Minutes),
                    replyMarkup: BotMessages.OccupyGptButton,
                    parseMode: ParseMode.Html,
                    protectContent: true,
                    replyParameters: query.Message.MessageId);
                CurrentUser = string.Empty;
                IsFree = true;
            }

            else if (CurrentUser != query.From.ToString())
            {
                //await bot.SendTextMessageAsync(query.Message!.Chat,
                //    $"You can't free someone else's ChatGPT.\nNow ChatGPT taken by user: {CurrentUser}." +
                //    $"\nTotal time in use: {TimeGptOccupy.Minutes} minutes",
                //    replyMarkup: BotMessages.ReleaseGptButton);
            }
        }

        public string IsGptFree()
        {
            return IsFree ? "\n\n\ud83d\udfe9Now GPT is free!\ud83d\udfe9.\nYou can take:" 
                : $"\ud83d\udfe5Now GPT is busy\ud83d\udfe5 by {CurrentUser}!";
        }
    }
}
