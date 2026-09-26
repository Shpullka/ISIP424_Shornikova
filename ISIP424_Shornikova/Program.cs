using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    public class TextStatistics
    {
        public string Text { get; set; }
        public int WordCount { get; set; }
        public int SentenceCount { get; set; }
        public int VowelCount { get; set; }
        public int ConsonantCount { get; set; }
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
                Console.Write("Выберите действие: ");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1": AnalizNewText(); break;
                    //case "2": ShowHistory(); break;
                    case "3": exit = true; break;
                    default: Console.WriteLine("Неверная команда. Попробуйте снова"); break;
                }
            }
        }
        static void AnalizNewText()
        {
            Console.WriteLine("\nВведите текст (минимум 100 символов). Для завершения ввода нажмите Enter: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || input.Length < 100)
            {
                Console.WriteLine("Ошибка: текст должен содержать минимум 100 символов");
                return;
            }
            TextStatistics stats = new TextStatistics();
            stats.Text = input;

            string[] words = input.Split(new char[] { ' ', '.', ',', '!', '?', ';', ':', '-', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            stats.WordCount = words.Length;

            if (words.Length > 0)
            {
                string shortest = words[0];
                string longest = words[0];
                for (int i = 0; i < words.Length; i++)
                {
                    string cleanWords = words[i].Trim(new char[] { '.', ',', '!', '?', ';', ':', '-', '(', ')', '"', '\'' });
                    if (cleanWords.Length == 0) continue;
                    if (cleanWords.Length < shortest.Length)
                    {
                        shortest = cleanWords;
                    }
                    if (cleanWords.Length > longest.Length)
                    {
                        longest = cleanWords;
                    }

                }
                stats.ShortWord = shortest;
                stats.LongWord = longest;
            }
            else
            {
                stats.ShortWord = "Нет слов";
                stats.LongWord = "Нет слов";
            }

            int sentenceCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '.' || input[i] == '!' || input[i] == '?')
                {
                    sentenceCount++;
                }
            }
            if (sentenceCount == 0 && input.Length > 0)
            {
                sentenceCount = 1;
            }
            stats.SentenceCount = sentenceCount;

            string vowels = "аеёиоуыэюяaeiouy";
            int viwelsCount = 0;
            int consonantCount = 0;
            Dictionary<char, int> frequency = new Dictionary <char, int>();
            for (int i = 0; i < input.Length; i++)
            {
                char c = char.ToLower(input[i]);
            }
        }
    }
}