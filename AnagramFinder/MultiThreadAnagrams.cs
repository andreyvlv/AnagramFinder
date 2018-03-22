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
                // Разбиение словаря на части, для последней части (i = threadCount - 1)
                // пропускается Take, и Skip идет до конца
                var partOfDict = i < threadCount - 1 ?
                    dictionary.Skip(i * step).Take(step).ToList() :
                    dictionary.Skip(i * step).ToList();               
                results[i] = FindAnagramsRefactored.AnagramsParser(word, partOfDict);
            });
            foreach (var arr in results)
            {
                result.AddRange(arr);
            }
            return result;
        }
    }
}
