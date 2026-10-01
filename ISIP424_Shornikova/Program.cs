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
        private static List<Books> Library = new List<Books>();

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
                        case "3": Searchbooks(); break;
                        case "4": SortBooks(); break;
                        case "5": ShowMinMaxPrice(); break;
                        case "6": GroupByAuthor(); break;
                        case "7": ShowAllBooks(); break;
                        case "0": exit = true; break;
                        default: Console.WriteLine("Неверная команда. Попробуйте снова"); break;
                    }
                }
            }
        }
    }
}
