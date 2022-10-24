using _5Words;
using _5Words.Extensions;
using _5Words.Managers;
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
        private static ITelegramBotClient bot { get; set; }

        static void Main(string[] args)
        {
            if (ConfigurationManager.Configuration.RunType == RunType.Console)
                ConsoleMode();
            else if (ConfigurationManager.Configuration.RunType == RunType.TelegramBot)
                BotMode().Wait();

        }

        static async Task BotMode()
        {
            SchedullerManager.Start();
            bot = new TelegramBotClient(ConfigurationManager.Configuration.TelegramBotApiKey);
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
                BotHandler.HandleUpdateAsync,
                BotHandler.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }

        static void ConsoleMode()
        {
            ConsoleUtility.ProgramStart();
            var charCount = ConsoleUtility.GetWordLength();
            var wstorage = new WordsStorage(charCount, ConfigurationManager.Configuration.DictionaryFileName, ConfigurationManager.Configuration.TemplateChar.FirstOrDefault());

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