using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var listOfAnagrams = FingAnagram.AnagramsParser(word, path).Result;
            Console.WriteLine($"\nКоличество анаграмм: {listOfAnagrams.Count}");
            Console.WriteLine("\nАнаграммы:");
            foreach (var anagram in listOfAnagrams)            
                Console.WriteLine(anagram);            
        }
    }
}
