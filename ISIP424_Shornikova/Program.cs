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
            return $"Курс: {Title}\n" + $"Кредитов: {Credits}\n" + $"Преподаватель: {TeacherName}\n" + $"Студентов: {Students.Count}\n";
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

        public Student FindStudentByName(string name) => _students.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        public Teacher FindTeacherByName(string name) => _teacher.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        public Course FindCourseByTitle(string title) => _course.FirstOrDefault(p => p.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<Student> GetStudentsByCourse(string CourseTitle)
        {
            var course = FindCourseByTitle(CourseTitle);
            return course?.Students ?? Enumerable.Empty<Student>();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
