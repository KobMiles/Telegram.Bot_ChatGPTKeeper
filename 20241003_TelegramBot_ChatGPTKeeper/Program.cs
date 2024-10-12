using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class Program
    {
        public static readonly HostBot ChatGptBot = new HostBot(Environment.GetEnvironmentVariable("TELEGRAM_API_KEY"));

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Запускаем бот
            await ChatGptBot.Start();

            Console.WriteLine("Bot is running...");

            // Удерживаем приложение активным с помощью бесконечного ожидания
            await Task.Delay(-1);  // Ожидание бесконечно (вместо Console.ReadLine())
        }
    }
}