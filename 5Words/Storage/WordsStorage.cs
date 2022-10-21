
using _5Words.Models;

namespace _5Words
{
    public class WordsStorage
    {
        public WordsStorage(int charCount, string fileName, char templateParseChar='_')
        {
            CharCount = charCount;
            Storage = FilterWordsLength(charCount, ReadFile());
            FileName = fileName;
            TemplateParseChar = templateParseChar;
        }

        private char TemplateParseChar { get; set; }
        private int CharCount { get; set; }
        public string FileName { get; set; } = "russian_nouns.txt";

        public List<string> Storage { get; private set; }

        private List<string> FilterWordsLength(int charCount, List<string> allWords)
        {
            return allWords.Where(x => x.Length == charCount).ToList();
        }

        private List<string> ReadFile()
        {
            var dir = Directory.GetCurrentDirectory();
            var path = Path.Combine(dir, FileName);
            if (File.Exists(path))
            {
                return File.ReadAllLines(path).ToList();
            }

            return new List<string>();
        }

        public List<string> FindNonReapeatingLettersWords()
        {
            var result = new List<string>();

            foreach (var storageItem in Storage)
            {
                var flag = true ;
                foreach (var chr in storageItem)
                {
                    var sClone = storageItem.Replace(chr.ToString(), "");
                    if (sClone.Length + 1 < storageItem.Length)
                    {
                        flag = false;
                        break; ;
                    } 
                }

                if (flag)                
                    result.Add(storageItem);
            }

            return result;
        }

        public List<string> FindWordsByChars(string chars, List<string> words = null)
        {
            if (words == null)
                words = Storage;

            var result = new List<string>();

            foreach (var storageItem in words)
            {
                var flag = true;
                foreach (var chr in chars)
                {
                    if (!storageItem.Contains(chr))
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                    result.Add(storageItem);
            }

            return result;
        }

        public List<string> FindWordsByNonChars(string chars, List<string> words = null)
        {
            if (words == null)
                words = Storage;

            var result = new List<string>();

            foreach (var storageItem in words)
            {
                var flag = true;
                foreach (var chr in chars)
                {
                    if (storageItem.Contains(chr))
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                    result.Add(storageItem);
            }

            return result;
        }

        public List<string> FindWordsByTemplate(string template, List<string> words =null)
        {
            if (words == null)
                words = Storage;

            if (template.Length != CharCount)
                return new List<string>();

            var result = new List<string>();

            foreach (var storageItem in words)
            {
                var flag = true;

                for (int i = 0; i < template.Length; i++)
                {
                    if (template[i] != TemplateParseChar)
                    {
                        if (template[i] != storageItem[i])
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag)
                    result.Add(storageItem);
            }

            return result;

        }

        public List<string> FindWordsByAntiTemplate(string template, List<string> words = null)
        {
            if (words == null)
                words = Storage;

            if (template.Length != CharCount)
                return new List<string>();

            var result = new List<string>();

            foreach (var storageItem in words)
            {
                var flag = true;

                for (int i = 0; i < template.Length; i++)
                {
                    if (template[i] != TemplateParseChar)
                    {
                        if (template[i] == storageItem[i])
                        {
                            flag = false;
                            break;
                        }
                    }
                }

                if (flag)
                    result.Add(storageItem);
            }

            return result;

        }

        public List<string> Filtrate(Filter filter)
        {
            var data = Storage.ToList();
            if (filter.EnableContains)
                data = FindWordsByChars(filter.Contains, data);

            if (filter.EnableNonContains)
                data = FindWordsByNonChars(filter.NonContains, data);

            if (filter.EnableByTemplate)
                data = FindWordsByTemplate(filter.Template, data);

            if (filter.EnableByAntiTemplate)
                data = FindWordsByAntiTemplate(filter.AntiTemplate, data);

            return data;
        }
    }
}
