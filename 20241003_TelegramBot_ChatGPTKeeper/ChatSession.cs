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
    internal class ChatSession
    {
        private HostBot _hostBot;
        public string CurrentUser { get; private set; } = String.Empty;

        public bool IsFree { get; private set; } = true;

        public DateTime TimeGptTaken;
        public TimeSpan TimeGptOccupy;

        public ChatSession(HostBot bot)
        {
            _hostBot = bot;
        }

        public async Task StartSession(CallbackQuery query)
        {
            if (CurrentUser == query.From.ToString())
            {
                return;
            }
            if (!IsFree)
            {
                await _hostBot.BotResponse.OnCannotReleaseOtherUserAnswer(query);
                TimeGptOccupy = DateTime.Now - TimeGptTaken;
                await _hostBot.BotResponse.OnBusyChatSessionMessage(query);
                return;
            }

            await _hostBot.BotResponse.OnPickedCallbackQuery(query);
            TimeGptTaken = DateTime.Now;
            CurrentUser = query.From.ToString();
            IsFree = false;
            await _hostBot.BotResponse.OnOccupiedChatMessage(query);
        }

        public async Task StopSession(CallbackQuery query)
        {
            if (CurrentUser == query.From.ToString())
            {
                await _hostBot.BotResponse.OnPickedCallbackQuery(query);
                await _hostBot.BotResponse.OnReleaseChatSessionMessage(query);
                CurrentUser = string.Empty;
                IsFree = true;
            }

            else if (CurrentUser != query.From.ToString())
            {
                await _hostBot.BotResponse.OnCannotReleaseOtherUserAnswer(query);
            }
        }

        public string IsGptFree()
        {
            return IsFree ? "\n\n\ud83d\udfe9Now GPT is free!\ud83d\udfe9.\nYou can take:" 
                : $"\ud83d\udfe5Now GPT is busy\ud83d\udfe5 by {CurrentUser}!";
        }
    }
}
