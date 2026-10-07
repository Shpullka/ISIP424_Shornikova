using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    public abstract class Person
    {
        private string name;
        private int age;
        private string contact;

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым");
                }
                name = value;
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (value < 16 || value > 100)
                {
                    throw new ArgumentException("Возраст должен быть в диапазоне от 16 до 100 лет");
                }
                age = value;
            }
        }

        public string Contact
        {
            get => contact;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Контактная информация не может быть пустой");
                }
                contact = value;
            }
        }

        protected Person(string name, int age, string contact)
        {
            this.Name = name;
            this.Age = age;
            this.Contact = contact;
        }
        public abstract string GetInfo();
    }

    public class Student : Person
    {

    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
