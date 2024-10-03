using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class ChatSession
    {
        public static string CurrentUser;

        public static bool IsFree = true;

        public static void StartSession(string user)
        {
            if (!IsFree)
            {
                return;
            }
            CurrentUser = user;
            IsFree = false;
        }

        public static void StopSession()
        {
            CurrentUser = string.Empty;
            IsFree = true;
        }
    }
}
