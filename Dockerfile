# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Копируем CSPROJ и восстанавливаем зависимости
COPY 20241003_TelegramBot_ChatGPTKeeper/20241003_TelegramBot_ChatGPTKeeper.csproj ./20241003_TelegramBot_ChatGPTKeeper/
RUN dotnet restore ./20241003_TelegramBot_ChatGPTKeeper/20241003_TelegramBot_ChatGPTKeeper.csproj

# Копируем остальные файлы проекта и билдим приложение
COPY ./20241003_TelegramBot_ChatGPTKeeper ./20241003_TelegramBot_ChatGPTKeeper
WORKDIR /app/20241003_TelegramBot_ChatGPTKeeper
RUN dotnet publish -c Release -o out

# Используем минимальный образ для запуска
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=build-env /app/20241003_TelegramBot_ChatGPTKeeper/out .

# Запускаем приложение
ENTRYPOINT ["dotnet", "20241003_TelegramBot_ChatGPTKeeper.dll"]
