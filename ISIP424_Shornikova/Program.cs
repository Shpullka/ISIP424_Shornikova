using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP424_Shornikova
{
    public abstract class Person
    {
        private string _name;
        private int _age;
        private string _contact;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым");
                }
                _name = value;
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 16 || value > 100)
                {
                    throw new ArgumentException("Возраст должен быть в диапазоне от 16 до 100 лет");
                }
                _age = value;
            }
        }

        public string Contact
        {
            get => _contact;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Контактная информация не может быть пустой");
                }
                _contact = value;
            }
        }

        protected Person(string name, int age, string contact)
        {
            Name = name;
            Age = age;
            Contact = contact;
        }
        public abstract string GetInfo();
    }

    public class Student : Person
    {
        public List<Course> RegisterCourse { get; private set; } = new List<Course>();
        public Student(string name, int age, string contact) : base(name, age, contact) { }
        public void SingInCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            if (!RegisterCourse.Contains(course))
            {
                RegisterCourse.Add(course);
                course.AddStudent(this);
            }
        }
        public override string GetInfo()
        {
            return $"Студент: {Name}, Возраст: {Age}, Контакты: {Contact}, Количество курсов: {RegisterCourse.Count}";
        }
    }

    public class Teacher : Person
    {
        public List<Course> TaughtCourse { get; private set; } = new List<Course>();
        public Teacher(string name, int age, string contact) : base(name, age, contact) { }
        public void AssignToCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            if (!TaughtCourse.Contains(course))
            {
                TaughtCourse.Add(course);
                course.AddTeacher(this);
            }
        }
        public override string GetInfo()
        {
            return $"Преподаватель: {Name}, Возраст: {Age}, Контактная информация: {Contact}, Ведёт курсов: {TaughtCourse.Count}";
        }
    }

    public class Course
    {
        private string title;
        private int credits;

        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    throw new ArgumentException("Название курса не может быть пустым");
                }
                title = value;
            }
        }

        public int Credits
        {
            get => credits;
            set
            {
                if (credits <= 0)
                {
                    throw new ArgumentException("Количество кредитов должно быть положительным");
                }
                credits = value;
            }
        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
