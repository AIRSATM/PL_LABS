using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("введите строку(минимум 10 символов): ");
        string input = Console.ReadLine();
        
        if (string.IsNullOrEmpty(input) || input.Length < 9)
        {
            Console.WriteLine("строка короткая");
            Console.WriteLine();
            return;
        }
        
        Console.WriteLine($"\nстрока: \"{input}\"");
        Console.WriteLine($"длина строки: {input.Length} символов");
        Console.WriteLine();
        
        Console.WriteLine("=извлечение подстрок=");
        
        int length1 = Math.Max(2, input.Length / 4); // минимум 2 символа
        string substring1 = input.Substring(0, length1);
        Console.WriteLine($"подстрока 1: извлекаем с позиции 0, длина {length1}");
        Console.WriteLine($"результат: \"{substring1}\"");
        
        int startPos2 = input.Length / 3;
        int length2 = Math.Max(1, input.Length / 3);
        // проверяем чтобы не вылезти за конец строки
        if (startPos2 + length2 > input.Length)
        {
            length2 = input.Length - startPos2;
        }
        string substring2 = input.Substring(startPos2, length2);
        Console.WriteLine($"подстрока 2: извлекаем с позиции {startPos2}, длина {length2}");
        Console.WriteLine($"результат: \"{substring2}\"");
        
        int length3 = Math.Max(3, input.Length / 5); // минимум 3 символа
        int startPos3 = Math.Max(0, input.Length - length3);
        string substring3 = input.Substring(startPos3);
        Console.WriteLine($"подстрока 3: извлекаем с позиции {startPos3} до конца");
        Console.WriteLine($"результат: \"{substring3}\"");
        
        Console.WriteLine();
        
        Console.WriteLine("=анализ длин подстрок=");
        Console.WriteLine($"длина подстроки 1: {substring1.Length} символов");
        Console.WriteLine($"длина подстроки 2: {substring2.Length} символов");
        Console.WriteLine($"длина подстроки 3: {substring3.Length} символов");
        Console.WriteLine();
        
        string[] substrings = { substring1, substring2, substring3 };
        string[] parts = { "подстрока 1", "подстрока 2", "подстрока 3" };
        
        Console.WriteLine("=сортировка по длине=");
        
        var sorted = substrings
            .Select((str, index) => new { 
                Text = str, 
                Part = parts[index], 
                Length = str.Length,
                Index = index 
            })
            .OrderBy(x => x.Length)  // сортируем по длине
            .ThenBy(x => x.Index)    // если длина такая же, оставляем как было
            .ToArray();
        
        Console.WriteLine("результат сортировки:");
        for (int i = 0; i < sorted.Length; i++)
        {
            var item = sorted[i];
            Console.WriteLine($"{i + 1}. {item.Part}: \"{item.Text}\" " +
                            $"(длина: {item.Length} символов)");
        }
        
        Console.WriteLine();
        Console.WriteLine("=доп.информация=");
        
        var shortest = sorted.First();
        var longest = sorted.Last();
        
        Console.WriteLine($"самая короткая подстрока: \"{shortest.Text}\" " +
                         $"({shortest.Length} символов)");
        Console.WriteLine($"самая длинная подстрока: \"{longest.Text}\" " +
                         $"({longest.Length} символов)");
        
        var groupsByLength = sorted.GroupBy(x => x.Length).ToArray();
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
