using _5Words.Commands.Interfaces;
using _5Words.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace _5Words.Commands
{
    /// <summary>Простановка НЕ содержащихся букв в параметры сессии </summary>
    public class NonContainsCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await SessionStorage.UpdateSession(message.Chat.Id, botClient, message, CommandType.NonContains, cancellationToken);
        }
    }
}
