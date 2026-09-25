using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    public class TextAnaliz
    {
        public string Text { get; set; }
        public int WordCount { get; set; }
        public int SentenceCount { get; set; }
        public int VowelCount { get; set; }
        public int ConsonantCount { get; set }
        public string ShortWord { get; set; }
        public string LongWord { get; set; }

        public Dictionary<char, int> LetterFrequency { get; set; }

        public TextStatistics()
        {
            LetterFrequency = new Dictionary<char, int>();
        }
    }
    
    internal class Program
    {
        static List<TextStatistics> history = new List<TextStatistics>();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Анализ текста");
                Console.WriteLine("1. Ввести новый текст для анализа");
                Console.WriteLine("2. Показать статистику по прошлым текстам");
                Console.WriteLine("3. Выйти");
            }
        }
    }
}
