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
            List<string> text = new List<string>();
            List<string> allStats = new List<string>();
            while (true)
            {
                Console.WriteLine("Введите текст.");
                string input = Console.ReadLine();
                if (input >= 100)
                {
                    Console.WriteLine("Верный ввод.");
                }
                else
                {
                    Console.WriteLine("Неверный ввод.");
                    continue;
                }
                char[] separators = new char[] { ' ', '.', ',', '!', '?', ':', ';', '-', '\n', '\r' };
                string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                int wordCount = words.Length;
                string shortestWord = words[0];
                string longestWord = words[0];
                for (int i = 1; i < words.Length; i++)
                {
                    if (words[i].Length < shortestWord.Length)
                    {
                        shortestWord = words[i];
                    }
                    if (words[i].Length > longestWord.Length)
                    {
                        longestWord = words[i];
                    }
                }
                int sentenceCount = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == '.' || input[i] == '!' || input[i] == '?')
                    {
                        sentenceCount++;
                    }
                }
                string vowels = "аеёиоуыэюяaeiouy";
                int vowelCount = 0;
                int consonantCount = 0;

                for (int i = 0; i < input.Length; i++)
                {
                    char c = char.ToLower(input[i]);
                    if (vowels.IndexOf(c) != -1)
                    {
                        vowelCount++;
                    }
                    else if (char.IsLetter(c))
                    {
                        consonantCount++;
                    }
                }
                Dictionary<char, int> letterFreq = new Dictionary<char, int>();
                for (int i = 0; i < input.Length; i++)
                {
                    char c = char.ToLower(input[i]);
                    if (char.IsLetter(c))
                    {
                        if (letterFreq.ContainsKey(c))
                        {
                            letterFreq[c]++;
                        }
                        else
                        {
                            letterFreq[c] = 1;
                        }
                    }
                }
                string currentStats = "Статистика текста";
                currentStats += "Количество слов: " + wordCount;
                currentStats += "Самое короткое слово: " + shortestWord;
                currentStats += "Самое длинное слово: " + longestWord;
                currentStats += "Количество предложений: " + sentenceCount;
                currentStats += "Гласных: " + vowelCount + ", Согласных: " + consonantCount;
                currentStats += "Частота букв:\n";
                foreach (KeyValuePair<char, int> pair in letterFreq)
                {
                    currentStats += pair.Key + " - " + pair.Value;
                }
                allStats.Add(currentStats);
                Console.WriteLine(currentStats);
                if (allStats.Count > 1)
                {
                    Console.WriteLine("Вывести статистику по прошлым текстам? (да/нет)");
                    string showPast = Console.ReadLine();
                    if (showPast.ToLower() == "да")
                    {
                        for (int i = 0; i < allStats.Count - 1; i++)
                        {
                            Console.WriteLine(allStats[i]);
                        }
                    }
                }
                Console.WriteLine("\nПродолжить работу с новым текстом? (да/нет)");
                string answer = Console.ReadLine();

                if (answer.ToLower() != "да")
                {
                    break;
                }
            }

        }
    }
}
