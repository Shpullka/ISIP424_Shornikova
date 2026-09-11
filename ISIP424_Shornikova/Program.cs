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

        class List
        {
            static List<Product> Products = new List<Product>();
            static int Code = 1001;
        }

        static void Main(string[] args)
        {
            
        }
    }
}
