using _5Words.Commands.Factory;
using _5Words.Extensions;
using _5Words.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace _5Words.Utility
{
    public static class BotHandler
    {
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                var messageText = message.Text.ToLower();
                var command = CommandFactory.Create(messageText);
                await command.Run(botClient, message, cancellationToken);
                //var chatId = update.Message.Chat.Id;
                //var userName = update.Message.Chat.Username;
            }
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }
    }
}
