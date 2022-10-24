using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Models;
using _5Words.Commands.Interfaces;
using _5Words.Managers;

namespace _5Words.Commands
{
    /// <summary>Поиск слов по параметрам в сессии </summary>
    public class FindCommand : IBotCommand
    {
        public async Task Run(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            Session session = null;
            if (!SessionStorage.Storage.TryGetValue(message.Chat.Id, out session) || session.Params.Length < 0)
            {
                await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.CantRecognize, cancellationToken:cancellationToken);
                return;
            }

            try
            {
                var wstorage = new WordsStorage(session.Params.Length, ConfigurationManager.Configuration.DictionaryFileName, ConfigurationManager.Configuration.TemplateChar.FirstOrDefault());
                var result = wstorage.Filtrate(session.Params.Filter);
                if (result == null || result.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.CantFind, cancellationToken:cancellationToken);
                    return;
                }
                else
                {
                    var wordsLimitCollection = result.Count() > 100 ? result.Take(100) : result;
                    var responseText = string.Concat(wordsLimitCollection.Select(x => $"{result.IndexOf(x) + 1}]{x}{Environment.NewLine}"));
                    await botClient.SendTextMessageAsync(message.Chat, responseText, cancellationToken:cancellationToken);
                    return;
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.CantRecognize, cancellationToken:cancellationToken);
                return;
            }
        }
    }
}
