using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace _5Words.Commands.Interfaces
{
    public interface IBotCommand
    {
        Task Run(ITelegramBotClient botClient, Message message);
    }
}
