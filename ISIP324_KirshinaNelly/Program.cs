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
            public double Price;

            public Book(string name, string author, Genre genre, int year, double price)
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
                Console.WriteLine($"[Код: {Id}] {Name} / Автор: {Author} / Жанр: {Genre} / Год: {Year} / Цена: {Price}");
            }
        }

        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            books.Add(new Book("Отверженные", "Виктор Гюго", Genre.Драма, 1967, 550.50));
            books.Add(new Book("Преступление и наказание", "Ф. Достоевский", Genre.Роман, 1866, 450.00));
            books.Add(new Book("Кладбище домашних животных", "Стивен Кинг", Genre.Ужасы, 1986, 700.00));
            books.Add(new Book("Шерлок Холмс", "А. Конан Дойл", Genre.Детектив, 1887, 350.00));
            books.Add(new Book("Властелин колец", "Дж. Р. Р. Толкин", Genre.Фэнтези, 1954, 800.00));

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
            Console.WriteLine("Добавление книги.");
            string name = ReadString("Название: ");
            string author = ReadString("Автор: ");
            Genre genre = ReadGenre();
            int year = ReadInt("Год издания: ");
            double price = ReadDouble("Цена: ");

            books.Add(new Book(name, author, genre, year, price));
            Console.WriteLine("Книга добавлена.");
        }

        static void RemoveBook()
        {
            Console.WriteLine("Удаление книги.");
            int id = ReadInt("Введите код книги для удаления: ");

            Book found = books.FirstOrDefault(b => b.Id == id);

            if (found != null)
            {
                books.Remove(found);
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Книги с таким кодом не существует.");
            }
        }

        static void SearchBook()
        {
            Console.WriteLine("Поиск книг.");
            Console.WriteLine("Введите название, автора или жанр:");
            string query = Console.ReadLine().Trim().ToLower();

            Console.WriteLine("Результаты поиска:");

            var foundBooks = books.Where(b => b.Name.ToLower().Contains(query) || b.Author.ToLower().Contains(query) || b.Genre.ToString().ToLower() == query).ToList();

            if (foundBooks.Any())
            {
                foreach (var b in foundBooks)
                {
                    b.PrintInfo();
                }
            }
            else
            {
                Console.WriteLine("Ничего не найдено.");
            }
            Console.WriteLine();
        }

        static void SortBook()
        {
            Console.WriteLine("Сортировка книг.");
            Console.WriteLine("1. По названию");
            Console.WriteLine("2. По году издания");
            Console.Write("Выберите вариант сортировки: ");
            string choice = Console.ReadLine();

            IEnumerable<Book> sortedBooks;

            if (choice == "1")
            {
                sortedBooks = books.OrderBy(b => b.Name);
                Console.WriteLine("Книги отсортированы по названию:");
            }
            else if (choice == "2")
            {
                sortedBooks = books.OrderBy(b => b.Year);
                Console.WriteLine("Книги отсортированы по году:");
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }

            foreach (var b in sortedBooks)
            {
                b.PrintInfo();
            }
            Console.WriteLine();
        }

        static void MinMaxPriceBook()
        {
            if (!books.Any())
            {
                Console.WriteLine("Список книг пуст.");
                return;
            }

            var cheapest = books.OrderBy(b => b.Price).First();
            var mostExpensive = books.OrderByDescending(b => b.Price).First();

            Console.WriteLine("Самая дешёвая книга:");
            cheapest.PrintInfo();
            Console.WriteLine("Самая дорогая книга:");
            mostExpensive.PrintInfo();
            Console.WriteLine();
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
            Console.WriteLine();
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

        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value > 0) return value;
                Console.WriteLine("Ошибка: Введите положительное число.");
            }
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0) return value;
                Console.WriteLine("Ошибка: Введите целое число больше нуля.");
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
                Console.WriteLine("Ошибка: Введите жанр из списка.");
            }
        }
    }
}