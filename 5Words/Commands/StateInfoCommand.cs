using _5Words.Commands.Interfaces;
using _5Words.Managers;
using _5Words.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace _5Words.Commands
{
    /// <summary>Отправка текущих параметров сессии </summary>
    public class StateInfoCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message)
        {
            Session session = null;
            if (!SessionStorage.Storage.TryGetValue(message.Chat.Id, out session) || session.Params.Length < 0)
            {
                await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.CantFindSession);
                return;
            }

            var info = $"Длина:{session.Params.Length} \n";
            info += $"Содержит:{session.Params.Filter.Contains} \n";
            info += $"Несодержит:{session.Params.Filter.NonContains} \n";
            info += $"Шаблон:{session.Params.Filter.Template} \n";
            info += $"Антишаблон:{session.Params.Filter.AntiTemplate} \n";
            await botClient.SendTextMessageAsync(message.Chat, info);
            return;
        }
    }
}
