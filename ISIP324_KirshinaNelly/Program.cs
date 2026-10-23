using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        public abstract class Person
        {
            private string fio;
            private int age;
            private string email;
            private string phone;

            protected Person(string fio, int age, string email, string phone)
            {
                Fio = fio;
                Age = age;
                Email = email;
                Phone = phone;
            }

            public string Fio
            {
                get => fio;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception("ФИО не может быть пустым.");
                    if (value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length < 3)
                        throw new Exception("ФИО должно содержать фамилию, имя и отчество.");
                    fio = value.Trim();
                }
            }

            public int Age
            {
                get => age;
                set
                {
                    if (value < 14 || value > 100)
                        throw new Exception("Возраст должен быть от 16 до 100 лет.");
                    age = value;
                }
            }

            public string Email
            {
                get => email;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception("Email не может быть пустым.");
                    if (!value.Contains("@") || !value.Contains("."))
                        throw new Exception("Некорректный email.");
                    email = value.Trim();
                }
            }

            public string Phone
            {
                get => phone;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception("Телефон не может быть пустым.");
                    phone = value.Trim();
                }
            }
            public virtual void PrintInfo()
            {
                Console.WriteLine($"  ФИО: {Fio}, Возраст: {Age}, Email: {Email}, Телефон: {Phone}");
            }
        }
        public class Teacher : Person
        {
            private static int nextTeacherID = 1;
            private int teacherID;
            private List<Course> courses = new List<Course>();

            public Teacher(string fio, int age, string email, string phone)
                : base(fio, age, email, phone)
            {
                teacherID = nextTeacherID++;
            }

            public int TeacherID => teacherID;
            public List<Course> Courses => courses;

            public void AssignToCourse(Course course)
            {
                if (course == null)
                    throw new Exception("Курс не может быть пустым.");
                course.SetTeacher(this);
            }

            internal void AddCourse(Course c) { if (!courses.Contains(c)) courses.Add(c); }
            internal void RemoveCourse(Course c) => courses.Remove(c);

            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($"  ID преподавателя: {teacherID}. Ведёт курсов: {courses.Count}");
                foreach (var c in courses)
                    Console.WriteLine($"    - {c.Name}");
            }
        }
        public class Student : Person
        {
            private static int nextStudentID = 1;
            private int studentID;
            private List<Course> courses = new List<Course>();

            public Student(string fio, int age, string email, string phone)
                : base(fio, age, email, phone)
            {
                studentID = nextStudentID++;
            }

            public int StudentID => studentID;
            public List<Course> Courses => courses;

            public void EnrollInCourses(Course course)
            {
                if (course == null)
                    throw new Exception("Курс не может быть пустым.");
                course.AddStudent(this);
            }

            internal void AddCourse(Course c) { if (!courses.Contains(c)) courses.Add(c); }

            public override void PrintInfo()
            {
                base.PrintInfo();
                Console.WriteLine($"  ID студента: {studentID}. Записан на курсов: {courses.Count}");
                foreach (var c in courses)
                    Console.WriteLine($"    - {c.Name}");
            }
        }
        