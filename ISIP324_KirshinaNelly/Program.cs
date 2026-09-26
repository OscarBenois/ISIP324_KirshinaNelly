using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        public enum Category { Напитки, Выпечка, Фастфуд }

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
                Console.WriteLine($"[Код: {code}] {name} / Цена: {price} / Остаток: {quantity} / Категория: {category} / В наличии: {(isThere ? "Да" : "Нет")}");
            }
        }
        static List<Product> products = new List<Product>();

        static void Main(string[] args)
        {
            products.Add(new Product("Кокакола", 150, 20, Category.Напитки));
            products.Add(new Product("Хлеб", 50, 15, Category.Выпечка));
            products.Add(new Product("Бургер", 200, 0, Category.Фастфуд));

            while (true)
            {
                Console.WriteLine("Учёт товаров в магазине");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Заказать поставку");
                Console.WriteLine("4. Продать товар");
                Console.WriteLine("5. Поиск товаров");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт от 0 до 5:");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        RemoveProduct();
                        break;
                    case "3":
                        SupplyProduct();
                        break;
                    case "4":
                        SellProduct();
                        break;
                    case "5":
                        SearchProduct();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }
        static void AddProduct()
        {
            Console.WriteLine("Добавление товара");
            string name = ReadString("Название: ");
            double price = ReadDouble("Цена: ");
            int quantity = ReadInt("Количество: ");
            Category category = ReadCategory();

            products.Add(new Product(name, price, quantity, category));
            Console.WriteLine("Товар добавлен.");
        }

        static void RemoveProduct()
        {
            Console.WriteLine("Удаление товара");
            int code = ReadInt("Введите код товара для удаления: ");
            Product found = FindByCode(code);

            if (found != null)
            {
                products.Remove(found);
                Console.WriteLine("Товар удален");
            }
            else
            {
                Console.WriteLine("Товара с таким кодом не существует");
            }
        }

        static void SupplyProduct()
        {
            Console.WriteLine("Заказ поставки");
            int code = ReadInt("Введите код товара: ");
            int amount = ReadInt("Сколько единиц поставить: ");
            Product found = FindByCode(code);

            if (found != null)
            {
                found.quantity += amount;
                Console.WriteLine("Поставка принята");
            }
            else
            {
                Console.WriteLine("Товара с таким кодом не существует");
            }

        }

        static void SellProduct()
        {
            Console.WriteLine("Продажа товара");
            int code = ReadInt("Введите код товара: ");
            int amount = ReadInt("Сколько единиц продать: ");
            Product found = FindByCode(code);

            if (found != null)
            {
                if (found.quantity >= amount)
                {
                    found.quantity -= amount;
                    Console.WriteLine("Продано");
                }
                else
                {
                    Console.WriteLine($"Ошибка: На складе только {found.quantity} шт., а вы хотите продать {amount} шт.");
                }
            }
            else
            {
                Console.WriteLine("Товара с таким кодом не существует");
            }

        }

        static void SearchProduct()
        {
            Console.WriteLine("Поиск товаров");
            Console.WriteLine("Введите код, название или категорию (Напитки, Выпечка, Фастфуд):");
            string query = Console.ReadLine().Trim().ToLower();

            Console.WriteLine("Результаты поиска:");
            int foundCount = 0;

            foreach (Product p in products)
            {
                bool matchCode = int.TryParse(query, out int code) && p.code == code;
                bool matchName = p.name.ToLower().Contains(query);
                bool matchCategory = p.category.ToString().ToLower() == query;

                if (matchCode || matchName || matchCategory)
                {
                    p.PrintInfo();
                    foundCount++;
                }
            }

            if (foundCount == 0)
            {
                Console.WriteLine("Ничего не найдено.");
            }
            Console.WriteLine();
        }
        static Product FindByCode(int code)
        {
            foreach (var p in products)
            {
                if (p.code == code) return p;
            }
            return null;
        }

        static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input)) return input;
                Console.WriteLine("Ошибка: Название не может быть пустым");
            }
        }

        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && value > 0) return value;
                Console.WriteLine("Ошибка: Введите положительное число");
            }
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value > 0) return value;
                Console.WriteLine("Ошибка: Введите целое число больше нуля");
            }
        }

        static Category ReadCategory()
        {
            Console.WriteLine("Доступные категории: Напитки, Выпечка, Фастфуд");
            while (true)
            {
                Console.Write("Категория: ");
                string input = Console.ReadLine();
                if (Enum.TryParse(input, true, out Category category)) return category;
                Console.WriteLine("Ошибка: Введите категорию из списка");
            }
        }
    }
}