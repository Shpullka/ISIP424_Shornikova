using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    public enum BooksGenres
    {
        Фантастика,
        Научная_фантастика,
        Детективы,
        Романы,
        Исторический
    }
    public class Books
    {
        public int BookID { get; set; }
        public string NameBook { get; set; }
        public string AuthorBook { get; set; }
        public BooksGenres Generes { get; set; }
        public int PublicationBook { get; set; }
        public decimal PriceBook { get; set; }
        public override string ToString()
        {
            return $"Код: {BookID}\n" + $"Название: {NameBook}\n" + $"Автор: {AuthorBook}\n" + $"Жанр: {Generes}\n" + $"Год публикации: {PublicationBook}\n" + $"Цена: {PriceBook}\n";
        }
    }

    internal class Program
    {
        private static int Code = 1;
        public static List<Books> Library = new List<Books>();

        static void Main(string[] args)
        {
            Library.Add(new Books { BookID = Code++, NameBook = "Война и Мир", AuthorBook = "Лев Толстой", Generes = BooksGenres.Исторический, PublicationBook = 1869, PriceBook = 1500 });
            Library.Add(new Books { BookID = Code++, NameBook = "1984", AuthorBook = "Джордж Оруэлл", Generes = BooksGenres.Научная_фантастика, PublicationBook = 1949, PriceBook = 800 });
            Library.Add(new Books { BookID = Code++, NameBook = "Убийство в Восточном экспрессе", AuthorBook = "Агата Кристи", Generes = BooksGenres.Детективы, PublicationBook = 1934, PriceBook = 600 });
            Library.Add(new Books { BookID = Code++, NameBook = "Властелин колец", AuthorBook = "Дж. Р. Р. Толкин", Generes = BooksGenres.Фантастика, PublicationBook = 1954, PriceBook = 2000 });
            Library.Add(new Books { BookID = Code++, NameBook = "Гордость и предубеждение", AuthorBook = "Джейн Остин", Generes = BooksGenres.Романы, PublicationBook = 1813, PriceBook = 450 });

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nУчёт книг в библиотеке");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу по идентификатору");
                Console.WriteLine("3. Найти книгу (по названию, автору, жанру)");
                Console.WriteLine("4. Сортировать книги (по названию, году)");
                Console.WriteLine("5. Показать самую дорогую и самую дешёвую книгу");
                Console.WriteLine("6. Группировать книги по авторам");
                Console.WriteLine("7. Показать все книги");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choise = Console.ReadLine();
                try
                {
                    switch (choise)
                    {
                        case "1": AddBook(); break;
                        case "2": RemoveBook(); break;
                        case "3": SearchBooks(); break;
                        case "4": SortBooks(); break;
                        //case "5": ShowMinMaxPrice(); break;
                        //case "6": GroupByAuthor(); break;
                        //case "7": ShowAllBooks(); break;
                        case "0": exit = true; break;
                        default: Console.WriteLine("Неверная команда. Попробуйте снова"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }
        }
        private static string GetStringInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("Ошибка: строка не может быть пустой");
            }
        }
        private static int GetIntInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                {
                    return result;
                }
                Console.WriteLine("Ошибка: число должно быть положительным");
            }
        }
        private static decimal GetDecimalInput(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (decimal.TryParse(Console.ReadLine(), out decimal result) && result >= 0)
                {
                    return result;
                }
                Console.WriteLine("Ошибка: нужно неотрицательное число");
            }
        }
        private static BooksGenres GetGenresInput()
        {
            Console.WriteLine("\nВыберите жанр: ");
            var categories = Enum.GetValues(typeof(BooksGenres));
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories.GetValue(i)}");
            }
            while (true)
            {
                int choice = GetIntInput("Ваш выбор: ");
                if (choice >= 1 && choice <= categories.Length)
                {
                    return (BooksGenres)categories.GetValue(choice - 1);
                }
                Console.WriteLine($"Ошибка: введите число от 1 до {categories.Length}");
            }
        }
        private static void AddBook()
        {
            Console.WriteLine("\n1. Добавление новой книги");
            string title = GetStringInput("Введите название: ");
            string author = GetStringInput("Введите автора: ");
            BooksGenres genres = GetGenresInput();
            int year = GetIntInput("Введите год издания: ");
            decimal price = GetDecimalInput("Введите цену: ");

            var newBook = new Books
            {
                BookID = Code++,
                NameBook = title,
                AuthorBook = author,
                Generes = genres,
                PublicationBook = year,
                PriceBook = price
            };

            Library.Add(newBook);
            Console.WriteLine($"Книга успешно добавлена! Присвоен код: {newBook.BookID}");
        }
        private static void RemoveBook()
        {
            Console.WriteLine("\n2. Удаление книги");
            if (Library.Count == 0)
            {
                Console.WriteLine("Библиотека пуста");
                return;
            }
            int id = GetIntInput("Введите ID книги для удаления: ");
            var BookRemove = Library.FirstOrDefault(p => p.BookID == id);
            if (BookRemove != null)
            {
                Library.Remove(BookRemove);
                Console.WriteLine($"Книга '{BookRemove.NameBook}' удалена");
            }
            else
            {
                Console.WriteLine("Книга с таким ID не найдена");
            }
        }
        private static void SearchBooks()
        {
            Console.WriteLine("\n3. Поиск книг");
            Console.WriteLine("1. По названию");
            Console.WriteLine("2. По автору");
            Console.WriteLine("3. По жанру");
            Console.Write("Выберите критерий поиска: ");
            string choice = Console.ReadLine();
            List<Books> results = new List<Books>();

            switch (choice)
            {
                case "1":
                    string title = GetStringInput("Введите название книги: ").ToLower();
                    results = Library.Where(P => P.NameBook.ToLower().Contains(title)).ToList();
                    break;

                case "2":
                    string author = GetStringInput("Введите название автора: ").ToLower();
                    results = Library.Where(p => p.AuthorBook.ToLower().Contains(author)).ToList();
                    break;

                case "3":
                    BooksGenres genres = GetGenresInput();
                    results = Library.Where(p => p.Generes == genres).ToList();
                    break;

                default:
                    Console.WriteLine("Неверный критерий поиска");
                    return;
            }
            if (results.Count == 0)
            {
                Console.WriteLine("Книги по вашему запросу не найдены");
            }
            else
            {
                Console.WriteLine($"\nНайдено книг: {results.Count}");
                foreach (var book in results)
                {
                    Console.WriteLine(book);
                }
            }

        }
        private static void SortBooks()
        {
            Console.WriteLine("\n4. Сортировка книг");
            Console.WriteLine("1. По названию");
            Console.WriteLine("2. По году издания");
            Console.Write("Выберите критерий сортировки: ");
            string choice = Console.ReadLine();
            List<Books> sorted = new List<Books>();

            switch (choice) 
            {
                case "1":
                    sorted = Library.OrderBy(P => P.NameBook).ToList(); 
                    break;

                case "2":
                    sorted = Library.OrderBy(p => p.PublicationBook).ToList();
                    break;

                default:
                    Console.WriteLine("Неверный критерий сортировки");
                    return;
            }
            if (sorted.Count == 0)
            {
                Console.WriteLine("Библиотека пуста. Сортировать нечего");
            }
            else
            {
                Console.WriteLine("\nОтсортированный список:");
                foreach (var book in sorted)
                {
                    Console.WriteLine(book);
                }
            }
        }
    }
}
