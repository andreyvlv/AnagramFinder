using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AnagramFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите слово");
            var word = Console.ReadLine();            
            GetAnagrams(word);
            Console.ReadKey();
        }

        static void GetAnagrams(string word)
        {
            string path = Environment.CurrentDirectory + @"/dictionary/zdf-win3.txt";
            var dict = File.ReadAllLines(path).ToList();
            Stopwatch sw = new Stopwatch();
            sw.Start();           
            var anagrams = AnagramFinder.GetAnagrams(word, dict);
            sw.Stop();
            Console.WriteLine($"\nВремя поиска: {sw.Elapsed.TotalMilliseconds:f2} мс");
            Console.WriteLine($"\nКоличество анаграмм: {anagrams.Count}");
            Console.WriteLine("\nАнаграммы:\n");
            foreach (var anagram in anagrams)
                Console.WriteLine(anagram);
        }
    }
}
