using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_KirshinaNelly
{
    internal class Program
    {
        class TextStatistics
        {
            public int WordCount;
            public string MinWord;
            public int SentenceCount;
            public int VowelCount;
            public int ConsonantCount;
            public string MaxWord;
            public Dictionary<char, int> LetterFrequency;
        }
        static void Main(string[] args)
        {
            List<TextStatistics> allStatistics = new List<TextStatistics>();

            while (true)
            {
                Console.WriteLine("1. Анализ текста.");
                Console.WriteLine("2. Просмотр статистики по прошлым текстам");
                Console.WriteLine("0. Выход");
                Console.WriteLine("Выберите пункт меню: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        NewText(allStatistics);
                        break;

                    case "2":
                        ShowPastStats(allStatistics);
                        break;

                    case "0":
                        Console.WriteLine("Досвидос.");
                        return;

                    default:
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                        break;
                }
            }
        }

        static void NewText(List<TextStatistics> allStatistics)
        {
            string input = "";
            while (true)
            {
                Console.WriteLine("Введите текст (минимум 100 символов):");
                input = Console.ReadLine();

                if (input.Length >= 100)
                {
                    Console.WriteLine("Верный ввод.");
                    break;
                }
                else
                {
                    Console.WriteLine("Невереный ввод. Попробуйте снова.");
                }
            }

            char[] separators = new char[] { ' ', '.', ',', '!', '?', ':', ';', '-', '(', ')' };
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int wordCount = words.Length;
            string shortestWord = words[0];
            string longestWord = words[0];

            for (int i = 1; i < words.Length; i++)
            {
                if (words[i].Length < shortestWord.Length)
                    shortestWord = words[i];

                if (words[i].Length > longestWord.Length)
                    longestWord = words[i];
            }

            int sentenceCount = input.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;

            string vowels = "аеёиоуыэюя";
            string consonants = "бвгджзклмнпрстфхцчшщй";
            int vowelCount = 0;
            int consonantCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (vowels.Contains(char.ToLower(input[i])))
                {
                    vowelCount++;
                }
                else if (consonants.Contains(char.ToLower(input[i])))
                {
                    consonantCount++;
                }
            }

            Dictionary<char, int> letterFreq = new Dictionary<char, int>();
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (char.IsLetter(c))
                {
                    c = char.ToLower(c);
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

            TextStatistics currentStats = new TextStatistics
            {
                WordCount = wordCount,
                MinWord = shortestWord,
                SentenceCount = sentenceCount,
                VowelCount = vowelCount,
                ConsonantCount = consonantCount,
                MaxWord = longestWord,
                LetterFrequency = letterFreq
            };

            allStatistics.Add(currentStats);
            Console.WriteLine("Статистика сохранена.\n");

            Console.WriteLine("Статистика текущего текста:");
            Console.WriteLine($"Слов: {currentStats.WordCount}");
            Console.WriteLine($"Предложений: {currentStats.SentenceCount}");
            Console.WriteLine($"Короткое слово: {currentStats.MinWord}");
            Console.WriteLine($"Длинное слово: {currentStats.MaxWord}");
            Console.WriteLine($"Гласных: {currentStats.VowelCount}, Согласных: {currentStats.ConsonantCount}");
            Console.WriteLine("Частота букв:");
            foreach (var pair in currentStats.LetterFrequency)
            {
                Console.Write($"{pair.Key}({pair.Value}) ");
            }
            Console.WriteLine(new string('-', 40));
        }
        static void ShowPastStats(List<TextStatistics> allStatistics)
        {
            if (allStatistics.Count == 0)
            {
                Console.WriteLine("\nИстория анализов пуста. Сначала введите текст (пункт 1).");
                return;
            }

            Console.WriteLine("История анализов текста.");
            for (int i = 0; i < allStatistics.Count; i++)
            {
                Console.WriteLine($"Текст №{i + 1}");
                Console.WriteLine($"Слов: {allStatistics[i].WordCount}");
                Console.WriteLine($"Предложений: {allStatistics[i].SentenceCount}");
                Console.WriteLine($"Короткое слово: {allStatistics[i].MinWord}");
                Console.WriteLine($"Длинное слово: {allStatistics[i].MaxWord}");
                Console.WriteLine($"Гласных: {allStatistics[i].VowelCount}, Согласных: {allStatistics[i].ConsonantCount}");
                Console.WriteLine("Частота букв:");

                foreach (var pair in allStatistics[i].LetterFrequency)
                {
                    Console.Write($"{pair.Key}({pair.Value}) ");
                }
                Console.WriteLine(" " new string('-', 20));
            }
        }
    }
}
