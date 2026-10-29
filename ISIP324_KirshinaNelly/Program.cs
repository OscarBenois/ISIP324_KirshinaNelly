using System;
using System.Collections.Generic;
using System.Linq;

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
                    if (value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 3)
                        throw new Exception("ФИО должно содержать фамилию, имя и отчество.");
                    fio = value.Trim();
                }
            }

            public int Age
            {
                get => age;
                set
                {
                    if (value < 16 || value > 100)
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
            public IReadOnlyList<Course> Courses => courses;

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
            public IReadOnlyList<Course> Courses => courses;

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
        public class Course
        {
            private static int nextCourseID = 1;
            private int courseID;
            private string name;
            private string description;
            private Teacher teacher;
            private List<Student> students = new List<Student>();

            public Course(string name, string description)
            {
                courseID = nextCourseID++;
                Name = name;
                Description = description;
            }

            public int CourseID => courseID;

            public string Name
            {
                get => name;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception("Название курса не может быть пустым.");
                    name = value.Trim();
                }
            }

            public string Description
            {
                get => description;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new Exception("Описание курса не может быть пустым.");
                    description = value.Trim();
                }
            }

            public Teacher Teacher => teacher;
            public IReadOnlyList<Student> Students => students;

            internal void SetTeacher(Teacher t)
            {
                if (teacher == t) return;
                if (teacher != null) teacher.RemoveCourse(this);
                teacher = t;
                if (t != null) t.AddCourse(this);
            }

            internal void AddStudent(Student s)
            {
                if (students.Contains(s))
                    throw new Exception("Студент уже записан на этот курс.");
                students.Add(s);
                s.AddCourse(this);
            }

            public void PrintInfo()
            {
                Console.WriteLine($"  ID курса: {courseID} / Название: {Name}");
                Console.WriteLine($"  Описание: {Description}");
                Console.WriteLine($"  Преподаватель: {(teacher != null ? teacher.Fio : "не назначен")}");
                Console.WriteLine($"  Студентов записано: {students.Count}");
            }
        }
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            List<Course> courses = new List<Course>();

            ShowMenu();
            void ShowMenu()
            {
                while (true)
                {
                    Console.WriteLine("Система управления институтом: ");
                    Console.WriteLine("1. Управление студентами");
                    Console.WriteLine("2. Управление преподавателями");
                    Console.WriteLine("3. Управление курсами");
                    Console.WriteLine("4. Показать все списки");
                    Console.WriteLine("0. Выход");
                    Console.Write("Выберите пункт: ");

                    try
                    {
                        switch (Console.ReadLine())
                        {
                            case "1": StudentMenu(); break;
                            case "2": TeacherMenu(); break;
                            case "3": CourseMenu(); break;
                            case "4": ShowAllLists(); break;
                            case "0": return;
                            default: Console.WriteLine("Неверный ввод."); break;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
                }
            }
            void StudentMenu()
            {
                while (true)
                {
                    Console.WriteLine("Студенты: ");
                    Console.WriteLine("1. Добавить студента");
                    Console.WriteLine("2. Показать всех студентов");
                    Console.WriteLine("3. Записать студента на курс");
                    Console.WriteLine("4. Показать курсы студента");
                    Console.WriteLine("0. Назад");
                    Console.Write("Выберите пункт: ");

                    try
                    {
                        switch (Console.ReadLine())
                        {
                            case "1": AddStudent(); break;
                            case "2": ShowStudents(); break;
                            case "3": EnrollStudent(); break;
                            case "4": ShowStudentCourses(); break;
                            case "0": return;
                            default: Console.WriteLine("Неверный ввод."); break;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
                }
            }

            void AddStudent()
            {
                Console.WriteLine("Добавление студента");
                Console.WriteLine("Формат ввода: ФИО; возраст; email; телефон");
                Console.Write("Ваш ввод: ");
                string[] parts = (Console.ReadLine() ?? "").Split(';');

                if (parts.Length != 4)
                {
                    Console.WriteLine("Неверный формат. Нужно 4 поля через ';'");
                    return;
                }

                if (!int.TryParse(parts[1].Trim(), out int age))
                {
                    Console.WriteLine("Ошибка: возраст должен быть числом.");
                    return;
                }

                Student s = new Student(parts[0].Trim(), age, parts[2].Trim(), parts[3].Trim());
                students.Add(s);
                Console.WriteLine($"Студент добавлен. ID: {s.StudentID}");
            }

            void ShowStudents()
            {
                if (students.Count == 0) { Console.WriteLine("Студентов нет."); return; }
                Console.WriteLine("Все студенты:");
                foreach (var s in students.OrderBy(x => x.Fio))
                {
                    Console.WriteLine("Студент: ");
                    s.PrintInfo();
                }
            }

            void EnrollStudent()
            {
                if (students.Count == 0) { Console.WriteLine("Список студентов пуст."); return; }
                if (courses.Count == 0) { Console.WriteLine("Список курсов пуст."); return; }

                Console.WriteLine("Выберите студента:");
                for (int i = 0; i < students.Count; i++)
                    Console.WriteLine($"{i + 1}. {students[i].Fio}");
                int si = ReadInt("Номер: ", 1, students.Count) - 1;

                Console.WriteLine("Выберите курс:");
                for (int i = 0; i < courses.Count; i++)
                    Console.WriteLine($"{i + 1}. {courses[i].Name}");
                int ci = ReadInt("Номер: ", 1, courses.Count) - 1;

                students[si].EnrollInCourses(courses[ci]);
                Console.WriteLine($"Студент '{students[si].Fio}' записан на курс '{courses[ci].Name}'.");
            }

            void ShowStudentCourses()
            {
                if (students.Count == 0) { Console.WriteLine("Список студентов пуст."); return; }

                Console.WriteLine("Выберите студента:");
                for (int i = 0; i < students.Count; i++)
                    Console.WriteLine($"{i + 1}. {students[i].Fio}");
                int si = ReadInt("Номер: ", 1, students.Count) - 1;

                var s = students[si];
                Console.WriteLine($"Курсы студента '{s.Fio}':");
                if (s.Courses.Count == 0) { Console.WriteLine("  Курсов нет."); return; }
                foreach (var c in s.Courses.OrderBy(x => x.Name))
                    Console.WriteLine($"  - [{c.CourseID}] {c.Name}");
            }
            void TeacherMenu()
            {
                while (true)
                {
                    Console.WriteLine("Преподаватели: ");
                    Console.WriteLine("1. Добавить преподавателя");
                    Console.WriteLine("2. Показать всех преподавателей");
                    Console.WriteLine("3. Назначить преподавателя на курс");
                    Console.WriteLine("0. Назад");
                    Console.Write("Выберите пункт: ");

                    try
                    {
                        switch (Console.ReadLine())
                        {
                            case "1": AddTeacher(); break;
                            case "2": ShowTeachers(); break;
                            case "3": AssignTeacher(); break;
                            case "0": return;
                            default: Console.WriteLine("Неверный ввод."); break;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
                }
            }

            void AddTeacher()
            {
                Console.WriteLine("Добавление преподавателя");
                Console.WriteLine("Формат ввода: ФИО; возраст; email; телефон");
                Console.Write("Ваш ввод: ");
                string[] parts = (Console.ReadLine() ?? "").Split(';');

                if (parts.Length != 4)
                {
                    Console.WriteLine("Неверный формат. Нужно 4 поля через ';'");
                    return;
                }

                if (!int.TryParse(parts[1].Trim(), out int age))
                {
                    Console.WriteLine("Ошибка: возраст должен быть числом.");
                    return;
                }

                if (age < 21)
                {
                    Console.WriteLine("Ошибка: преподаватель должен быть не младше 21 года.");
                    return;
                }

                Teacher t = new Teacher(parts[0].Trim(), age, parts[2].Trim(), parts[3].Trim());
                teachers.Add(t);
                Console.WriteLine($"Преподаватель добавлен. ID: {t.TeacherID}");
            }

            void ShowTeachers()
            {
                if (teachers.Count == 0) { Console.WriteLine("Преподавателей нет."); return; }
                Console.WriteLine("Все преподаватели:");
                foreach (var t in teachers.OrderBy(x => x.Fio))
                {
                    Console.WriteLine("Преподаватель: ");
                    t.PrintInfo();
                }
            }

            void AssignTeacher()
            {
                if (teachers.Count == 0) { Console.WriteLine("Список преподавателей пуст."); return; }
                if (courses.Count == 0) { Console.WriteLine("Список курсов пуст."); return; }

                Console.WriteLine("Выберите преподавателя:");
                for (int i = 0; i < teachers.Count; i++)
                    Console.WriteLine($"{i + 1}. {teachers[i].Fio}");
                int ti = ReadInt("Номер: ", 1, teachers.Count) - 1;

                Console.WriteLine("Выберите курс:");
                for (int i = 0; i < courses.Count; i++)
                {
                    string current = courses[i].Teacher != null ? courses[i].Teacher.Fio : "не назначен";
                    Console.WriteLine($"{i + 1}. {courses[i].Name} (текущий преподаватель: {current})");
                }
                int ci = ReadInt("Номер: ", 1, courses.Count) - 1;

                teachers[ti].AssignToCourse(courses[ci]);
                Console.WriteLine($"Преподаватель '{teachers[ti].Fio}' назначен на курс '{courses[ci].Name}'.");
            }
            void CourseMenu()
            {
                while (true)
                {
                    Console.WriteLine("Курсы: ");
                    Console.WriteLine("1. Добавить курс");
                    Console.WriteLine("2. Показать все курсы");
                    Console.WriteLine("3. Показать детали курса");
                    Console.WriteLine("4. Вывести студентов курса");
                    Console.WriteLine("0. Назад");
                    Console.Write("Выберите пункт: ");

                    try
                    {
                        switch (Console.ReadLine())
                        {
                            case "1": AddCourse(); break;
                            case "2": ShowCourses(); break;
                            case "3": ShowCourseDetails(); break;
                            case "4": ShowCourseStudents(); break;
                            case "0": return;
                            default: Console.WriteLine("Неверный ввод."); break;
                        }
                    }
                    catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
                }
            }

            void AddCourse()
            {
                Console.WriteLine("Добавление курса");
                Console.WriteLine("Формат ввода: название; описание");
                Console.Write("Ваш ввод: ");
                string[] parts = (Console.ReadLine() ?? "").Split(';');

                if (parts.Length != 2)
                {
                    Console.WriteLine("Неверный формат. Нужно 2 поля через ';'");
                    return;
                }

                var c = new Course(parts[0].Trim(), parts[1].Trim());
                courses.Add(c);

                if (teachers.Count > 0)
                {
                    Console.Write("Назначить преподавателя сразу? (1 - да, 0 - нет): ");
                    if (Console.ReadLine() == "1")
                    {
                        Console.WriteLine("Выберите преподавателя:");
                        for (int i = 0; i < teachers.Count; i++)
                            Console.WriteLine($"{i + 1}. {teachers[i].Fio}");
                        int ti = ReadInt("Номер: ", 1, teachers.Count) - 1;
                        teachers[ti].AssignToCourse(c);
                    }
                }

                Console.WriteLine($"Курс создан. ID: {c.CourseID}");
            }

            void ShowCourses()
            {
                if (courses.Count == 0) { Console.WriteLine("Курсов нет."); return; }
                Console.WriteLine("Все курсы:");
                foreach (var c in courses.OrderBy(x => x.Name))
                {
                    Console.WriteLine("Курс");
                    c.PrintInfo();
                }
            }

            void ShowCourseDetails()
            {
                if (courses.Count == 0) { Console.WriteLine("Курсов нет."); return; }

                Console.WriteLine("Выберите курс:");
                for (int i = 0; i < courses.Count; i++)
                    Console.WriteLine($"{i + 1}. {courses[i].Name}");
                int ci = ReadInt("Номер: ", 1, courses.Count) - 1;

                Console.WriteLine();
                courses[ci].PrintInfo();
            }

            void ShowCourseStudents()
            {
                if (courses.Count == 0) { Console.WriteLine("Курсов нет."); return; }

                Console.WriteLine("Выберите курс:");
                for (int i = 0; i < courses.Count; i++)
                    Console.WriteLine($"{i + 1}. {courses[i].Name}");
                int ci = ReadInt("Номер: ", 1, courses.Count) - 1;

                var c = courses[ci];
                Console.WriteLine($"Курс: {c.Name}");
                Console.WriteLine($"Преподаватель: {(c.Teacher != null ? c.Teacher.Fio : "не назначен")}");
                Console.WriteLine($"Описание: {c.Description}");

                if (c.Students.Count == 0)
                {
                    Console.WriteLine("На курсе пока нет студентов.");
                    return;
                }

                Console.WriteLine("Список студентов:");
                foreach (var s in c.Students.OrderBy(x => x.Fio))
                    Console.WriteLine($"  - [{s.StudentID}] {s.Fio}");
            }
            void ShowAllLists()
            {
                Console.WriteLine("Все списки: ");

                Console.WriteLine("Студенты: ");
                if (students.Count == 0) Console.WriteLine("Список студентов пуст.");
                else foreach (var s in students.OrderBy(x => x.Fio))
                        Console.WriteLine($"  [{s.StudentID}] {s.Fio}");

                Console.WriteLine("Преподаватели: ");
                if (teachers.Count == 0) Console.WriteLine("Список преподавателей пуст.");
                else foreach (var t in teachers.OrderBy(x => x.Fio))
                        Console.WriteLine($"  [{t.TeacherID}] {t.Fio}");

                Console.WriteLine("Курсы: ");
                if (courses.Count == 0) Console.WriteLine("Список курсов пуст.");
                else foreach (var c in courses.OrderBy(x => x.Name))
                        Console.WriteLine($"  [{c.CourseID}] {c.Name}");
            }
            int ReadInt(string prompt, int min, int max)
            {
                while (true)
                {
                    Console.Write(prompt);
                    if (int.TryParse(Console.ReadLine(), out int v) && v >= min && v <= max)
                        return v;
                    Console.WriteLine($"Введите целое число от {min} до {max}.");
                }
            }
        }
    }
}