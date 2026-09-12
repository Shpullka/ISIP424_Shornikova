using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    internal class Program
    {
        public enum ProductCategory
        {
            Электроника,
            Одежда,
            Бытовая_техника,
            Продукты
        }

        public class Product
        {
            public int ProductID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public ProductCategory Category { get; set; }

            public bool InStock => Quantity > 0;
            public override string ToString()
            {
                return $"Код: {ProductID}\n" + $"Название: {Name}\n" + $"Цена: {Price}\n" + $"Количество: {Quantity}\n" + $"Категория: {Category}\n" + $"В наличии: {(InStock ? "Да" : "Нет")}\n";
            }
        }

        public class Manager
        {
            public static List<Product> Products = new List<Product>();
            public static int Code = 1;
        }

        static void Main(string[] args)
        {
            Manager.Products.Add(new Product { ProductID = Manager.Code++, Name = "Смартфон", Price = 50000, Quantity = 10, Category = ProductCategory.Электроника });
            Manager.Products.Add(new Product { ProductID = Manager.Code++, Name = "Футболка", Price = 1500, Quantity = 50, Category = ProductCategory.Одежда });
            Manager.Products.Add(new Product { ProductID = Manager.Code++, Name = "Микроволновка", Price = 8000, Quantity = 5, Category = ProductCategory.Бытовая_техника });
            Manager.Products.Add(new Product { ProductID = Manager.Code++, Name = "Хлеб", Price = 40, Quantity = 100, Category = ProductCategory.Продукты });
            Manager.Products.Add(new Product { ProductID = Manager.Code++, Name = "Наушники", Price = 3000, Quantity = 0, Category = ProductCategory.Электроника });

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Система учета товаров");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Заказать поставку товара");
                Console.WriteLine("4. Продать товар");
                Console.WriteLine("5. Поиск товаров");
                Console.WriteLine("6. Показать все товары");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProduct(); break;
                    case "2": DeleteProduct(); break;
                    case "3": SupplyProduct(); break;
                    case "4": SellProduct(); break;
                    case "5": SearchProducts(); break;
                    case "6": ShowAllProducts(); break;
                    case "0": exit = true; break;
                    default: Console.WriteLine("Неверная команда. Попробуйте снова."); break;
                }
            }
        }
    }
}
