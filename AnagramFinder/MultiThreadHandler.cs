using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramFinder
{
    class MultiThreadHandler
    {              
        public static List<string> GetAnagrams(string word, List<string> dictionary, int threadCount)
        {                       
            var result = new List<string>();
            Parallel.For(0, threadCount, i =>
            {
                // Splitting the dictionary into chuncks                          
                var totalLength = dictionary.Count;
                var chunkLength = (int)Math.Ceiling(totalLength / (double)threadCount);
                var part = dictionary.Skip(i * chunkLength).Take(chunkLength).ToList();

                var anagrams = AnagramFinder.GetAnagrams(word, part);

                lock (result)
                    result.AddRange(anagrams);               
            });
            return result;
        }
    }
}
