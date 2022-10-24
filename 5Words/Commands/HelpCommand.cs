using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Commands.Interfaces;
using _5Words.Managers;

namespace _5Words.Commands
{
    /// <summary>Отправка сообщения помощи по функционалу бота</summary>
    public class HelpCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.Help, ParseMode.Html);
        }
    }
}
