using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            List<string> names = new List<string>();
            List<decimal> amounts = new List<decimal>();

            int count;
            while (true)
            {
                Console.Write("Введите количество операций (от 2 до 40): ");
                string input = Console.ReadLine();
                count = Convert.ToInt32(input);

                if (count >= 2 && count <= 40)
                {
                    break;
                }
                Console.Write("Ошибка! Введите число от 2 до 40");
            }

            Console.Write("\nВведите траты:");
            for (int i = 0; i < count; i++)
            {
                Console.Write($"\nОперация {i + 1}: ");
                Console.Write("Введите название товара или услуги: ");
                string name = Console.ReadLine();
                names.Add(name);
                Console.Write("Количество денег (рубли): ");
                string amountInput = Console.ReadLine();
                decimal amount = Convert.ToDecimal(amountInput);
                amounts.Add(amount);
            }

            Console.Write("\nВведите операцию: ");
            int choice = 0;
            string decision = Console.ReadLine();
            choice = Convert.ToInt32(decision);
           
            switch (choice)
            {
                case 1:
                    Console.    Write("1. Вывод данных");
                    for (int i = 0; i < names.Count; i++)
                    {
                        Console.Write(names[i] + ": " + amounts[i] + " ₽");
                    }
                    break;

                case 2:
                    Console.WriteLine("2. Статистика");
                    decimal sum = 0;
                    decimal max = amounts[0];
                    decimal min = amounts[0];

                    for (int i = 0; i < names.Count; i++)
                    {
                        sum = sum + amounts[i];
                        if (amounts[i] > max)
                        {
                            max = amounts[i];
                        }
                        if (amounts[i] < min)
                        {
                            min = amounts[i];
                        }
                    }

                    decimal average = sum / amounts.Count;
                    Console.WriteLine("Среднее: " + average);
                    Console.WriteLine("Минимум: " + min);
                    Console.WriteLine("Максимальное: " + max);
                    Console.WriteLine("Сумма: " + sum);
                    break;

                case 3:
                    Console.WriteLine("3. Сортировка");
                    List<string> SortNames = new List<string>(names);
                    List<decimal> SortDecimal = new List<decimal>(amounts);

                    int n = SortDecimal.Count;
                    for (int i = 0; i < n - 1; i++)
                    {
                        for (int j = 0; j < n - i - 1; j++)
                        {
                            if (SortDecimal[j] > SortDecimal[j + 1])
                            {
                                decimal tempDecimal = SortDecimal[j];
                                SortDecimal[j] = SortDecimal[j + 1];
                                SortDecimal[j + 1] = tempDecimal;

                                string tempName = SortNames[j];
                                SortNames[j] = SortNames[j + 1];
                                SortNames[j + 1] = tempName;
                            }
                        }
                    }

                    for (int i = 0; i < SortNames.Count; i++)
                    {
                        Console.WriteLine(SortNames[i] + ": " + SortDecimal[i] + " рублей");
                    }
                    break;

                case 4:
                    Console.WriteLine("4. Конвертация валюты");
                    Console.Write("Введите курс: ");
                    string convert = Console.ReadLine();
                    decimal rate = Convert.ToDecimal(convert);

                    Console.Write("Введите название валюты: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Курс 1 " + rate + " = " + rate + " рублей\n");

                    for (int i = 0; i < names.Count; i++)
                    {
                        decimal converted = amounts[i] / rate;
                        Console.WriteLine(names[i] + ": " + converted + " " + name);
                    }
                    break;

                case 5:
                    Console.WriteLine("5. Поиск по названию");
                    Console.Write("\nВведите название для поиска: ");
                    string search = Console.ReadLine();
                    Console.WriteLine("\nРезультаты поиска: ");

                    bool found = false;
                    for (int i = 0; i < names.Count; i++)
                    {
                        if (names[i].ToLower().Contains(search.ToLower()))
                        {
                            Console.WriteLine(names[i] + ": " + amounts[i] + "рублей");
                        }
                        else
                        {
                            Console.WriteLine("Ничего не найдено");
                            break;
                        }
                        
                    }
                    break;

                case 0:
                    Console.WriteLine("Выход из программы");
                    break;
            }
        }
    }
}
