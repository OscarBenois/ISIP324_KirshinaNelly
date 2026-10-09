using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        public enum Genre
        { Драма, Ужасы, Роман, Детектив, Фэнтези }
        public class Book
        {
            public int Id;
            public string Name;
            public string Author;
            public Genre Genre;
            public uint Year;
            public uint Price;

            public Book(int id, string name, string author, Genre genre, int year, decimal price)
            {
                Id = id;
                Name = name;
                Author = author;
                Genre = genre;
                Year = year;
                Price = price;

            }
            public void PrintInfo()
            {
                Console.WriteLine($"[Код: {id}] {name} / Цена: {price} / Автор: {author} / Жанр: {genre} / Год: {year}");
            }
        }
        static List<Book> books = new List<Book>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            books.Add(new Book(nextId++, "Отверженные", "Виктор Гюго", Genre.Драма, 1967, 550.50m));
            books.Add(new Book(nextId++, "Преступление и наказание", "Ф. Достоевский", Genre.Роман, 1866, 450.00m));
            books.Add(new Book(nextId++, "Кладбище домашних животных", "Стивен Кинг", Genre.Ужасы, 1986, 700.00m));
            books.Add(new Book(nextId++, "Шерлок Холмс", "А. Конан Дойл", Genre.Детектив, 1887, 350.00m));
            books.Add(new Book(nextId++, "Властелин колец", "Дж. Р. Р. Толкин", Genre.Фэнтези, 1954, 800.00m));

            while (true)
            {
                Console.WriteLine("Учёт книг в библиотеке");
                Console.WriteLine("1. Добавить книгу.");
                Console.WriteLine("2. Удалить книгу (через id).");
                Console.WriteLine("3. Поиск книги.");
                Console.WriteLine("4. Сортировка книг.");
                Console.WriteLine("5. Самая дорогая/дешёвая книга.");
                Console.WriteLine("6. Количество книг у автора.");
                Console.WriteLine("0. Выход.");
                Console.Write("Выберите пункт от 0 до 6:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        RemoveBook();
                        break;
                    case "3":
                        SearchBook();
                        break;
                    case "4":
                        SortBook();
                        break;
                    case "5":
                        MinMaxPriceBook();
                        break;
                    case "6":
                        AuthorsBook();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }

        }
        static void AddBook()
        {

        }
        static void RemoveBook()
        {

        }
        static void SearchBook()
        {

        }
        static void SortBook()
        {

        }
        static void MinMaxPriceBook()
        {

        }
        static void AuthorsBook()
        {

        }
    }
}
