using _5Words.Extensions;
using _5Words.Models;
using MyApp;
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
                var chatId = update.Message.Chat.Id;
                var userName = update.Message.Chat.Username;
                if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.Start))
                {
                    await BotUtility.SendHi(botClient, message);
                    await BotUtility.SendHelp(botClient, message);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.Find))
                {
                    await BotUtility.Find(chatId, botClient, message);
                    return;

                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.Help))
                {
                    await BotUtility.SendHelp(botClient, message);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.Random))
                {
                    await BotUtility.SendRandom(chatId, botClient, message);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.Length))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.Length);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.Contains))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.Contains);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.NonContains))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.NonContains);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.Template))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.Template);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.AntiTemplate))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.AntiTemplate);
                    return;
                }
                else if (messageText.StartWithAny(ConfigurationManager.Configuration.Commands.StateInfo))
                {
                    await BotUtility.SendInfo(chatId, botClient, message);
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.CantRecognize);

            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }


    }
}
