﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _5Words
{
    public class WordsStorage
    {
        public WordsStorage(int charCount)
        {
            CharCount = charCount;
            Storage = FilterWordsLength(charCount, ReadFile());
        }
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
                    if (template[i] != '_')
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
    }
}
