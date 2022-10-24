using _5Words.Commands.Interfaces;
using _5Words.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace _5Words.Commands
{
    /// <summary>Простановка содержащихся букв в параметры сессии </summary>
    public class ContainsCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message)
        {
            await SessionStorage.UpdateSession(message.Chat.Id, botClient, message, CommandType.Contains);
        }
    }
}
