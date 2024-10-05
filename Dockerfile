# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Копируем CSPROJ и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем все файлы и билдим проект
COPY . ./
RUN dotnet publish -c Release -o out

# Используем минимальный образ .NET Runtime для запуска
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Устанавливаем переменные окружения для Railway
ENV TELEGRAM_BOT_TOKEN=your_telegram_bot_token_here

# Команда для запуска приложения
ENTRYPOINT ["dotnet", "_20241003_TelegramBot_ChatGPTKeeper.dll"]
