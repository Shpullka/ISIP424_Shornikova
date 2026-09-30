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
                    case "2": ShowHistory(); break;
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
            int vowelsCount = 0;
            int consonantCount = 0;
            Dictionary<char, int> frequency = new Dictionary<char, int>();
            for (int i = 0; i < input.Length; i++)
            {
                char c = char.ToLower(input[i]);
                if (char.IsLetter(c))
                {
                    if (vowels.IndexOf(c) >= 0)
                    {
                        vowelsCount++;
                    }
                    else
                    {
                        consonantCount++;
                    }
                    if (frequency.ContainsKey(c))
                    {
                        frequency[c]++;
                    }
                    else
                    {
                        frequency[c] = 1;
                    }
                }
            }
            stats.VowelCount = vowelsCount;
            stats.ConsonantCount = consonantCount;
            stats.LetterFrequency = frequency;
            history.Add(stats);
            PrintStatistics(stats);
        }
        static void PrintStatistics(TextStatistics stats)
        {
            Console.WriteLine("\nРезультаты анализы");
            Console.WriteLine($"Количество слов: {stats.WordCount}");
            Console.WriteLine($"Количество предложений: {stats.SentenceCount}");
            Console.WriteLine($"Количество гласных букв: {stats.VowelCount}");
            Console.WriteLine($"Количество согласных букв: {stats.ConsonantCount}");
            Console.WriteLine($"Самое короткое слово: {stats.ShortWord}");
            Console.WriteLine($"Самое длинное слово: {stats.LongWord}");
            Console.WriteLine("\nЧастота встречаемости букв: ");
            foreach (KeyValuePair<char, int> pair in stats.LetterFrequency)
            {
                Console.WriteLine($"'{pair.Key}' : {pair.Value}");
            }
        }
        static void ShowHistory()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("\nИстория пуста. Сначала проанализируйте хотя бы один текст");
                return;
            }
            Console.WriteLine($"\nИстория анализов. Всего текстов {history.Count}");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"\nТекст {i + 1}");
                Console.WriteLine($"Текст: {history[i].Text}");
                PrintStatistics(history[i]);
            }
        }
    }
}