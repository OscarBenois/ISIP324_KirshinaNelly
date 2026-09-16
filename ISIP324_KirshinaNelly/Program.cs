using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> products = new List<string>();
            List<double> costs = new List<double>();
            int numberofoperations = 0;
            while (true)
            {
                Console.WriteLine("Введите количество операций (от 2 до 40)");
                numberofoperations = Convert.ToInt32(Console.ReadLine());
                if (numberofoperations >= 2 && numberofoperations <= 40)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка. Введите число операций от 2 до 40.");
                }
            }
        }
    }
}