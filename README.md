# Telegram.Bot_ChatGPTKeeper

This project is a custom-built Telegram bot using the `Telegram.Bot` library, designed to facilitate shared access to a ChatGPT Premium subscription among multiple users. The primary goal of the bot is to manage the availability of the ChatGPT session, ensuring that only one user at a time can use the session, thus avoiding overlapping interactions.

## Purpose

The bot is ideal for teams or groups who share a single ChatGPT Premium account. Without proper coordination, multiple users may attempt to use the account simultaneously, leading to confusion or interrupted conversations. This bot simplifies session management by allowing users to "occupy" or "release" the session. When a session is occupied, the bot informs others that ChatGPT is currently in use, helping users coordinate and make the most out of their shared subscription.

## Key Features

- **Session Occupation and Release**: 
   - Users can "occupy" the ChatGPT session, indicating that they are actively using it.
   - Once the session is finished, users can "release" it, making it available to others.
   - The bot continuously tracks the session status and informs users if the session is free or busy.

- **Real-Time Status Updates**:
   - The bot provides real-time updates to all participants, letting everyone know the current status of the session (free or occupied).
   - This ensures transparency and better coordination between users.

- **Intuitive Interface**:
   - Simple buttons for occupying or freeing the session, allowing users to interact easily without needing to type commands.
   - Lightweight interface designed to be user-friendly.

- **Flexible Hosting**:
   - The bot is configured for deployment on cloud platforms such as Azure WebJobs, making it easy to host and maintain.

## Project Structure

```bash
Telegram.Bot_ChatGPTKeeper/
├── Core/                                 # Core functionality
│   ├── ChatSession.cs                    # Manages individual chat sessions
│   ├── Program.cs                        # Main entry point for the bot
│   ├── TelegramBotHost.cs                # Initializes and configures the Telegram bot
├── Handlers/                             # Handles bot responses
│   ├── ChatBotResponseHandler.cs         # Processes incoming messages and bot logic
├── Messages/                             # Message-related utilities
│   ├── ChatBotMessages.cs                # Predefined messages sent by the bot
├── Services/                             # External services
│   ├── MessageSender.cs                  # Sends messages through the bot
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
