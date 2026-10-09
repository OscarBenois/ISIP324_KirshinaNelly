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
        { Фантастика, Ужасы, Роман, Детектив, Фэнтези }
        public class Book
        {
            public int Id;
            public string Name;
            public string Author;
            public Genre Genre;
            public uint Year;
            public uint Price;

        }
        public Book(int id, string name, string author, Genre genre, int year, decimal price)
        {
            Id = id;
            Name = name;
            Author = author;
            Genre = genre;
            Year = year;
            Price = price;
        }

        static void Main(string[] args)
        {
        }
    }
}
