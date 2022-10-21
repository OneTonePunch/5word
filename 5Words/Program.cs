﻿using _5Words;
using _5Words.Models;
using _5Words.Utility;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyApp
{
    internal class Program
    {
        public static ApplicationConfiguration Configuration { get; private set; }
        private static ITelegramBotClient bot { get; set; }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                var messageText = message.Text.ToLower();
                var chatId = update.Message.Chat.Id;
                var userName = update.Message.Chat.Username;
                if (messageText.StartsWith(Configuration.Commands.Start)||messageText.StartsWith("/start"))
                {
                    await BotUtility.SendHi(botClient, message);
                    await BotUtility.SendHelp(botClient, message);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.Find))
                {
                    await BotUtility.Find(chatId, botClient, message);
                    return;

                }
                else if (messageText.StartsWith(Configuration.Commands.Help)||messageText.StartsWith("/help"))
                {
                    await BotUtility.SendHelp(botClient, message);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.Random)||messageText.StartsWith("/random"))
                {
                    await BotUtility.SendRandom(chatId, botClient, message);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.Length))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.Length);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.Contains))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.Contains);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.NonContains))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.NonContains);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.Template))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.Template);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.AntiTemplate))
                {
                    await BotUtility.UpdateSession(chatId, botClient, message, CommandType.AntiTemplate);
                    return;
                }
                else if (messageText.StartsWith(Configuration.Commands.StateInfo)||messageText.StartsWith("/params"))
                {
                    await BotUtility.SendInfo(chatId, botClient, message);
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, Configuration.Messages.CantRecognize);
                
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Configuration = ApplicationConfiguration.LoadConfiguration();
            if (Configuration.RunType == RunType.Console)
                ConsoleMode();
            else if (Configuration.RunType == RunType.TelegramBot)
                BotMode().Wait();
           
        }

        static async Task BotMode()
        {
            SchedullerManager.Start();
            bot = new TelegramBotClient(Configuration.TelegramBotApiKey);
            Console.WriteLine("Bot run " + (await bot.GetMeAsync()).FirstName);
            await BotUtility.SetCommandMenu(bot);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage,
                    UpdateType.CallbackQuery
                }

            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        static void ConsoleMode()
        {
            ConsoleUtility.ProgramStart();
            var charCount = ConsoleUtility.GetWordLength();
            var wstorage = new WordsStorage(charCount, Configuration.DictionaryFileName, Configuration.TemplateChar.FirstOrDefault());

            var nonRepeatLetters = wstorage.FindNonReapeatingLettersWords();
            ConsoleUtility.StorageInfo(charCount, wstorage.Storage.Count, nonRepeatLetters.Count);
            var commandObject = new ConsoleUtility(wstorage);

            while (true)
            {
                var command = ConsoleUtility.GetMenuItem();
                commandObject.Run(command);
            }

            Console.ReadLine();
        }
    }
}