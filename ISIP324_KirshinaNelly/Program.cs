using System;
using System.Collections.Generic;

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
                Console.WriteLine("Анализ текста");
                Console.WriteLine("1. Ввести текст и проанализировать.");
                Console.WriteLine("2. Вывести статистику по прошлым текстам.");
                Console.WriteLine("0. Выход.");
                Console.Write("Выберите пункт меню: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AnalyzeText(allStatistics);
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

        static void AnalyzeText(List<TextStatistics> allStatistics)
        {
            string input = "";
            while (true)
            {
                Console.WriteLine("Введите текст (минимум 100 символов):");
                input = Console.ReadLine();

                if (input.Length >= 100)
                {
                    Console.WriteLine("Условие ввода выполнено.");
                    break;
                }
                else
                {
                    Console.WriteLine("Условие ввода выполнено неверно. Попробуйте снова.");
                }
            }

            char[] separators = new char[] { ' ', '.', ',', '!', '?', ':', ';', '-', '(', ')' };
            string[] words = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            int wordCount = words.Length;
            Console.WriteLine($"Слов в тексте: {wordCount}");

            int sentenceCount = input.Split(new char[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
            Console.WriteLine($"Предложений в тексте: {sentenceCount}");

            string minWord = words[0];
            string maxWord = words[0];

            for (int i = 1; i < words.Length; i++)
            {
                string cleanWord = words[i].Trim('.', ',', '!', '?', ':', ';', '-', '(', ')');

                if (cleanWord.Length < minWord.Length)
                {
                    minWord = cleanWord;
                }

                if (cleanWord.Length > maxWord.Length)
                {
                    maxWord = cleanWord;
                }
            }

            Console.WriteLine($"Самое короткое слово: {minWord}");
            Console.WriteLine($"Самое длинное слово: {maxWord}");
            string vowels = "аеёиоуыэюя";
            string consonants = "бвгджзклмнпрстфхцчшщй";
            int vowelCount = 0;
            int consonantCount = 0;

            Dictionary<char, int> letterFrequency = new Dictionary<char, int>();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                char lowerC = char.ToLower(c); 

                if (vowels.Contains(lowerC))
                {
                    vowelCount++;
                }
                else if (consonants.Contains(lowerC))
                {
                    consonantCount++;
                }

                if (char.IsLetter(c))
                {
                    if (letterFrequency.ContainsKey(lowerC))
                    {
                        letterFrequency[lowerC]++;
                    }
                    else
                    {
                        letterFrequency[lowerC] = 1;
                    }
                }
            }

            Console.WriteLine($"Гласных букв: {vowelCount}");
            Console.WriteLine($"Согласных букв: {consonantCount}");

            TextStatistics currentStats = new TextStatistics
            {
                WordCount = wordCount,
                MinWord = minWord,
                SentenceCount = sentenceCount,
                VowelCount = vowelCount,
                ConsonantCount = consonantCount,
                MaxWord = maxWord,
                LetterFrequency = letterFrequency
            };

            allStatistics.Add(currentStats);
            Console.WriteLine("Статистика текущего текста сохранена в историю.");
        }


        static void ShowPastStats(List<TextStatistics> allStatistics)
        {
            Console.WriteLine("Статистика по прошлым текстам");

            if (allStatistics.Count == 0)
            {
                Console.WriteLine("История пуста. Сначала введите текст (пункт 1).");
                return;
            }

            for (int i = 0; i < allStatistics.Count; i++)
            {
                Console.WriteLine($"Текст №{i + 1}");
                Console.WriteLine($"Слов: {allStatistics[i].WordCount}");
                Console.WriteLine($"Предложений: {allStatistics[i].SentenceCount}");
                Console.WriteLine($"Короткое слово: {allStatistics[i].MinWord}");
                Console.WriteLine($"Длинное слово: {allStatistics[i].MaxWord}");
                Console.WriteLine($"Гласных: {allStatistics[i].VowelCount}, Согласных: {allStatistics[i].ConsonantCount}");

                Console.WriteLine("Частота букв:");

                foreach (KeyValuePair<char, int> pair in allStatistics[i].LetterFrequency)
                {
                    Console.Write($"{pair.Key}({pair.Value}) ");
                }
                Console.WriteLine();
            }
        }
    }
}