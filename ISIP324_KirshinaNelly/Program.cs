using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        public enum Genre { Драма, Ужасы, Роман, Детектив, Фэнтези }

        public class Book
        {
            private static int nextId = 1;

            public int Id;
            public string Name;
            public string Author;
            public Genre Genre;
            public int Year;
            public decimal Price;

            public Book(string name, string author, Genre genre, int year, decimal price)
            {
                this.Id = nextId++;
                this.Name = name;
                this.Author = author;
                this.Genre = genre;
                this.Year = year;
                this.Price = price;
            }

            public void PrintInfo()
            {
                Console.WriteLine($"[Код: {Id}] {Name} / Автор: {Author} / Жанр: {Genre} / Год: {Year} / Цена: {Price} руб.");
            }
        }

        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            books.Add(new Book("Отверженные", "Виктор Гюго", Genre.Драма, 1967, 550));
            books.Add(new Book("Преступление и наказание", "Ф. Достоевский", Genre.Роман, 1866, 450));
            books.Add(new Book("Кладбище домашних животных", "Стивен Кинг", Genre.Ужасы, 1986, 700));
            books.Add(new Book("Шерлок Холмс", "А. Конан Дойл", Genre.Детектив, 1887, 350));
            books.Add(new Book("Властелин колец", "Дж. Р. Р. Толкин", Genre.Фэнтези, 1954, 800));

            while (true)
            {
                Console.WriteLine("Учёт книг в библиотеке");
                Console.WriteLine("1. Добавить книгу.");
                Console.WriteLine("2. Удалить книгу (через id).");
                Console.WriteLine("3. Поиск книги (по названию, автору, жанру).");
                Console.WriteLine("4. Сортировка книг (по названию или году).");
                Console.WriteLine("5. Самая дорогая и самая дешёвая книга.");
                Console.WriteLine("6. Количество книг по авторам.");
                Console.WriteLine("0. Выход.");
                Console.Write("Выберите пункт от 0 до 6: ");

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
                        FindBook(); 
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
                        Console.WriteLine("Неверный ввод. Попробуйте снова."); 
                        break;
                }
            }
        }

        static void AddBook()
        {
            Console.WriteLine("Добавление книги");
            string name = ReadString("Название: ");
            string author = ReadString("Автор: ");
            Genre genre = ReadGenre();
            int year = ReadInt("Год издания: ", 1, 2026);
            decimal price = ReadDecimal("Цена: ");

            books.Add(new Book(name, author, genre, year, price));
            Console.WriteLine("Книга успешно добавлена.");
        }

        static void RemoveBook()
        {
            Console.WriteLine("Удаление книги");
            int id = ReadInt("Введите код книги для удаления: ");
            Book found = books.FirstOrDefault(b => b.Id == id);

            if (found != null)
            {
                books.Remove(found);
                Console.WriteLine($"Книга '{found.Name}' удалена.");
            }
            else
            {
                Console.WriteLine("Книги с таким кодом не существует.");
            }
        }

        static void FindBook()
        {
            Console.WriteLine("Поиск книги");
            Console.WriteLine("1. Поиск по названию");
            Console.WriteLine("2. Поиск по автору");
            Console.WriteLine("3. Поиск по жанру");
            Console.Write("Выберите вариант: ");
            string choice = Console.ReadLine();

            List<Book> foundBooks = new List<Book>();

            if (choice == "1")
            {
                string query = ReadString("Введите название (или часть): ").ToLower();
                foundBooks = books.Where(b => b.Name.ToLower().Contains(query)).ToList();
            }
            else if (choice == "2")
            {
                string query = ReadString("Введите автора (или часть): ").ToLower();
                foundBooks = books.Where(b => b.Author.ToLower().Contains(query)).ToList();
            }
            else if (choice == "3")
            {
                Genre genre = ReadGenre();
                foundBooks = books.Where(b => b.Genre == genre).ToList();
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }

            Console.WriteLine($"Результаты поиска (найдено: {foundBooks.Count}):");
            if (foundBooks.Any())
            {
                foreach (var b in foundBooks) b.PrintInfo();
            }
            else
            {
                Console.WriteLine("Ничего не найдено.");
            }
        }

        static void SortBook()
        {
            Console.WriteLine("Сортировка книг");
            Console.WriteLine("1. По названию");
            Console.WriteLine("2. По году издания");
            Console.Write("Выберите параметр: ");
            string param = Console.ReadLine();

            Console.WriteLine("1. По возрастанию");
            Console.WriteLine("2. По убыванию");
            Console.Write("Выберите направление: ");
            string dir = Console.ReadLine();

            IEnumerable<Book> sortedBooks;

            if (param == "1")
            {
                sortedBooks = (dir == "2") ? books.OrderByDescending(b => b.Name) : books.OrderBy(b => b.Name);
                Console.WriteLine("Книги отсортированы по названию:");
            }
            else if (param == "2")
            {
                sortedBooks = (dir == "2") ? books.OrderByDescending(b => b.Year) : books.OrderBy(b => b.Year);
                Console.WriteLine("Книги отсортированы по году:");
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }

            foreach (var b in sortedBooks) b.PrintInfo();
        }

        static void MinMaxPriceBook()
        {
            if (!books.Any())
            {
                Console.WriteLine("Список книг пуст.");
                return;
            }
            decimal maxPrice = books.Max(b => b.Price);
            decimal minPrice = books.Min(b => b.Price);

            var expensive = books.Where(b => b.Price == maxPrice);
            var cheap = books.Where(b => b.Price == minPrice);

            Console.WriteLine("Самая дорогая книга:");
            foreach (var book in expensive) book.PrintInfo();

            Console.WriteLine("Самая дешёвая книга:");
            foreach (var book in cheap) book.PrintInfo();
        }

        static void AuthorsBook()
        {
            if (!books.Any())
            {
                Console.WriteLine("Список книг пуст.");
                return;
            }

            Console.WriteLine("Количество книг по авторам:");
            var grouped = books.GroupBy(b => b.Author);

            foreach (var group in grouped)
            {
                Console.WriteLine($"{group.Key}: {group.Count()} шт.");
            }
        }

        static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) return input;
                Console.WriteLine("Ошибка: Значение не может быть пустым.");
            }
        }

        static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max) return value;
                Console.WriteLine($"Ошибка: Введите целое число от {min} до {max}.");
            }
        }

        static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal value) && value > 0) return value;
                Console.WriteLine("Ошибка: Введите положительное число.");
            }
        }

        static Genre ReadGenre()
        {
            Console.WriteLine("Доступные жанры: Драма, Ужасы, Роман, Детектив, Фэнтези.");
            while (true)
            {
                Console.Write("Жанр: ");
                string input = Console.ReadLine();
                if (Enum.TryParse(input, true, out Genre genre)) return genre;
                Console.WriteLine("Ошибка: Введите жанр точно из списка.");
            }
        }
    }
}