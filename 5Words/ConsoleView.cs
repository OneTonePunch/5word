using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5Words
{
    public class ConsoleView
    {
        private string _lineString = "**********************************************";
        public void ProgramStart()
        {
            Console.WriteLine("Программа 5Слов запущена");            
        }

        public int GetWordLength()
        {
            Console.WriteLine("Введите длинну искомого слова");
            var result = Convert.ToInt32(Console.ReadLine());
            return result;
        }

        public void StorageInfo(int charCount, int wordsByCount, int wordsNonRepeatLetters )
        {
            Console.WriteLine(_lineString);
            Console.WriteLine($"Хранилище содержит {wordsByCount} слов длиной {charCount}");
            Console.WriteLine($"Хранилище содержит {wordsNonRepeatLetters} слов длиной {charCount} с неповторяющимися буквами");
            Console.WriteLine(_lineString);
        }

        public void ShowMenu()
        {
            Console.WriteLine("Введите команду");
            Console.WriteLine("1) Выдать рандомное слово с неповторяющимися буквами");
            Console.WriteLine("2) Получить слова содержащие буквы");
            Console.WriteLine("3) Получить слова НЕ содержащие буквы");
            Console.WriteLine("4) Получить слова содержащие буквы и Не содержащие другие буквы");
            Console.WriteLine("5) Получить слова по шаблону(пример аб_а_)");
            Console.WriteLine("6) Получить слова содержащие буквы, не содержащие другие буквы, шаблон");
            Console.WriteLine("7) Очистить");
        }

        public int GetMenuItem()
        {
            ShowMenu();
            var result = Convert.ToInt32(Console.ReadLine());
            return result;
        }

        public void ShowWords(List<string> words, string description = null, bool needPause = true)
        {
            Console.WriteLine(_lineString);

            if (!string.IsNullOrEmpty(description))
                Console.WriteLine(description);

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine(_lineString);

            if (needPause) 
                Console.ReadLine();
        }
    }
}
