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
        