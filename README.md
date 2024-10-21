# Telegram.Bot_ChatGPTKeeper

This project is a Telegram bot built using the `Telegram.Bot` library. It includes several components for managing chat sessions, responding to messages, and handling Telegram bot hosting. The bot is designed to track and manage interactions within a chat, maintaining session data and handling responses intelligently.

## Features

- **Chat Session Management**:
    - Handles individual chat sessions using the `ChatSession` class.
    - Tracks the state of each chat and processes responses accordingly.
- **Message Handling**:
    - Processes and responds to bot commands and user messages via the `ChatBotResponseHandler`.
    - Sends messages through the `MessageSender` class.
- **Bot Hosting**:
    - Manages bot startup and configuration using the `TelegramBotHost` class.
    - Configured for deployment using Azure WebJobs.

## Project Structure

```bash
Telegram.Bot_ChatGPTKeeper/
├── 20241003_TelegramBot_ChatGPTKeeper.sln    # Solution file
├── 20241003_TelegramBot_ChatGPTKeeper/       # Main project folder
│   ├── Core/                                 # Core functionality
│   │   ├── ChatSession.cs                    # Manages individual chat sessions
│   │   ├── Program.cs                        # Main entry point for the bot
│   │   ├── TelegramBotHost.cs                # Initializes and configures the Telegram bot
│   ├── Handlers/                             # Handles bot responses
│   │   ├── ChatBotResponseHandler.cs         # Processes incoming messages and bot logic
│   ├── Messages/                             # Message-related utilities
│   │   ├── ChatBotMessages.cs                # Predefined messages sent by the bot
│   ├── Services/                             # External services
│   │   ├── MessageSender.cs                  # Sends messages through the bot
│   ├── Configurations/                       # Configuration files
│   │   ├── .filenesting.json                 # File nesting settings
│   │   ├── nuget.config                      # NuGet configuration
│   ├── Properties/                           # Additional project settings
│   │   ├── ServiceDependencies/              # Service deployment files
│   │   │   ├── profile.arm.json              # Web Deploy profile
├── .github/workflows/                        # GitHub Actions workflows
│   ├── master_chatgptkeepertelegrambot.yml   # CI/CD pipeline configuration for the bot
├── .gitignore                                # Git ignore rules
└── .gitattributes                            # Git attributes file
```

## Getting Started

### Prerequisites

- .NET 6 or later
- Telegram Bot API token (create a bot through [BotFather](https://core.telegram.org/bots#botfather))
- Visual Studio 2022 (or any compatible IDE with .NET support)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/KobMiles/Telegram.Bot_ChatGPTKeeper.git
   ```

2. Open the solution in Visual Studio:

   ```bash
   cd Telegram.Bot_ChatGPTKeeper
   ```

3. Build and run the project.

### Bot Configuration

1. Update the `TelegramBotHost.cs` file with your bot token from BotFather:

   ```csharp
   var botClient = new TelegramBotClient("YOUR_BOT_TOKEN");
   ```

2. Set up deployment configurations if hosting on Azure WebJobs or any other cloud provider.

### Usage

- The bot will start and listen for incoming messages.
- Depending on the logic in `ChatBotResponseHandler`, the bot will respond to user inputs and manage chat sessions.
- It keeps track of user states using `ChatSession` and interacts with users based on predefined messages in `ChatBotMessages`.

## Deployment

To deploy the bot using Azure WebJobs, follow the steps below:

1. Configure the necessary deployment profiles under `Properties/ServiceDependencies`.
2. Set up the Azure environment for WebJobs.

## Contributing

Contributions are welcome! Please fork this repository and open a pull request if you would like to improve the bot.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
