using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Threading.Tasks;

namespace AnagramFinder
{   
    class AnagramFinder
    {
        public static List<string> GetAnagrams(string wordToAnagrams, List<string> dictionary)
        {
            wordToAnagrams = wordToAnagrams.ToLower();
            var result = new List<string>();
            var wordToAnagramsLettersCount = GetLetterAndLetterCount(wordToAnagrams);

            foreach (var str in dictionary)
                if (IsNotExcept(wordToAnagrams, str))
                {
                    if (IsLetterCountExcess(str, wordToAnagramsLettersCount))
                        continue;
                    result.Add(str);
                }
            return result;
        }

        static bool IsLetterCountExcess(string str, Dictionary<char, int> letterCounts)
        {
            foreach (var kvPair in GetLetterAndLetterCount(str))
                if (kvPair.Value > letterCounts[kvPair.Key])
                    return true;
            return false;
        }

        static bool IsNotExcept(string first, string second)
        {
            foreach (var chr in second)            
                if (!IsExist(chr, first))
                    return false;           
            return true;
        }

        static bool IsExist(char chr, string str)
        {
            foreach (var item in str)
                if (item == chr)
                    return true;
            return false;
        }
       
        static Dictionary<char, int> GetLetterAndLetterCount(string str)
        {
            var result = new Dictionary<char, int>();
            foreach (var letter in str)            
                if(result.ContainsKey(letter))               
                    result[letter]++;               
                else               
                    result.Add(letter, 1);                          
            return result;
        }       
    }
}