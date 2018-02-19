using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Threading.Tasks;

namespace AnagramFinder
{
    // Исправленная версия
    class FindAnagramsRefactored
    {
        public static List<string> AnagramsParser(string wordToAnagrams, string path)
        {
            wordToAnagrams = wordToAnagrams.ToLower();

            var dictionaryList = new List<string>();
            var result = new List<string>();
            var wrongWords = new List<string>();

            dictionaryList = OpenFile(path);
             
            var wordToAnagramsLettersCount = GetLetterAndLetterCount(wordToAnagrams);

            foreach (var str in dictionaryList)
            {
                if (str.ToCharArray().Except(wordToAnagrams.ToCharArray()).Count() == 0)
                {
                    foreach (var kvPair in GetLetterAndLetterCount(str))
                        if (kvPair.Value > wordToAnagramsLettersCount[kvPair.Key])
                            wrongWords.Add(str);
                    result.Add(str);
                }
            }
            return result.Except(wrongWords).ToList();
        }

        // исправленный метод поиска количества всех букв в слове
        static Dictionary<char, int> GetLetterAndLetterCount(string str)
        {
            var result = new Dictionary<char, int>();
            foreach (var letter in str)
            {
                if(result.ContainsKey(letter))               
                    result[letter]++;               
                else               
                    result.Add(letter, 1);               
            }
            return result;
        }

        private static List<string> OpenFile(string path)
        {
            try
            {
                string fullPath = Path.GetFullPath(@"" + path);
                return File.ReadAllLines(fullPath).ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
