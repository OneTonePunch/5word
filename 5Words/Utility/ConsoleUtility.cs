using _5Words.Models;

namespace _5Words.Utility
{
    public class ConsoleUtility
    {
        private Random _random;
        private WordsStorage _storage;
        private static string _lineString = "**********************************************";
        public ConsoleUtility(WordsStorage storage)
        {
            _random = new Random();
            _storage = storage;
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
            ShowWords(new List<string> { nonRepeatLetters[randomIndex] });
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
                ShowWords(wordsByChars);
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
                ShowWords(wordsByNonChars);
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
                ShowWords(filteredWords);
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
                ShowWords(wordsByTemplate);
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
                ShowWords(filteredWords);
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
                ShowWords(filteredWords);
        }
        public void Clear()
        {
            Console.Clear();
        }


        private void ShowWords(List<string> words, string description = null, bool needPause = true)
        {
            Console.WriteLine(_lineString);

            if (!string.IsNullOrEmpty(description))
                Console.WriteLine(description);

            var index = 1;
            foreach (var word in words)
            {
                Console.WriteLine($"{index}){word}");
                index++;
            }

            if (needPause)
                Console.ReadLine();
        }

        public static void ProgramStart()
        {
            Console.WriteLine(_lineString);
            Console.WriteLine("Программа 5Слов запущена");
        }

        public static int GetWordLength()
        {
            Console.WriteLine(_lineString);
            Console.WriteLine("Введите длинну искомого слова");
            var result = Convert.ToInt32(Console.ReadLine());
            return result;
        }

        public static void StorageInfo(int charCount, int wordsByCount, int wordsNonRepeatLetters)
        {
            Console.WriteLine(_lineString);
            Console.WriteLine($"Хранилище содержит {wordsByCount} слов длиной {charCount}");
            Console.WriteLine($"Хранилище содержит {wordsNonRepeatLetters} слов длиной {charCount} с неповторяющимися буквами");
        }

        public static void ShowMenu()
        {
            Console.WriteLine(_lineString);
            Console.WriteLine("Введите команду");
            Console.WriteLine("1) Выдать рандомное слово с неповторяющимися буквами");
            Console.WriteLine("2) Получить слова содержащие буквы");
            Console.WriteLine("3) Получить слова НЕ содержащие буквы");
            Console.WriteLine("4) Получить слова содержащие буквы и Не содержащие другие буквы");
            Console.WriteLine("5) Получить слова по шаблону(пример аб_а_)");
            Console.WriteLine("6) Получить слова содержащие буквы, не содержащие другие буквы, шаблон");
            Console.WriteLine("7) Получить слова содержащие буквы, не содержащие другие буквы, по шаблону и анти-шаблону");
            Console.WriteLine("8) Очистить");
        }

        public static int GetMenuItem()
        {
            ShowMenu();
            var result = Convert.ToInt32(Console.ReadLine());
            return result;
        }
    }
}
