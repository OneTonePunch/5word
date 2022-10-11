using _5Words;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleView = new ConsoleView();
            consoleView.ProgramStart();
            var charCount = consoleView.GetWordLength();

            var wstorage = new WordsStorage(charCount, "russian_nouns.txt");
            var nonRepeatLetters = wstorage.FindNonReapeatingLettersWords();
            consoleView.StorageInfo(charCount, wstorage.Storage.Count, nonRepeatLetters.Count);
            var commandObject = new Command(wstorage);

            while (true)
            {
                var command = consoleView.GetMenuItem();
                commandObject.Run(command);
            }

            Console.ReadLine();
        }
    }
}