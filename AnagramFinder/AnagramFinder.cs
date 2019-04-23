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
            var result = new List<string>();
            var wordToAnagramsLettersCount = GetLetterAndLetterCount(wordToAnagrams);
           
            for (int i = 0; i < dictionary.Count; i++)           
                if (dictionary[i].Length <= wordToAnagrams.Length 
                    && !HasExcessLetters(wordToAnagrams, dictionary[i]) 
                    && !IsLetterCountExcess(dictionary[i], wordToAnagramsLettersCount))
                    result.Add(dictionary[i]);
                else
                    continue;            
            return result;
        }

        static bool IsLetterCountExcess(string str, Dictionary<char, int> letterCounts)
        {
            foreach (var kvPair in GetLetterAndLetterCount(str))
                if (kvPair.Value > letterCounts[kvPair.Key])
                    return true;
            return false;
        }

        static Dictionary<char, int> GetLetterAndLetterCount(string str)
        {
            var result = new Dictionary<char, int>();
            foreach (var letter in str)
                if (result.ContainsKey(letter))
                    result[letter]++;
                else
                    result.Add(letter, 1);
            return result;
        }

        static bool HasExcessLetters(string first, string second)
        {
            foreach (var chr in second)            
                if (!Contains(chr, first))
                    return true;           
            return false;
        }

        static bool Contains(char chr, string str)
        {
            foreach (var item in str)
                if (item == chr)
                    return true;
            return false;
        }             
    }
}