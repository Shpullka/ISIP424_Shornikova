using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            return $"Студент: {Name}\n" + $"Возраст: {Age}\n" + $"Контакты: {Contact}\n" + $"Количество курсов: {RegisterCourse.Count}\n";
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
                course.SetTeacher(this);
            }
        }
        public override string GetInfo()
        {
            return $"Преподаватель: {Name}\n" + $"Возраст: {Age}\n" + $"Контактная информация: {Contact}\n" + $"Ведёт курсов: {TaughtCourse.Count}\n";
        }
    }

    public class Course
    {
        private string _title;
        private int _credits;

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название курса не может быть пустым");
                }
                _title = value;
            }
        }

        public int Credits
        {
            get => _credits;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Количество кредитов должно быть положительным");
                }
                _credits = value;
            }
        }

        public List<Student> Students { get; private set; } = new List<Student>();
        public Teacher Instructor { get; private set; }

        public Course(string title, int credits)
        {
            Title = title;
            Credits = credits;
        }

        public void SetTeacher(Teacher teacher)
        {
            Instructor = teacher;
        }

        public void AddStudent(Student student)
        {
            if (!Students.Contains(student))
            {
                Students.Add(student);
            }
        }

        public string GetCourseDetails()
        {
            string TeacherName = Instructor != null ? Instructor.Name : "Не назначен";
            return $"\nКурс: {Title}\n" + $"Кредитов: {Credits}\n" + $"Преподаватель: {TeacherName}\n" + $"Студентов: {Students.Count}\n";
        }
    }

    public class University
    {
        private List<Student> _students = new List<Student>();
        private List<Teacher> _teacher = new List<Teacher>();
        private List<Course> _course = new List<Course>();

        public void AddStudent(Student student) => _students.Add(student);
        public void AddTeacher(Teacher teacher) => _teacher.Add(teacher);
        public void AddCourse(Course course) => _course.Add(course);

        public IEnumerable<Student> GetAllStudents() => _students;
        public IEnumerable<Teacher> GetAllTeacher() => _teacher;
        public IEnumerable<Course> GetAllCourse() => _course;

        public Student FindStudentByName(string name) => _students.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        public Teacher FindTeacherByName(string name) => _teacher.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        public Course FindCourseByTitle(string title) => _course.FirstOrDefault(p => p.Title.ToLower() == title.ToLower());

        public List<Student> GetStudentsByCourse(string CourseTitle)
        {
            var course = FindCourseByTitle(CourseTitle);
            if (course == null)
            {
                Console.WriteLine($"Курса '{CourseTitle}' не существует");
            }
            return course.Students;
        }
    }

    internal class Program
    {
        private static University _university = new University();
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nСистема управления университетом");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Записать студента на курс");
                Console.WriteLine("3. Список всех студентов");
                Console.WriteLine("4. Информация о конкретном студенте");
                Console.WriteLine("5. Добавить преподавателя");
                Console.WriteLine("6. Назначить преподавателя на курс");
                Console.WriteLine("7. Список всех преподавателей");
                Console.WriteLine("8. Информация о конкретном преподавателе");
                Console.WriteLine("9. Создать курс");
                Console.WriteLine("10. Список всех курсов");
                Console.WriteLine("11. Информация о конкретном курсе");
                Console.WriteLine("12. Просмотреть всю информацию");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": AddStudent(); break;
                        case "2": EnrollStudent(); break;
                        case "3": ShowAllStudents(); break;
                        case "4": ShowStudentDetails(); break;
                        case "5": AddTeacher(); break;
                        case "6": AssignTeacher(); break;
                        case "7": ShowAllTeachers(); break;
                        case "8": ShowTeacherDetails(); break;
                        case "9": AddCourse(); break;
                        case "10": ShowAllCourses(); break;
                        case "11": ShowCourseDetails(); break;
                        case "12": AllInformation(); break;
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
        static void AddStudent()
        {
            Console.WriteLine("\n1. Добавление нового студента");
            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введите контакты: ");
            string contact = Console.ReadLine();

            var student = new Student(name, age, contact);
            _university.AddStudent(student);
            Console.WriteLine("Студент успешно добавлен!");

        }
        static void EnrollStudent()
        {
            Console.WriteLine("\n2. Запись студента на курс");
            Console.Write("Введите имя студента: ");
            string sName = Console.ReadLine();
            var student = _university.FindStudentByName(sName);
            if (student == null)
            {
                throw new Exception("Студент не найден");
            }

            Console.Write("Введите название курса: ");
            string sTitle = Console.ReadLine();
            var course = _university.FindCourseByTitle(sTitle);
            if (course == null)
            {
                throw new Exception("Курс не найден");
            }

            student.SingInCourse(course);
            Console.WriteLine($"Студент '{student.Name}' записан на курс '{course.Title}'");
        }
        static void ShowAllStudents()
        {
            Console.WriteLine("\n3. Список всех студентов в системе");
            var students = _university.GetAllStudents().ToList();

            if (students.Count == 0)
            {
                Console.WriteLine("В системе пока нет студентов");
            }
            foreach (var s in students)
            {
                Console.WriteLine(s.GetInfo());
            }
        }
        static void ShowStudentDetails()
        {
            Console.WriteLine("\n4. Информация о конкретном студенте с его курсами");
            Console.Write("Введите имя студента: ");
            string name = Console.ReadLine();

            var student = _university.FindStudentByName(name);
            if (student == null)
            {
                throw new Exception($"Студент с именем '{name}' не найден");
            }
            Console.WriteLine("\nИнформация о студенте");
            Console.WriteLine(student.GetInfo());

            Console.WriteLine("\nКурсы, на которые записан студент:");
            var courses = student.RegisterCourse;
            if (courses.Count == 0)
            {
                Console.WriteLine("Студент пока не записан ни на один курс");
            }
            foreach (var c in courses)
            {
                Console.WriteLine($"- '{c.Title}' ({c.Credits} кредитов)\n" + $"Преподаватель: {(c.Instructor?.Name ?? "Не назначен")}\n");
            }
        }
        static void AddTeacher()
        {
            Console.WriteLine("\n5. Добавление нового преподавателя");
            Console.Write("Введите имя преподавателя: ");
            string name = Console.ReadLine();
            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Введите контакты: ");
            string contact = Console.ReadLine();

            var teacher = new Teacher(name, age, contact);
            _university.AddTeacher(teacher);
            Console.WriteLine("Преподаватель успешно добавлен!");
        }
        static void AssignTeacher()
        {
            Console.WriteLine("\n6. Запись преподавателя на курс");
            Console.Write("Введите имя преподавателя: ");
            string tName = Console.ReadLine();
            var teacher = _university.FindTeacherByName(tName);
            if (teacher == null)
            {
                throw new Exception("Преподаватель не найден");
            }

            Console.Write("Введите название курса: ");
            string tTitle = Console.ReadLine();
            var course = _university.FindCourseByTitle(tTitle);
            if (course == null)
            {
                throw new Exception("Курс не найден");
            }

            teacher.AssignToCourse(course);
            Console.WriteLine($"Преподаватель '{teacher.Name}' назначен на курс '{course.Title}'");
        }
        static void ShowAllTeachers()
        {
            Console.WriteLine("\n7. Список всех преподавателей в системе");
            var teachers = _university.GetAllTeacher().ToList();

            if (teachers.Count == 0)
            {
                Console.WriteLine("В системе пока нет преподавателей");
            }
            foreach (var t in teachers)
            {
                Console.WriteLine(t.GetInfo());
            }
        }
        static void ShowTeacherDetails()
        {
            Console.WriteLine("\n8. Информация о конкретном преподавателе с его курсами");
            Console.Write("Введите имя преподавателя: ");
            string name = Console.ReadLine();
            var teacher = _university.FindTeacherByName(name);
            if (teacher == null)
            {
                throw new Exception($"Преподаватель с именем '{name}' не найден");
            }
            Console.WriteLine("\nИнформация о преподавателе");
            Console.WriteLine(teacher.GetInfo());

            Console.WriteLine("\nКурсы, которые ведет преподаватель:");
            var courses = teacher.TaughtCourse;
            if (courses.Count == 0)
            {
                Console.WriteLine("Преподаватель пока не ведет ни один курс");
            }
            foreach (var c in courses)
            {
                Console.WriteLine($"- '{c.Title}' ({c.Credits} кредитов)\n" + $"Студентов на курсе: {c.Students.Count}\n");
            }
        }
        static void AddCourse()
        {
            Console.WriteLine("\n9. Создание нового курса");
            Console.Write("Введите название курса: ");
            string title = Console.ReadLine();
            Console.Write("Введите количество кредитов: ");
            int credits = int.Parse(Console.ReadLine());

            var course = new Course(title, credits);
            _university.AddCourse(course);
            Console.WriteLine("Курс успешно создан!");
        }  
        static void ShowAllCourses()
        {
            Console.WriteLine("\n10. Список всех курсов в системе");
            var courses = _university.GetAllCourse().ToList();
            if (courses.Count == 0)
            {
                Console.WriteLine("В системе пока нет курсов");
            }
            foreach (var c in courses)
            {
                Console.WriteLine(c.GetCourseDetails());
            }
        }
        static void ShowCourseDetails()
        {
            Console.WriteLine("\n11. Информация о конкретном курсе");
            Console.Write("Введите название курса: ");
            string title = Console.ReadLine();
            var course = _university.FindCourseByTitle(title);
            if (course == null)
            {
                throw new Exception($"Курс с названием '{title}' не найден");
            }
            Console.WriteLine("\nИнформация о курсе");
            Console.WriteLine(course.GetCourseDetails());

            Console.WriteLine("\nСтуденты, записанные на курс:");
            var students = course.Students;
            if (students.Count == 0)
            {
                Console.WriteLine("На курс пока никто не записан");
            }
            foreach (var s in students)
            {
                Console.WriteLine($"- '{s.Name}' (Возраст: {s.Age})");
            }
        }
        static void AllInformation()
        {
            Console.WriteLine("\n12. Вывод всей информации");
            Console.WriteLine("\nВсе студенты");
            foreach (var student in _university.GetAllStudents())
            {
                Console.WriteLine(student.GetInfo());
            }
            Console.WriteLine("\nВсе преподаватели");
            foreach (var teacher in _university.GetAllTeacher())
            {
                Console.WriteLine(teacher.GetInfo());
            }
            Console.WriteLine("\nВсе курсы");
            foreach (var course in _university.GetAllCourse())
            {
                Console.WriteLine(course.GetCourseDetails());
                var StudentOnCourse = _university.GetStudentsByCourse(course.Title);
                if (StudentOnCourse.Count > 0)
                {
                    Console.WriteLine("Студенты на курсе:");
                    foreach (var student in StudentOnCourse) 
                    {
                        Console.WriteLine($"- {student.Name}");
                    }
                }
            }
        }
    }
}
