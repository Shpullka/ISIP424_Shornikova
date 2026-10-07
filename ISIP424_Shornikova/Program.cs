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
        public static List<int> Basket = new List<int>();

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
                Console.WriteLine("8. Пакетный импорт книг");
                Console.WriteLine("9. Добавить книгу в корзину");
                Console.WriteLine("10. Показать корзину и итоговую стоимость");
                Console.WriteLine("11. Очистить корзину");
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
                        case "5": ShowMinMaxPrice(); break;
                        case "6": GroupByAuthor(); break;
                        case "7": ShowAllBooks(); break;
                        case "8": BatchImport(); break;
                        case "9": AddToCart(); break;
                        case "10": ShowCart(); break;
                        case "11": ClearCart(); break;
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
        private static bool TryParseGenre(string input, out BooksGenres genre)
        {
            return Enum.TryParse(input.Trim(), true, out genre);
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
                Basket.Remove(id);
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
        private static void ShowMinMaxPrice()
        {
            Console.WriteLine("\n5. Самая дорогая и дешёвая книга");
            if (Library.Count == 0)
            {
                Console.WriteLine("Библиотека пуста");
                return;
            }
            decimal MaxPrice = Library.Max(p => p.PriceBook);
            decimal MinPrice = Library.Min(p => p.PriceBook);
            var maxPriceBook = Library.Where(p => p.PriceBook == MaxPrice).ToList();
            var minPriceBook = Library.Where(p => p.PriceBook == MinPrice).ToList();
            Console.WriteLine($"Самая дорогая книга: {maxPriceBook}");
            Console.WriteLine($"\nСамая дешёвая книга: {minPriceBook}");
        }
        private static void GroupByAuthor()
        {
            Console.WriteLine("\n6. Группировка по авторам");
            if (Library.Count == 0)
            {
                Console.WriteLine("Библиотека пуста");
                return;
            }
            var grouped = Library.GroupBy(p => p.AuthorBook);
            foreach (var group in grouped)
            {
                Console.WriteLine($"Автор: {group.Key} (Книг: {group.Count()})");
                foreach (var book in group)
                {
                    Console.WriteLine($"  - {book.NameBook} ({book.PublicationBook})");
                }
            }
        }
        private static void BatchImport()
        {
            Console.WriteLine("\n8. Пакетный импорт книг");
            Console.WriteLine("Формат строки: Название;Автор;Жанр;Год;Цена");
            Console.WriteLine("Доступные жанры: Фантастика, Научная_фантастика, Детективы, Романы, Исторический");
            int count = GetIntInput("Введите количество книг для добавления");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nКнига {i} из {count}:");
                Console.Write("");
                string line = Console.ReadLine();
                string[] parts = line.Split(';');
                if (parts.Length != 5)
                {
                    Console.WriteLine("Ошибка: ожидается 5 полей");
                    return;
                }
                for (int j = 0; j < parts.Length; j++)
                {
                    parts[j] = parts[j].Trim();
                }

                if (string.IsNullOrWhiteSpace(parts[0]))
                {
                    Console.WriteLine("Ошибка: название пустое");
                    return;
                }
                if (string.IsNullOrWhiteSpace(parts[1]))
                {
                    Console.WriteLine("Ошибка: автор пустой");
                    return;
                }
                if (!TryParseGenre(parts[2], out BooksGenres genre))
                {
                    Console.WriteLine($"Ошибка: неизвестный жанр '{parts[2]}'");
                    return;
                }
                if (!int.TryParse(parts[3], out int year) || year <= 0)
                {
                    Console.WriteLine($"Ошибка: неверный год '{parts[3]}'");
                    return;
                }
                if (!decimal.TryParse(parts[4], out decimal price) || price < 0)
                {
                    Console.WriteLine($"Ошибка: неверная цена '{parts[4]}'");
                    return;
                }

                var newBook = new Books
                {
                    BookID = Code++,
                    NameBook = parts[0],
                    AuthorBook = parts[1],
                    Generes = genre,
                    PublicationBook = year,
                    PriceBook = price
                };
                Library.Add(newBook);
                Console.WriteLine($"Книга успешно добавлена! Присвоен код: {newBook.BookID}");
            }
            Console.WriteLine($"Импорт завершён");
        }
        private static void AddToCart()
        {
            Console.WriteLine("\n9. Добавление книги в корзину");
            if (Library.Count == 0)
            {
                Console.WriteLine("Библиотека пуста");
                return;
            }
            int id = GetIntInput("Введите ID книги: ");
            var book = Library.FirstOrDefault(p => p.BookID == id);
            if (book == null)
            {
                Console.WriteLine("Книга с таким ID не найдена");
                return;
            }
            if (Basket.Contains(id))
            {
                Console.WriteLine($"Книга '{book.NameBook}' уже в корзине");
                return;
            }
            Basket.Add(id);
            Console.WriteLine($"Книга '{book.NameBook}' добавлена в корзину");
        }
        private static void ShowCart()
        {
            Console.WriteLine("\n10. Корзина");
            if (Basket.Count == 0)
            {
                Console.WriteLine("Корзина пуста");
                return;
            }
            var BasketBooks = Library.Where(p => Basket.Contains(p.BookID)).ToList();
            Console.WriteLine("Книги в корзине:");
            foreach (var book in BasketBooks)
            {
                Console.WriteLine($"  - '{book.NameBook}' ({book.AuthorBook}) — {book.PriceBook}");
            }

            decimal total = BasketBooks.Sum(p => p.PriceBook);
            Console.WriteLine($"\nИтоговая стоимость: {total}");
        }
        private static void ClearCart()
        {
            Console.WriteLine("\n11. Очистка корзины");
            if (Basket.Count == 0)
            {
                Console.WriteLine("Корзина уже пуста");
                return;
            }
            Basket.Clear();
            Console.WriteLine("Корзина очищена");
        }
        private static void ShowAllBooks()
        {
            Console.WriteLine("\n7. Все книги в библиотеке");
            if (Library.Count == 0)
            {
                Console.WriteLine("Библиотека пуста");
                return;
            }
            foreach (var book in Library)
            {
                Console.WriteLine(book);
            }
        }
    }
}
