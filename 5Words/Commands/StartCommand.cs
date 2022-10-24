using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Commands.Interfaces;
using _5Words.Managers;

namespace _5Words.Commands
{
    public class StartCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.Greeting, ParseMode.Html);
            await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.Help, ParseMode.Html);
        }
    }
}
