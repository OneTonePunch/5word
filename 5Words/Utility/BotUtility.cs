using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Models;
using MyApp;
using Telegram.Bot.Types.ReplyMarkups;
using System.Reflection;
using Newtonsoft.Json;

namespace _5Words.Utility
{
    public static class BotUtility
    {
        private static Random _random = new Random();

        public static async Task SendHi(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.Greeting);
        }
        public static async Task SendHelp(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.Help);
            return;
        }

        public static async Task SendInfo(long chatId, ITelegramBotClient botClient, Message message)
        {
            Session session = null;
            if (!SessionStorage.Storage.TryGetValue(chatId, out session)||session.Params.Length<0)
            {
                await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantFindSession);
                return;
            }
                
            var info =$"Длина:{session.Params.Length} \n"; 
            info+= $"Содержит:{session.Params.Filter.Contains} \n";
            info+= $"Несодержит:{session.Params.Filter.NonContains} \n";
            info+=$"Шаблон:{session.Params.Filter.Template} \n";
            info+=$"Антишаблон:{session.Params.Filter.AntiTemplate} \n";
            await botClient.SendTextMessageAsync(message.Chat, info);
            return;
        }

        public static async Task Find(long chatId, ITelegramBotClient botClient, Message message)
        {
            Session session = null;
            if (!SessionStorage.Storage.TryGetValue(chatId, out session)||session.Params.Length<0)
            {
                await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantRecognize);
                return;
            }
                
            

            try
            {
                var wstorage = new WordsStorage(session.Params.Length, Program.Configuration.DictionaryFileName, Program.Configuration.TemplateChar.FirstOrDefault());
                var result = wstorage.Filtrate(session.Params.Filter);
                if (result == null || result.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantFind);
                    return;
                }
                else
                {
                    var wordsLimitCollection = result.Count() > 100 ? result.Take(100) : result;
                    var responseText = string.Concat(wordsLimitCollection.Select(x => $"{result.IndexOf(x) + 1}]{x}{Environment.NewLine}"));
                    await botClient.SendTextMessageAsync(message.Chat, responseText);
                    return;
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantRecognize);
                return;
            }
        }

        public static async Task SendRandom(long chatId, ITelegramBotClient botClient, Message message)
        {
            Session session = null;
            if (!SessionStorage.Storage.TryGetValue(chatId, out session)||session.Params.Length<0)
            {
                await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantRecognize);
                return;
            }
                

            try
            {
                var wstorage = new WordsStorage(session.Params.Length, Program.Configuration.DictionaryFileName, Program.Configuration.TemplateChar.FirstOrDefault());
                var nonRepeatLetters = wstorage.FindNonReapeatingLettersWords();
                int randomIndex = _random.Next(0, nonRepeatLetters.Count - 1);
                var result = new List<string> { nonRepeatLetters[randomIndex] };
                if (result == null || result.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantFind);
                    return;
                }
                else
                {
                    var responseText = string.Concat(result.Select(x => $"{x}{Environment.NewLine}"));
                    await botClient.SendTextMessageAsync(message.Chat, responseText);
                    return;
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantRecognize);
                return;
            }
        }
        
        public static async Task SetCommandMenu(ITelegramBotClient bot)
        {
            var commandsProps = Program.Configuration.Commands.GetType().GetProperties();
            var botCommandsMenuCollection = new List<BotCommand> {
                new BotCommand
                {
                    Command = "/start",
                    Description = "Начало работы",
                },
                new BotCommand
                {
                    Command = "/help",
                    Description = "Помощь по поиску"
                },
                new BotCommand
                {
                    Command = "/params",
                    Description = "Текущие параметры поиска"
                },
                new BotCommand
                {
                    Command = "/random",
                    Description = "Случайное слово(обязательно должен присутствовать параметр длина)"
                },
            };
            await bot.SetMyCommandsAsync(botCommandsMenuCollection);
        }

        public static async Task UpdateSession(long chatId, ITelegramBotClient botClient, Message message, CommandType commandType)
        {
            Session session = null;
            if (!SessionStorage.Storage.TryGetValue(chatId, out session))
            {
                session = new Session(chatId, message.Chat.Username, new SessionParams{
                    Filter = new Filter()
                });
            }

            var commandText = Program.Configuration.Commands.GetValueByType(commandType);
            var valueText = message.Text.Replace(commandText,"")?.Trim();
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
            
            SessionStorage.AddOrUpdate(chatId, session);    

            await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.SessionUpdated);
        }

    }
}
