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
        }
    }
    }
}
