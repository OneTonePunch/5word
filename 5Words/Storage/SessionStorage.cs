
using _5Words.Models;
using _5Words.Utility;
using System.Collections.Concurrent;
using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Extensions;
using _5Words.Managers;

namespace _5Words
{
    public static class SessionStorage
    {
        static SessionStorage()
        {
            Storage = new ConcurrentDictionary<long, Session>();
        }
        public static ConcurrentDictionary<long, Session> Storage { get; private set; }

        public static void AddOrUpdate(long chatId, Session session = null)
        {
            session.LastUpdate = DateTime.Now;
            if (Storage.TryGetValue(chatId, out Session storageSession))
            {
                storageSession = session;
            }
            else
            {
                Storage.TryAdd(chatId, session);
            }
        }

        public static async Task UpdateSession(long chatId, ITelegramBotClient botClient, Message message, CommandType commandType, CancellationToken cancellationToken)
        {
            Session session = null;
            if (!Storage.TryGetValue(chatId, out session))
            {
                session = new Session(chatId, message.Chat.Username, new SessionParams
                {
                    Filter = new Filter()
                });
            }

            var commandText = ConfigurationManager.Configuration.Commands.GetValueByType(commandType);
            var valueText = message.Text.ToLower().ReplaceAll(commandText)?.Trim();
            switch (commandType)
            {
                case CommandType.Length:
                    session.Params.Length = Convert.ToInt32(valueText);
                    break;
                case CommandType.Contains:
                    session.Params.Filter.Contains = valueText;
                    break;
                case CommandType.NonContains:
                    session.Params.Filter.NonContains = valueText;
                    break;
                case CommandType.Template:
                    session.Params.Filter.Template = valueText;
                    break;
                case CommandType.AntiTemplate:
                    session.Params.Filter.AntiTemplate = valueText;
                    break;
            }

            AddOrUpdate(chatId, session);

            await botClient.SendTextMessageAsync(message.Chat, $"{EmojiUtility.GetEmojiChar(EmojiType.Grining)}{ConfigurationManager.Configuration.Messages.SessionUpdated}", cancellationToken:cancellationToken);
        }
    }
}
