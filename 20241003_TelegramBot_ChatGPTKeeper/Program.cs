namespace _20241003_TelegramBot_ChatGPTKeeper
{
    internal class Program
    {
        private static readonly TelegramBotHost ChatGptBot = new (Environment.GetEnvironmentVariable("TELEGRAM_API_KEY")
                                                                  ?? throw new ArgumentNullException("TELEGRAM_API_KEY not found"));

        private static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            await ChatGptBot.Start();
        }
    }
}