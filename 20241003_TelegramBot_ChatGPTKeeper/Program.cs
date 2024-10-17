namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class Program
    {
        public static readonly TelegramBotHost ChatGptBot = new TelegramBotHost(Environment.GetEnvironmentVariable("TELEGRAM_API_KEY")
                                                                        ?? throw new ArgumentNullException("TELEGRAM_API_KEY not found"));

        private static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            await ChatGptBot.Start();
        }
    }
}