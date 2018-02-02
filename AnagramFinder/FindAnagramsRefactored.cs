using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Threading.Tasks;

namespace AnagramFinder
{
    // Исправленная версия без пояснительных комментариев

    class FindAnagramRefactored
    {
        public static List<string> AnagramsParser(string wordToAnagram, string path)
        {
            string word = wordToAnagram.ToLower();

            var dictionaryList = new List<string>();
            var result = new List<string>();
            var wrongWords = new List<string>();

            dictionaryList = OpenFile(path);

            foreach (var str in dictionaryList)
            {
                if (str.ToCharArray().Except(word.ToCharArray()).Count() == 0)
                {
                    foreach (var key in GetLetterAndLetterCount(str))
                        if (key.Value > GetLetterAndLetterCount(word)[key.Key])
                            wrongWords.Add(str);
                    result.Add(str);
                }
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
