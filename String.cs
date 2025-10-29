using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("введите строку(минимум 10 символов): ");
        string sourceString = Console.ReadLine();
        
        if (string.IsNullOrEmpty(sourceString) || sourceString.Length < 6)
        {
            Console.WriteLine("строка короткая");
            Console.WriteLine();
            return;
        }
        
        Console.WriteLine($"\nстрока: \"{sourceString}\"");
        Console.WriteLine($"длина строки: {sourceString.Length} символов");
        Console.WriteLine();
        
        Console.WriteLine("=извлечение подстрок=");
        
        int length1 = Math.Max(2, sourceString.Length / 4); // минимум 2 символа
        string substring1 = sourceString.Substring(0, length1);
        Console.WriteLine($"подстрока 1: извлекаем с позиции 0, длина {length1}");
        Console.WriteLine($"результат: \"{substring1}\"");
        
        int startPos2 = sourceString.Length / 3;
        int length2 = Math.Max(1, sourceString.Length / 3);
        // Убеждаемся, что не выходим за границы строки
        if (startPos2 + length2 > sourceString.Length)
        {
            length2 = sourceString.Length - startPos2;
        }
        string substring2 = sourceString.Substring(startPos2, length2);
        Console.WriteLine($"подстрока 2: извлекаем с позиции {startPos2}, длина {length2}");
        Console.WriteLine($"результат: \"{substring2}\"");
        
        int length3 = Math.Max(3, sourceString.Length / 5); // минимум 3 символа
        int startPos3 = Math.Max(0, sourceString.Length - length3);
        string substring3 = sourceString.Substring(startPos3);
        Console.WriteLine($"подстрока 3: извлекаем с позиции {startPos3} до конца");
        Console.WriteLine($"результат: \"{substring3}\"");
        
        Console.WriteLine();
        
        Console.WriteLine("=анализ длин подстрок=");
        Console.WriteLine($"длина подстроки 1: {substring1.Length} символов");
        Console.WriteLine($"длина подстроки 2: {substring2.Length} символов");
        Console.WriteLine($"длина подстроки 3: {substring3.Length} символов");
        Console.WriteLine();
        
        string[] substrings = { substring1, substring2, substring3 };
        string[] substringLabels = { "подстрока 1", "подстрока 2", "подстрока 3" };
        
        Console.WriteLine("=сортировка по длине=");
        
        var indexedSubstrings = substrings
            .Select((str, index) => new { 
                Text = str, 
                OriginalLabel = substringLabels[index], 
                Length = str.Length,
                Index = index 
            })
            .OrderBy(x => x.Length)  // сортируем по длине
            .ThenBy(x => x.Index)    // при равной длине сохраняем исходный порядок
            .ToArray();
        
        Console.WriteLine("результат сортировки:");
        for (int i = 0; i < indexedSubstrings.Length; i++)
        {
            var item = indexedSubstrings[i];
            Console.WriteLine($"{i + 1}. {item.OriginalLabel}: \"{item.Text}\" " +
                            $"(длина: {item.Length} символов)");
        }
        
        Console.WriteLine();
        Console.WriteLine("=доп.информация=");
        
        var shortest = indexedSubstrings.First();
        var longest = indexedSubstrings.Last();
        
        Console.WriteLine($"самая короткая подстрока: \"{shortest.Text}\" " +
                         $"({shortest.Length} символов)");
        Console.WriteLine($"самая длинная подстрока: \"{longest.Text}\" " +
                         $"({longest.Length} символов)");
        
        var groupsByLength = indexedSubstrings.GroupBy(x => x.Length).ToArray();
        if (groupsByLength.Any(g => g.Count() > 1))
        {
            Console.WriteLine("\nподстроки с одинаковой длиной:");
            foreach (var group in groupsByLength.Where(g => g.Count() > 1))
            {
                Console.WriteLine($"длина {group.Key}: " + 
                    string.Join(", ", group.Select(x => $"\"{x.Text}\"")));
            }
        }
    }
}
