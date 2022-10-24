using _5Words.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace _5Words.Commands
{
    /// <summary>Кастомная комманда в случае если нужная команда не нашлась</summary>
    public class NotFindCommand : IBotCommand
    {
        public Task Run(ITelegramBotClient botClient, Message message)
        {
            throw new NotImplementedException();
        }
    }
}
