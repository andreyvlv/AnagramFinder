using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Threading.Tasks;

namespace AnagramFinder
{
    class FingAnagram
    {     
        public static Task<List<string>> AnagramsParser(string wordToAnagram, string path)
        {
            return Task.Run(() =>
            {
               // dictionary для введенного слова см. далее
               Dictionary<char, int> arrayCountValues = new Dictionary<char, int>();
               // список слов для исключенных слов
               List<string> dictException = new List<string>();
               // список слов для словаря 
               List<string> dictionaryList = new List<string>();
               // список для результатов исключения 
               List<string> dictionaryList2 = new List<string>();

               string word = wordToAnagram;
               word = word.ToLower();

               // загрузить файл словаря
               dictionaryList = openFile(path);

               // Добавить слово в словарь dictionaryList2 если нет буквенной разницы между
               //введенным словом и строчкой из словаря. Например введеное слово "слово", 
               //а текущее словарное слово "словарь" => слово "словарь" исключаем, т.к. содержит буквы "а", "р", "ь";
               // А если текущее словарное слово "лов", то добавляем его в список dictionaryList2, т.к. буквы "л", "о" и "в" совпадают     
               foreach (string str in dictionaryList)
               {
                   int exc = str.ToCharArray().Except(word.ToCharArray()).ToArray().Length;
                   if (exc == 0)
                   {
                       dictionaryList2.Add(str);
                   }
               }

               // Составляем dictionary для введенннго слова в котором ключом будет буква слова, 
               // а значением количество ее повторений в слове 
               foreach (var letter in word.Distinct().ToArray())
               {
                   var count = word.Count(chr => chr == letter);
                   arrayCountValues.Add(letter, count);
               }

               foreach (string d in dictionaryList2)
               {
                   Dictionary<char, int> arrayCountValuesInCycle = new Dictionary<char, int>();
                   // Составляем dictionary для каждого слова из словаря в котором ключом будет буква слова, 
                   // а значением количество ее повторений в слове 
                   foreach (var letter in d.Distinct().ToArray())
                   {
                       var count = d.Count(chr => chr == letter);
                       arrayCountValuesInCycle.Add(letter, count);
                   }
                   // цикл сравнения текущего слова в словаре и введнного слова, 
                   // если какая-либо буква из словарного слова встречается больше чем та же буква в введеном слове,
                   // то текущее слово из цикла добавляется в список dictException. Например введенное слово - "слово", 
                   // а текущее в цикле слово "олово", слово "олово" подходит по буквам, но не подходит по их количеству.
                   // В слове "олово" три буквы "о", а в слове "слово" две. 
                   // Массивы принимают подобный вид - "слово" - [в] => [1], [л] => [1], [о] => [2], [с] => [1];
                   // "олово" - [в] => [1], [л] => [1], [о] => [3].  
                   // добавляем каждое слово в словарь неподходящих слов dictException
                   foreach (KeyValuePair<char, int> key in arrayCountValuesInCycle)
                   {
                       foreach (KeyValuePair<char, int> key1 in arrayCountValues)
                       {
                           if ((key.Key == key1.Key) && (key.Value > key1.Value))
                           {
                               dictException.Add(d);
                           }
                       }
                   }
               }
               // удаляем из словаря все неподходящие слова
               dictException = dictionaryList2.Except(dictException).ToList();
               return dictException;
           });
        }
        
        
        private static List<string> openFile(string path)
        {           
            IList<string> wordList = new List<string>(); // список dictionaryList для  загрузки словаря
            try
            {               
                string fullPath = Path.GetFullPath(@"" + path);                
                string[] dictionary = File.ReadAllLines(fullPath);               
                wordList = dictionary.ToList();                
                return wordList.ToList();
            }
            catch (IOException ex)
            {                              
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
