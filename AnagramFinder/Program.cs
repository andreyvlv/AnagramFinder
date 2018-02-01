using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AnagramFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите слово");
            string word = Console.ReadLine();
            GetAnagrams(word);
            Console.ReadKey();
        }

        static void GetAnagrams(string word)
        {
            string path = @"../../dictionary/zdf-win3.txt";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var listOfAnagrams = FingAnagramNew.AnagramsParser(word, path);
            sw.Stop();
            Console.WriteLine($"\nВремя поиска: {sw.ElapsedMilliseconds / 1000.0} сек");
            Console.WriteLine($"\nКоличество анаграмм: {listOfAnagrams.Count}");
            Console.WriteLine("\nАнаграммы:\n");
            foreach (var anagram in listOfAnagrams)            
                Console.WriteLine(anagram);            
        }
    }
}
