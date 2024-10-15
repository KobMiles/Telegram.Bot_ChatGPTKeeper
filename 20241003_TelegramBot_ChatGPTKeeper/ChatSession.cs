using Telegram.Bot.Types;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class ChatSession
    {
        private readonly HostBot _hostBot;
        public string CurrentUser { get; private set; } = string.Empty;

        public bool IsFree { get; private set; } = true;

        private DateTime _timeGptTaken;
        private TimeSpan _timeGptOccupy;

        public ChatSession(HostBot hostBot)
        {
            _hostBot = hostBot;
        }

        public async Task StartSession(CallbackQuery query)
        {
            if (CurrentUser == query.From.ToString())
            {
                return;
            }

            if (!IsFree)
            {
                await _hostBot.BotResponse.NotifyCannotReleaseByOtherUser(query);
                _timeGptOccupy = DateTime.Now - _timeGptTaken;
                await _hostBot.BotResponse.SendBusyChatSessionNotification(query, _timeGptOccupy.Minutes);
                return;
            }

            await _hostBot.BotResponse.AcknowledgeCallbackSelection(query);
            _timeGptTaken = DateTime.Now;
            CurrentUser = query.From.ToString();
            IsFree = false;
            await _hostBot.BotResponse.SendChatOccupiedMessage(query);
        }

        public async Task StopSession(CallbackQuery query)
        {
            if (CurrentUser == query.From.ToString())
            {
                await _hostBot.BotResponse.AcknowledgeCallbackSelection(query);

                _timeGptOccupy = DateTime.Now - _timeGptTaken;

                await _hostBot.BotResponse.SendChatReleaseNotification(query, _timeGptOccupy.Minutes);
                CurrentUser = string.Empty;
                IsFree = true;
            }

            else if (CurrentUser != query.From.ToString())
            {
                await _hostBot.BotResponse.NotifyCannotReleaseByOtherUser(query);
            }
        }

        public string IsGptFree()
        {
            return IsFree ? "\n\n\ud83d\udfe9Now GPT is free!\ud83d\udfe9.\nYou can take:"
                : $"\n\n\ud83d\udfe5Now GPT is busy\ud83d\udfe5 by {CurrentUser}!";
        }
    }
}
