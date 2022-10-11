using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words
{
    public class Command
    {
        private Random _random;
        private WordsStorage _storage;
        private ConsoleView _consoleView;
        public Command(WordsStorage storage)
        {
            _random = new Random();
            _storage = storage;
            _consoleView = new ConsoleView();
        }
        public void Run(int n)
        {
            if (n == 1)
                RandomWord();
            else if (n == 2)
                ContainChars();
            else if (n == 3)
                NonContainChars();
            else if (n == 4)
                ContainsAndNonContainsChars();
            else if (n == 5)
                WordsByTemplate();
            else if (n == 6)
                ContainsAndNonContainsCharsAndTemplate();
            else if (n == 7)
                Clear();

        }
        public void RandomWord()
        {
            var nonRepeatLetters = _storage.FindNonReapeatingLettersWords();
            int randomIndex = _random.Next(0, nonRepeatLetters.Count - 1);
            _consoleView.ShowWords(new List<string> { nonRepeatLetters[randomIndex] });
        }
        public void ContainChars()
        {

            Console.WriteLine("Введите буквы которые должно содержать слово(пример:абе)");
            var chars = Console.ReadLine();
            var wordsByChars = _storage.FindWordsByChars(chars);
            if (wordsByChars.Count == 0)
                Console.WriteLine("В словаре нет слов содержащих все эти буквы");
            else
                _consoleView.ShowWords(wordsByChars);
        }
        public void NonContainChars()
        {

            Console.WriteLine("Введите буквы которые не должно содержать слово(пример:абе)");
            var chars = Console.ReadLine();
            var wordsByNonChars = _storage.FindWordsByNonChars(chars);
            if (wordsByNonChars.Count == 0)
                Console.WriteLine("В словаре нет слов не содержащих все эти буквы");
            else
                _consoleView.ShowWords(wordsByNonChars);
        }
        public void ContainsAndNonContainsChars()
        {
            Console.WriteLine("Введите буквы которые должно содержать слово(пример:абе)");
            var charsContains = Console.ReadLine();
            Console.WriteLine("Введите буквы которые не должно содержать слово(пример:абе)");
            var charsNonContains = Console.ReadLine();
            var wordsByChars = _storage.FindWordsByChars(charsContains);
            var filteredWords = _storage.FindWordsByNonChars(charsNonContains, wordsByChars);

            if (filteredWords.Count == 0)
                Console.WriteLine("В словаре нет таких слов");
            else
                _consoleView.ShowWords(filteredWords);
        }
        public void WordsByTemplate()
        {
            Console.WriteLine("Введите шаблон (пример:аб_а_)");
            var template = Console.ReadLine();
            var wordsByTemplate = _storage.FindWordsByTemplate(template);
            if (wordsByTemplate.Count == 0)
                Console.WriteLine("В словаре нет таких слов");
            else
                _consoleView.ShowWords(wordsByTemplate);
        }
        public void ContainsAndNonContainsCharsAndTemplate()
        {
            Console.WriteLine("Введите буквы которые должно содержать слово(пример:абе)");
            var charsContains = Console.ReadLine();
            Console.WriteLine("Введите буквы которые не должно содержать слово(пример:абе)");
            var charsNonContains = Console.ReadLine();
            Console.WriteLine("Введите шаблон (пример:аб_а_)");
            var template = Console.ReadLine();
            var wordsByChars = _storage.FindWordsByChars(charsContains);
            var wordsByNonCharsInWordsByChars = _storage.FindWordsByNonChars(charsNonContains, wordsByChars);
            var filteredWords = _storage.FindWordsByTemplate(template, wordsByNonCharsInWordsByChars);
            if (filteredWords.Count == 0)
                Console.WriteLine("В словаре нет таких слов");
            else
                _consoleView.ShowWords(filteredWords);
        }
        public void Clear()
        {
            Console.Clear();
        }
    }
}
