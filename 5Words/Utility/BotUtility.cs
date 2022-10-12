using Telegram.Bot.Types;
using Telegram.Bot;
using _5Words.Models;
using MyApp;
using Telegram.Bot.Types.ReplyMarkups;

namespace _5Words.Utility
{
    public static class BotUtility
    {
        private static Random _random = new Random();

        private static InlineKeyboardMarkup botKeyboard { get; set; }
        static BotUtility()
        {
            botKeyboard = new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]
                {
                    new InlineKeyboardButton[]
                    {
                         InlineKeyboardButton.WithCallbackData("Помощь", Program.Configuration.Commands.Help)
                    }
                }
            );
        }
        public static async Task SendHi(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.Greeting, replyMarkup:botKeyboard);
        }
        public static async Task SendHelp(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.Help, replyMarkup: botKeyboard);
            return;
        }

        public static async Task Find(ITelegramBotClient botClient, Message message)
        {
            try
            {
                var commandString = Program.Configuration.Commands.Find;
                var jsonText = message.Text
                    .ToLower()
                    .Replace(commandString, "")
                    .Trim();
                var findMessage = ParseFindMessage(jsonText);

                var wstorage = new WordsStorage(findMessage.Length, Program.Configuration.DictionaryFileName);
                var filter = new Filter
                {
                    EnableByAntiTemplate = string.IsNullOrEmpty(findMessage.AntiTemplate) ? false : true,
                    EnableByTemplate = string.IsNullOrEmpty(findMessage.Template) ? false : true,
                    EnableContains = string.IsNullOrEmpty(findMessage.Contains) ? false : true,
                    EnableNonContains = string.IsNullOrEmpty(findMessage.NonContains) ? false : true,
                    AntiTemplate = findMessage.AntiTemplate?.ToLower(),
                    Template = findMessage.Template?.ToLower(),
                    Contains = findMessage.Contains?.ToLower(),
                    NonContains = findMessage.NonContains?.ToLower()
                };
                var result = wstorage.Filtrate(filter);
                if (result == null || result.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantFind, replyMarkup: botKeyboard);
                    return;
                }
                else
                {
                    var responseText = string.Concat(result.Select(x => $"{result.IndexOf(x) + 1}]{x}{Environment.NewLine}"));
                    await botClient.SendTextMessageAsync(message.Chat, responseText, replyMarkup: botKeyboard);
                    return;
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantRecognize, replyMarkup: botKeyboard);
                return;
            }
        }

        public static async Task SendRandom(ITelegramBotClient botClient, Message message)
        {
            try
            {
                var commandString = Program.Configuration.Commands.Random;
                var jsonText = message.Text.ToLower()
                    .Replace(commandString, "")
                    .Trim();
                var findMessage = ParseRandomMessage(jsonText);

                var wstorage = new WordsStorage(findMessage.Length, Program.Configuration.DictionaryFileName);
                var nonRepeatLetters = wstorage.FindNonReapeatingLettersWords();
                int randomIndex = _random.Next(0, nonRepeatLetters.Count - 1);
                var result = new List<string> { nonRepeatLetters[randomIndex] };
                if (result == null || result.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantFind, replyMarkup: botKeyboard);
                    return;
                }
                else
                {
                    var responseText = string.Concat(result.Select(x => $"{x}{Environment.NewLine}"));
                    await botClient.SendTextMessageAsync(message.Chat, responseText, replyMarkup: botKeyboard);
                    return;
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat, Program.Configuration.Messages.CantRecognize, replyMarkup: botKeyboard);
                return;
            }
        }

        private static Dictionary<string, string> ParseMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return new Dictionary<string, string>();

            var splited = message.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var result = new Dictionary<string, string>();
            foreach (var item in splited)
            {
                var sp = item.Split('=');
                if (sp.Length == 2)
                {
                    result.Add(sp[0].Trim().ToLower(), sp[1].Trim().ToLower());
                }
            }

            return result;
        }

        private static RandomMessage ParseRandomMessage(string message)
        {
            var splitted = ParseMessage(message);
            if (splitted != null && splitted.Count > 0)
            {
                var result = new RandomMessage();
                var lengthColumn = splitted.ContainsKey("length") ? "length" : "длина";
                if (splitted.TryGetValue(lengthColumn, out string lengthString) && int.TryParse(lengthString, out int length))
                {
                    result.Length = length;
                    return result;
                }
            }

            return null;
        }

        private static FindMessage ParseFindMessage(string message)
        {
            var splitted = ParseMessage(message);
            if (splitted != null && splitted.Count > 0)
            {
                var result = new FindMessage();
                var lengthColumn = splitted.ContainsKey("length") ? "length" : "длина";
                if (splitted.TryGetValue(lengthColumn, out string lengthString) && int.TryParse(lengthString, out int length))
                {
                    result.Length = length;
                }
                var containsColumn = splitted.ContainsKey("contains") ? "contains" : "содержит";
                if (splitted.TryGetValue(containsColumn, out string contains) && !string.IsNullOrEmpty(contains))
                {
                    result.Contains = contains;
                }
                var nonContainsColumn = splitted.ContainsKey("noncontains") ? "noncontains" : "несодержит";
                if (splitted.TryGetValue(nonContainsColumn, out string noncontains) && !string.IsNullOrEmpty(noncontains))
                {
                    result.NonContains = noncontains;
                }
                var templateColumn = splitted.ContainsKey("template") ? "template" : "шаблон";
                if (splitted.TryGetValue(templateColumn, out string template) && !string.IsNullOrEmpty(template))
                {
                    result.Template = template;
                }
                var antiTemplateColumn = splitted.ContainsKey("antitemplate") ? "antitemplate" : "антишаблон";
                if (splitted.TryGetValue(antiTemplateColumn, out string antitemplate) && !string.IsNullOrEmpty(antitemplate))
                {
                    result.AntiTemplate = antitemplate;
                }

                return result;
            }

            return null;
        }
    }
}
