using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Commands.Interfaces;
using _5Words.Managers;

namespace _5Words.Commands
{
    /// <summary>Отправка приветственного сообщения</summary>
    public class StartCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.Greeting, ParseMode.Html, cancellationToken:cancellationToken);
            await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.Help, ParseMode.Html,cancellationToken:cancellationToken);
        }
    }
}
