using _5Words.Commands.Interfaces;
using _5Words.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace _5Words.Commands
{
    /// <summary>Простановка антишаблона в параметры сессии </summary>
    public class AntiTemplateCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await SessionStorage.UpdateSession(message.Chat.Id, botClient, message, CommandType.AntiTemplate, cancellationToken);
        }
    }
}
