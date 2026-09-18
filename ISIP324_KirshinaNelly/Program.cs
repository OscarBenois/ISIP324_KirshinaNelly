using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        public enum Category { Напитки, Выпечка, Фастфуд, Хозтовары, Еда }

        public class Product
        {
            private static int nextCode = 1; 

            public int code;
            public string name;
            public double price; 
            public int quantity;
            public Category category;

            
            public bool isThere => quantity > 0;

            public Product(string name, double price, int quantity, Category category)
            {
                this.code = nextCode++;
                this.name = name;
                this.price = price;
                this.quantity = quantity;
                this.category = category;
            }
            public void PrintInfo()
            {
                Console.WriteLine($"[Код: {code}] {name} | Цена: {price} | Остаток: {quantity} | Категория: {category} | В наличии: {(isThere ? "Да" : "Нет")}");
            }
        }