using _5Words.Models;

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
                ContainsAndNonContainsCharsAndTemplateAndAntiTemplate();
            else if (n == 8)
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

            var filter = new Filter
            {
                EnableContains = true,
                Contains = chars
            };
        
            var wordsByChars = _storage.Filtrate(filter);
            if (wordsByChars.Count == 0)
                Console.WriteLine("В словаре нет слов содержащих все эти буквы");
            else
                _consoleView.ShowWords(wordsByChars);
        }
        public void NonContainChars()
        {

            Console.WriteLine("Введите буквы которые не должно содержать слово(пример:абе)");
            var chars = Console.ReadLine();

            var filter = new Filter
            {
                EnableNonContains = true,
                NonContains = chars
            };

            var wordsByNonChars = _storage.Filtrate(filter);
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

            var filter = new Filter
            {
                EnableContains = true,
                EnableNonContains = true,
                Contains = charsContains,
                NonContains = charsNonContains
            };

            var filteredWords = _storage.Filtrate(filter);

            if (filteredWords.Count == 0)
                Console.WriteLine("В словаре нет таких слов");
            else
                _consoleView.ShowWords(filteredWords);
        }
        public void WordsByTemplate()
        {
            Console.WriteLine("Введите шаблон (пример:аб_а_)");
            var template = Console.ReadLine();

            var filter = new Filter
            {
                EnableByTemplate = true,
                Template = template
            };

            var wordsByTemplate = _storage.Filtrate(filter);
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

            var filter = new Filter
            {
                EnableContains = true,
                EnableNonContains = true,
                EnableByTemplate = true,
                Contains = charsContains,
                NonContains = charsNonContains,
                Template = template
            };

            var filteredWords = _storage.Filtrate(filter);
            if (filteredWords.Count == 0)
                Console.WriteLine("В словаре нет таких слов");
            else
                _consoleView.ShowWords(filteredWords);
        }

        public void ContainsAndNonContainsCharsAndTemplateAndAntiTemplate()
        {
            Console.WriteLine("Введите буквы которые должно содержать слово(пример:абе)");
            var charsContains = Console.ReadLine();
            Console.WriteLine("Введите буквы которые не должно содержать слово(пример:абе)");
            var charsNonContains = Console.ReadLine();
            Console.WriteLine("Введите шаблон (пример:аб_а_)");
            var template = Console.ReadLine();
            Console.WriteLine("Введите анти-шаблон (пример:__г_д)");
            var antiTemplate = Console.ReadLine();

            var filter = new Filter
            {
                EnableContains = true,
                EnableNonContains = true,
                EnableByTemplate = true,
                EnableByAntiTemplate = true,
                Contains = charsContains,
                NonContains = charsNonContains,
                Template = template,
                AntiTemplate = antiTemplate
            };

            var filteredWords = _storage.Filtrate(filter);
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
