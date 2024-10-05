using Telegram.Bot;

namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class Program
    {
        public static HostBot? ChatGptBot;

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Читаем API токен из переменной окружения
            string? botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
            if (string.IsNullOrEmpty(botToken))
            {
                Console.WriteLine("Error: TELEGRAM_BOT_TOKEN environment variable is not set.");
                return; // Останавливаем программу, если токен не задан
            }

            // Инициализируем бота с API токеном
            ChatGptBot = new HostBot(botToken);
            ChatGptBot.Start();

            Console.WriteLine("Bot is running. Press Enter to stop...");
            Console.ReadLine(); // Ждем ввода для завершения программы
        }
    }
}