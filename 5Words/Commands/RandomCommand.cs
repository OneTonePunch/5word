using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Models;
using _5Words.Utility;
using _5Words.Commands.Interfaces;
using _5Words.Managers;

namespace _5Words.Commands
{
    /// <summary>Отправка случайного слова в зависимости от параметра длины слова </summary>
    public class RandomCommand : IBotCommand
    {
        private static Random _random = new Random();
        public async Task Run(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            Session session = null;
            if (!SessionStorage.Storage.TryGetValue(message.Chat.Id, out session) || session.Params.Length < 0)
            {
                await botClient.SendTextMessageAsync(message.Chat, $"{EmojiUtility.GetEmojiChar(EmojiType.Disappointed)}{ConfigurationManager.Configuration.Messages.CantRecognize}", cancellationToken:cancellationToken);
                return;
            }


            try
            {
                var wstorage = new WordsStorage(session.Params.Length, ConfigurationManager.Configuration.DictionaryFileName, ConfigurationManager.Configuration.TemplateChar.FirstOrDefault());
                var nonRepeatLetters = wstorage.FindNonReapeatingLettersWords();
                int randomIndex = _random.Next(0, nonRepeatLetters.Count - 1);
                var result = new List<string> { nonRepeatLetters[randomIndex] };
                if (result == null || result.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, ConfigurationManager.Configuration.Messages.CantFind, cancellationToken:cancellationToken);
                    return;
                }
                else
                {
                    var responseText = string.Concat(result.Select(x => $"{x}{Environment.NewLine}"));
                    await botClient.SendTextMessageAsync(message.Chat, responseText,cancellationToken:cancellationToken);
                    return;
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat, $"{EmojiUtility.GetEmojiChar(EmojiType.Disappointed)}{ConfigurationManager.Configuration.Messages.CantRecognize}", cancellationToken:cancellationToken);
                return;
            }
        }
    }
}
