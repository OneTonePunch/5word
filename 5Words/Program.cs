using _5Words;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyApp
{
    internal class Program
    {
        private static ITelegramBotClient bot = new TelegramBotClient("5738281587:AAGr5g-dWjzpq4BhC-EP8LKsXuK4qUgOcLk");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;
                var messageText = message.Text;
                var chatId = update.Message.Chat.Id;
                var userName = update.Message.Chat.Username;
                if (message.Text.ToLower() == "/start")
                {
                    await BotUtility.SendHi(botClient, message);
                    await BotUtility.SendHelp(botClient, message);
                    return;
                }
                else if (message.Text.ToLower().StartsWith("/find"))
                {
                    await BotUtility.Find(botClient, message);
                    return;

                }
                else if (message.Text.ToLower().StartsWith("/help"))
                {
                    await BotUtility.SendHelp(botClient, message);
                    return;
                }
                else if (message.Text.ToLower().StartsWith("/rnd"))
                {
                    await BotUtility.SendRandom(botClient, message);
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
                
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[] 
                {
                    UpdateType.Message,
                    UpdateType.EditedMessage
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