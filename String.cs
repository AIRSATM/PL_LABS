using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Введите строку: ");
        string input = Console.ReadLine();

        List<string> substrings = new List<string>();

        int tmp = 0;
        for (int i = 0; i <= input.Length; i++)
        {
            if (i == input.Length || input[i] == ' ')
            {
                int length = i - tmp;
                if (length > 0)
                {
                    string word = input.Substring(tmp, length);
                    substrings.Add(word);
                }
                tmp = i + 1;
            }
        }

        Console.WriteLine("\n=== Этап 1: Найденные подстроки ===");
        foreach (var s in substrings)
        {
            Console.WriteLine($"Подстрока: \"{s}\" (Длина: {s.Length})");
        }

        // Берем только первые 3 подстроки (если их меньше, то сколько есть)
        var selected = substrings.Take(3).ToList();

        Console.WriteLine("\n=== Этап 2: Выбраны 3 подстроки для сравнения ===");
        foreach (var s in selected)
        {
            Console.WriteLine($"Выбрана: \"{s}\" (Длина: {s.Length})");
        }

        // Сортировка выбранных подстрок по длине
        var sorted = selected.OrderBy(s => s.Length).ToList();

        Console.WriteLine("\n=== Этап 3: Сравнение и сортировка по возрастанию длины ===");
        foreach (var s in sorted)
        {
            Console.WriteLine($"\"{s}\" (Длина: {s.Length})");
        }

        Console.WriteLine("\nПрограмма завершена: обработаны только первые 3 подстроки.");
    }
}
