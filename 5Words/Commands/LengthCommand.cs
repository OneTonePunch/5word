using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Models;
using _5Words.Commands.Interfaces;

namespace _5Words.Commands
{
    /// <summary>Простановка длины слова в параметры сессии </summary>
    public class LengthCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await SessionStorage.UpdateSession(message.Chat.Id, botClient, message, CommandType.Length, cancellationToken);
        }
    }
}
