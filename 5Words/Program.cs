using _5Words;
using System;
using System.Runtime.InteropServices;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var consoleView = new ConsoleView();
            Random rnd = new Random();

            consoleView.ProgramStart();
            var charCount = consoleView.GetWordLength();

            var wstorage = new WordsStorage(charCount);
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