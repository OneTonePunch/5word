using _5Words.Commands.Interfaces;
using _5Words.Managers;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace _5Words.Commands
{
    /// <summary>Кастомная комманда в случае если нужная команда не нашлась</summary>
    public class NotFindCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.CantFindSession, cancellationToken: cancellationToken);
        }
    }
}
