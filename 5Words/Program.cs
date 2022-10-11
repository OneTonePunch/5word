﻿using _5Words;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace MyApp
{
    internal class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("5738281587:AAGr5g-dWjzpq4BhC-EP8LKsXuK4qUgOcLk");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать в поиск слов! А точнее русских существительных по фильтрам.");
                    await botClient.SendTextMessageAsync(message.Chat, "Пример поиска:");
                    await botClient.SendTextMessageAsync(message.Chat, "/find {length:\"5\", contains:\"абз\", noncontains:\"совикх\", template:\"аб_а_\", antitemplate:\"__б_з\"}");
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
                else if (message.Text.ToLower().Contains("/find"))
                {
                    try
                    {
                        var jsonText = message.Text.ToLower().Replace("/find", "").Trim();
                        var findMessage = JsonConvert.DeserializeObject<FindMessage>(jsonText);

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
                            var responseText = string.Concat(result.Select(x => $"{x}{Environment.NewLine}"));
                            await botClient.SendTextMessageAsync(message.Chat, responseText);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        await botClient.SendTextMessageAsync(message.Chat,"Упс... не удалось распознать вашу комманду");
                        return;
                    }
                    
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        //static void Main(string[] args)
        //{
        //    var consoleView = new ConsoleView();
        //    consoleView.ProgramStart();
        //    var charCount = consoleView.GetWordLength();

        //    var wstorage = new WordsStorage(charCount, "russian_nouns.txt");
        //    var nonRepeatLetters = wstorage.FindNonReapeatingLettersWords();
        //    consoleView.StorageInfo(charCount, wstorage.Storage.Count, nonRepeatLetters.Count);
        //    var commandObject = new Command(wstorage);

        //    while (true)
        //    {
        //        var command = consoleView.GetMenuItem();
        //        commandObject.Run(command);
        //    }

        //    Console.ReadLine();
        //}
    }
}