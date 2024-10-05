using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class Program
    {
        public static readonly HostBot ChatGptBot = new HostBot("7653275610:AAGsG2rxUAc0IfLcNgn-W0K9Qw6fw_cbOC0");
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ChatGptBot.Start();

            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
            Console.WriteLine("Bot stop after enter:");
            Console.ReadLine();
        }
    }
}
