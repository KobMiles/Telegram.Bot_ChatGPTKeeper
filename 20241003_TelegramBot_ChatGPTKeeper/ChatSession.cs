using Telegram.Bot.Types;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class ChatSession
    {
        private readonly TelegramBotHost _telegramBotHost;
        private readonly ChatBotResponseHandler _chatBotResponseHandler;

        public string ActiveUser { get; private set; } = string.Empty;

        public bool IsSessionFree { get; private set; } = true;

        private DateTime _sessionStartTime;
        private TimeSpan _sessionDuration;

        public ChatSession(TelegramBotHost telegramBotHost, ChatBotResponseHandler chatBotResponseHandler)
        {
            _telegramBotHost = telegramBotHost;
            _chatBotResponseHandler = chatBotResponseHandler;
        }

        public async Task StartSession(CallbackQuery query)
        {
            if (ActiveUser == query.From.ToString())
            {
                return;
            }

            if (!IsSessionFree)
            {
                await _chatBotResponseHandler.NotifyCannotReleaseByOtherUser(query);
                _sessionDuration = DateTime.Now - _sessionStartTime;
                await _chatBotResponseHandler.SendBusyChatSessionNotification(query, _sessionDuration.Minutes);
                return;
            }

            await _chatBotResponseHandler.AcknowledgeCallbackSelection(query);
            _sessionStartTime = DateTime.Now;
            ActiveUser = query.From.ToString();
            IsSessionFree = false;
            await _chatBotResponseHandler.SendChatOccupiedMessage(query);
        }

        public async Task StopSession(CallbackQuery query)
        {
            if (ActiveUser == query.From.ToString())
            {
                await _chatBotResponseHandler.AcknowledgeCallbackSelection(query);

                _sessionDuration = DateTime.Now - _sessionStartTime;

                await _chatBotResponseHandler.SendChatReleaseNotification(query, _sessionDuration.Minutes);
                ActiveUser = string.Empty;
                IsSessionFree = true;
            }

            else if (ActiveUser != query.From.ToString())
            {
                await _chatBotResponseHandler.NotifyCannotReleaseByOtherUser(query);
            }
        }

        public string IsGptFree()
        {
            return IsSessionFree ? "\n\n\ud83d\udfe9Now GPT is free!\ud83d\udfe9.\nYou can take GPT:"
                : $"\n\n\ud83d\udfe5Now GPT is busy\ud83d\udfe5 by {ActiveUser}!";
        }
    }
}
