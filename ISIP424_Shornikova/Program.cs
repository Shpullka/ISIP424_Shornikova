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
            private static int Code = 1;
            public static int NextCode() => Code++;
        }

        static void Main(string[] args)
        {
            Manager.Products.Add(new Product { ProductID = Manager.NextCode(), Name = "Смартфон", Price = 50000, Quantity = 10, Category = ProductCategory.Электроника });
            Manager.Products.Add(new Product { ProductID = Manager.NextCode(), Name = "Футболка", Price = 1500, Quantity = 50, Category = ProductCategory.Одежда });
            Manager.Products.Add(new Product { ProductID = Manager.NextCode(), Name = "Микроволновка", Price = 8000, Quantity = 5, Category = ProductCategory.Бытовая_техника });
            Manager.Products.Add(new Product { ProductID = Manager.NextCode(), Name = "Хлеб", Price = 40, Quantity = 100, Category = ProductCategory.Продукты });
            Manager.Products.Add(new Product { ProductID = Manager.NextCode(), Name = "Наушники", Price = 3000, Quantity = 0, Category = ProductCategory.Электроника });

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nСистема учета товаров");
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
        static int GetInInput(string message, int min = 0)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= min)
                {
                    return result;
                }
                Console.WriteLine($"Ошибка: введите целое число не меньше {min}");
            }
        }

        static decimal GetDecimalInput(string message, decimal min = 0)
        {
            while (true)
            {
                Console.Write(message);
                if (decimal.TryParse(Console.ReadLine(), out decimal result) && result >= min)
                {
                    return result;
                }
                Console.WriteLine($"Ошибка: введите число не меньше {min}");
            }
        }

        static string GetStringInput(string message)
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

        static ProductCategory GetCategoryInput()
        {
            Console.WriteLine("\nВыберите категорию: ");
            var categories = Enum.GetValues(typeof(ProductCategory));
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories.GetValue(i)}");
            }
            while (true)
            {
                int choise = GetInInput("Ваш выбор: ", 1);
                if (choise <= categories.Length)
                {
                    return (ProductCategory)categories.GetValue(choise - 1);
                }
                Console.WriteLine("Ошибка: выберите номер из списка");
            }
        }

        static Product ProductById(int id)
        {
            return Manager.Products.FirstOrDefault(p => p.ProductID == id);
        }

        static void AddProduct()
        {
            Console.WriteLine("\n1. Добавление товаров");
            string name = GetStringInput("Введите название: ");
            decimal price = GetDecimalInput("Введите цену: ");
            int quantity = GetInInput("Введите количество: ");
            ProductCategory category = GetCategoryInput();

            var newProduct = new Product
            {
                ProductID = Manager.NextCode(),
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = category
            };

            Manager.Products.Add(newProduct);
            Console.WriteLine($"Товар успешно добавлен! Присвоен код: {newProduct.ProductID}");
        }

        static void DeleteProduct()
        {
            Console.WriteLine("\n2. Удаление товара");
            int id = GetInInput("Введите номер товара: ");
            var product = ProductById(id);
            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден");
                return;
            }
            Manager.Products.Remove(product);
            Console.WriteLine($"Товар '{product.Name}' удален");
        }

        static void SupplyProduct()
        {
            Console.WriteLine("\n3.Поставка товара");
            int id = GetInInput("Введите номер товара: ");
            var product = ProductById(id);
            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден");
                return;
            }
            int amount = GetInInput("Введите количество для поставки: ", 1);
            product.Quantity += amount;
            Console.WriteLine($"Поставка выполнена. Новое количество '{product.Name}': {product.Quantity}");
        }

        static void SellProduct()
        {
            Console.WriteLine("\n4. Продажа товара");
            int id = GetInInput("Введите номер товара: ");
            var product = ProductById(id);
            if (product == null)
            {
                Console.WriteLine("Товар с таким кодом не найден");
                return;
            }
            if (!product.InStock) {
                Console.WriteLine("Товара нет в наличии!");
                return;
            }
            int amount = GetInInput($"Введите количество для продажи (в наличии {product.Quantity}): ", 1);
            if (amount > product.Quantity)
            {
                Console.WriteLine("Ошибка! Недостаточно товара на складе");
                return;
            }
            product.Quantity -= amount;
            Console.WriteLine($"Продажа успешна. Остаток '{product.Name}': {product.Quantity}");
        }

        static void SearchProducts()
        {
            Console.WriteLine("\n5. Поиск товаров");
            Console.WriteLine("1. По коду");
            Console.WriteLine("2. По названию");
            Console.WriteLine("3. По категории");
            Console.WriteLine("Выберите способ: ");
            string choise = Console.ReadLine();
            List<Product> result = new List<Product>();
            switch (choise)
            {
                case "1":
                    int id = GetInInput("Введите номер товара: ");
                    var product = ProductById(id);
                    if (product != null)
                    {
                        result.Add(product);
                    }
                    break;

                case "2":
                    string name = GetStringInput("Введите название товара: ").ToLower();
                    result = Manager.Products.Where(p => p.Name.ToLower().Contains(name)).ToList();
                    break;

                case "3":
                    ProductCategory category = GetCategoryInput();
                    result = Manager.Products.Where(p => p.Category == category).ToList();
                    break;

                default:
                    Console.WriteLine("Неверный выбор");
                    return;
            }
            if (result.Count == 0)
            {
                Console.WriteLine("Ничего не найдено");
            }
            else
            {
                Console.WriteLine($"\nНайдено товаров: {result.Count}");
                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }
            }
        }

        static void ShowAllProducts()
        {
            Console.WriteLine("\n6. Показ всех товаров");
            if (Manager.Products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст");
                return;
            }
            Console.WriteLine($"\nВсего товаров: {Manager.Products.Count}");
            foreach (var product in Manager.Products)
            {
                Console.WriteLine(Manager.Products);
            }
        }
    }
}
  

  

