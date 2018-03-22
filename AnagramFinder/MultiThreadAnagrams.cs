using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramFinder
{
    class MultiThreadAnagrams
    {
        public static List<string> GetAnagrams(string word, List<string> dictionary, int threadCount)
        {
            int step = dictionary.Count / threadCount;
            var results = new List<string>[threadCount];
            var result = new List<string>();
            Parallel.For(0, threadCount, i =>
            {
                var part = i < threadCount - 1 ?
                    dictionary.Skip(i * step).Take(step).ToList() :
                    dictionary.Skip(i * step).ToList();
                results[i] = FindAnagramsRefactored.AnagramsParser(word, part);
            });
            foreach (var arr in results)
            {
                result.AddRange(arr);
            }
            return result;
        }
    }
}
