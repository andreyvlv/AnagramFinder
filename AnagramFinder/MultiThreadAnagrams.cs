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
            var result = new List<string>();
            Parallel.For(0, threadCount, i =>
            {
                // Разбиение словаря на части                          
                var totalLength = dictionary.Count;
                var chunkLength = (int)Math.Ceiling(totalLength / (double)threadCount);
                var part = dictionary.Skip(i * chunkLength).Take(chunkLength).ToList();

                var anagrams = FindAnagramsRefactored.AnagramsParser(word, part);

                lock (result)
                    result.AddRange(anagrams);               
            });
            return result;
        }
    }
}
