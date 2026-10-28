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
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Маркетплейс GMWOG||GG.MOW||WONGG");
                if (Core.CurrentUser == null)
                {
                    Console.WriteLine("1. Войти");
                    Console.WriteLine("2. Зарегистрироваться");
                    Console.WriteLine("3. Посмотреть товары (без входа)");
                    Console.WriteLine("0. Выход");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1": LoginMenu(); break;
                        case "2": RegisterMenu(); break;
                        case "3": ProductsMenu(); break;
                        case "0": exit = true; break;
                        default: Console.WriteLine("Неверная команда. Попробуйте снова."); break;
                    }
                }
                else
                {
                    Console.WriteLine($"Вы вошли как: {Core.CurrentUser.Nickname}");
                    Console.WriteLine("1. Посмотреть товары и добавить в корзину");
                    Console.WriteLine("2. Моя корзина");
                    Console.WriteLine("3. История заказов");
                    Console.WriteLine("4. Выйти из аккаунта");
                    Console.Write("Ваш выбор: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1": ProductsMenu(); break;
                        case "2": CartMenu(); break;
                        case "3": HistoryMenu(); break;
                        case "4": Core.Logout(); break;
                    }
                } 
            }
        }
    }
}
