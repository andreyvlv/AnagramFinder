using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Threading.Tasks;

namespace AnagramFinder
{
    // Исправленная версия без пояснительных комментариев

    class FingAnagramRefactored
    {
        public static List<string> AnagramsParser(string wordToAnagram, string path)
        {
            string word = wordToAnagram.ToLower();

            var dictionaryList = new List<string>();
            var result = new List<string>();
            var wrongWords = new List<string>();

            dictionaryList = OpenFile(path);

            foreach (string str in dictionaryList)
                if (str.ToCharArray().Except(word.ToCharArray()).Count() == 0)
                    result.Add(str);

            foreach (var str in result)
            {
                foreach (KeyValuePair<char, int> key in GetLetterAndLetterCount(str))
                    foreach (KeyValuePair<char, int> key1 in GetLetterAndLetterCount(word))
                        if ((key.Key == key1.Key) && (key.Value > key1.Value))
                            wrongWords.Add(str);
            }
            return result.Except(wrongWords).ToList();
        }

        static Dictionary<char, int> GetLetterAndLetterCount(string str)
        {
            var result = new Dictionary<char, int>();
            foreach (var letter in str.Distinct().ToArray())
            {
                var count = str.Count(chr => chr == letter);
                result.Add(letter, count);
            }
            return result;
        }


        private static List<string> OpenFile(string path)
        {
            IList<string> wordList = new List<string>();
            try
            {
                string fullPath = Path.GetFullPath(@"" + path);
                string[] dictionary = File.ReadAllLines(fullPath);
                wordList = dictionary.ToList();
                return wordList.ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
