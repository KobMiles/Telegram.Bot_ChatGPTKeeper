using Telegram.Bot.Types;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class ChatSession
    {
        private readonly HostBot _hostBot;
        public string ActiveUser { get; private set; } = string.Empty;

        public bool IsSessionFree { get; private set; } = true;

        private DateTime _sessionStartTime;
        private TimeSpan _sessionDuration;

        public ChatSession(HostBot hostBot)
        {
            _hostBot = hostBot;
        }

        public async Task StartSession(CallbackQuery query)
        {
            if (ActiveUser == query.From.ToString())
            {
                return;
            }

            if (!IsSessionFree)
            {
                await _hostBot.BotResponse.NotifyCannotReleaseByOtherUser(query);
                _sessionDuration = DateTime.Now - _sessionStartTime;
                await _hostBot.BotResponse.SendBusyChatSessionNotification(query, _sessionDuration.Minutes);
                return;
            }

            await _hostBot.BotResponse.AcknowledgeCallbackSelection(query);
            _sessionStartTime = DateTime.Now;
            ActiveUser = query.From.ToString();
            IsSessionFree = false;
            await _hostBot.BotResponse.SendChatOccupiedMessage(query);
        }

        public async Task StopSession(CallbackQuery query)
        {
            if (ActiveUser == query.From.ToString())
            {
                await _hostBot.BotResponse.AcknowledgeCallbackSelection(query);

                _sessionDuration = DateTime.Now - _sessionStartTime;

                await _hostBot.BotResponse.SendChatReleaseNotification(query, _sessionDuration.Minutes);
                ActiveUser = string.Empty;
                IsSessionFree = true;
            }

            else if (ActiveUser != query.From.ToString())
            {
                await _hostBot.BotResponse.NotifyCannotReleaseByOtherUser(query);
            }
        }

        public string IsGptFree()
        {
            return IsSessionFree ? "\n\n\ud83d\udfe9Now GPT is free!\ud83d\udfe9.\nYou can take:"
                : $"\n\n\ud83d\udfe5Now GPT is busy\ud83d\udfe5 by {ActiveUser}!";
        }
    }
}
