using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace _5Words
{
    public static class BotUtility
    {
        private static Random _random = new Random();
        public static async Task SendHi(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать в поиск слов! А точнее русских существительных по фильтрам.");
        }
        public static async Task SendHelp(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat, "Пример поиска:");
            await botClient.SendTextMessageAsync(message.Chat, "/find length=5;contains=абз;noncontains=совикх; template=аб_а_;antitemplate=__б_з");
            await botClient.SendTextMessageAsync(message.Chat, "Где:");
            await botClient.SendTextMessageAsync(message.Chat, "length - длина слова (пример 5)");
            await botClient.SendTextMessageAsync(message.Chat, "contains - символы, которые должны содержаться в слове (пример абз)");
            await botClient.SendTextMessageAsync(message.Chat, "noncontains - символы, которые НЕ должны содержаться в слове (пример совикх)");
            await botClient.SendTextMessageAsync(message.Chat, @"template - шаблон, в котором укзаны точные положения букв. _ - неизвестная буква. 
                    Пример: аб_а_. Точное положение буквы а - 1 и 4, б - 2. остальные буквы нам не известны");

            await botClient.SendTextMessageAsync(message.Chat, @"antitemplate - шаблон, в котором укзаны положения букв которые не должны быть на этом месте но присутствуют в слове. _ - неизвестная буква. 
                    Пример: __б_з.");
            return;
        }

        public static async Task Find(ITelegramBotClient botClient, Message message)
        {
            try
            {
                var jsonText = message.Text.ToLower().Replace("/find", "").Trim();
                var findMessage = ParseFindMessage(jsonText);//JsonConvert.DeserializeObject<FindMessage>(jsonText);

                var wstorage = new WordsStorage(findMessage.Length, "russian_nouns.txt");
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
                    await botClient.SendTextMessageAsync(message.Chat, "Поиск не смог найти подходящего слова... Попробуйте с другими параметрами");
                    return;
                }
                else
                {
                    var responseText = string.Concat(result.Select(x => $"{result.IndexOf(x)+1}]{x}{Environment.NewLine}"));
                    await botClient.SendTextMessageAsync(message.Chat, responseText);
                    return;
                }
            }
            catch (Exception ex)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Упс... не удалось распознать вашу комманду");
                return;
            }
        }

        public static async Task SendRandom(ITelegramBotClient botClient, Message message)
        {
            try
            {
                var jsonText = message.Text.ToLower().Replace("/rnd", "").Trim();
                var findMessage = ParseRandomMessage(jsonText);//JsonConvert.DeserializeObject<RandomMessage>(jsonText);

                var wstorage = new WordsStorage(findMessage.Length, "russian_nouns.txt");
                var nonRepeatLetters = wstorage.FindNonReapeatingLettersWords();
                int randomIndex = _random.Next(0, nonRepeatLetters.Count - 1);
                var result = new List<string> { nonRepeatLetters[randomIndex] };
                if (result == null || result.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Поиск не смог найти подходящего слова... Попробуйте с другими параметрами");
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
                await botClient.SendTextMessageAsync(message.Chat, "Упс... не удалось распознать вашу комманду");
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
                if (sp.Length==2)
                {
                    result.Add(sp[0].Trim().ToLower(), sp[1].Trim().ToLower());
                }
            }

            return result;
        }
    
        private static RandomMessage ParseRandomMessage(string message)
        {
            var splitted = ParseMessage(message);
            if (splitted!=null&&splitted.Count>0)
            {
                var result = new RandomMessage();
                if (splitted.TryGetValue("length", out string lengthString)&&Int32.TryParse(lengthString, out int length))
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
            if (splitted!=null&&splitted.Count>0)
            {
                var result = new FindMessage();
                if (splitted.TryGetValue("length", out string lengthString)&&Int32.TryParse(lengthString, out int length))
                {
                    result.Length = length;
                }
                if (splitted.TryGetValue("contains", out string contains)&&!string.IsNullOrEmpty(contains))
                {
                    result.Contains = contains;
                }
                if (splitted.TryGetValue("noncontains", out string noncontains)&&!string.IsNullOrEmpty(noncontains))
                {
                    result.NonContains = noncontains;
                }
                if (splitted.TryGetValue("template", out string template)&&!string.IsNullOrEmpty(template))
                {
                    result.Template = template;
                }
                if (splitted.TryGetValue("antitemplate", out string antitemplate)&&!string.IsNullOrEmpty(antitemplate))
                {
                    result.AntiTemplate = antitemplate;
                }

                return result;
            }

            return null;
        }
    }
}
