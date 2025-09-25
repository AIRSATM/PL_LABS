// ЛАБА по классам по ЯП
using System;

// Класс для представления точки на плоскости
public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
    
    public Point(double x = 0, double y = 0)
    {
        X = x;
        Y = y;
    }
    
    public override string ToString()
    {
        return $"({X:F2}, {Y:F2})";
    }
}

// Основной класс Прямоугольник
public class Rectangle
{
    private Point pointA; // Координаты левого нижнего угла
    private double sideA; // Длина горизонтальной стороны
    private double sideB; // Длина вертикальной стороны

    // 1) Конструкторы
    // Конструктор по умолчанию - создает единичный квадрат в начале координат
    public Rectangle()
    {
        pointA = new Point(0, 0);
        sideA = 1;
        sideB = 1;
    }
    
    // Конструктор с аргументами
    public Rectangle(double x, double y, double a, double b)
    {
        if (a <= 0 || b <= 0)
            throw new ArgumentException("Длины сторон должны быть положительными");
        
        pointA = new Point(x, y);
        sideA = a;
        sideB = b;
    }
    
    // Конструктор с точкой
    public Rectangle(Point point, double a, double b)
    {
        if (a <= 0 || b <= 0)
            throw new ArgumentException("Длины сторон должны быть положительными");
        
        pointA = new Point(point.X, point.Y);
        sideA = a;
        sideB = b;
    }

    // 2) Методы ввода/вывода
    public void Input()
    {
        Console.WriteLine("=== Ввод данных прямоугольника ===");
        
        Console.Write("Введите X-координату точки A (левый нижний угол): ");
        double x = Convert.ToDouble(Console.ReadLine());
        
        Console.Write("Введите Y-координату точки A (левый нижний угол): ");
        double y = Convert.ToDouble(Console.ReadLine());
        
        Console.Write("Введите длину горизонтальной стороны (a): ");
        double a = Convert.ToDouble(Console.ReadLine());
        
        Console.Write("Введите длину вертикальной стороны (b): ");
        double b = Convert.ToDouble(Console.ReadLine());
        
        if (a <= 0 || b <= 0)
        {
            Console.WriteLine("Ошибка: длины сторон должны быть положительными!");
            return;
        }
        
        pointA = new Point(x, y);
        sideA = a;
        sideB = b;
        
        Console.WriteLine("Данные успешно введены!");
    }
    
    public void Output()
    {
        Console.WriteLine("=== Информация о прямоугольнике ===");
        Console.WriteLine($"Точка A (левый нижний угол): {pointA}");
        Console.WriteLine($"Горизонтальная сторона: {sideA:F2}");
        Console.WriteLine($"Вертикальная сторона: {sideB:F2}");
        Console.WriteLine($"Координаты углов:");
        Console.WriteLine($"  A (лев.низ): {pointA}");
        Console.WriteLine($"  B (прав.низ): ({pointA.X + sideA:F2}, {pointA.Y:F2})");
        Console.WriteLine($"  C (прав.верх): ({pointA.X + sideA:F2}, {pointA.Y + sideB:F2})");
        Console.WriteLine($"  D (лев.верх): ({pointA.X:F2}, {pointA.Y + sideB:F2})");
    }

    // 3) Нахождение площади
    public double GetArea()
    {
        return sideA * sideB;
    }

    // 4) Нахождение радиуса описанной окружности
    // Для прямоугольника радиус описанной окружности = половина диагонали
    public double GetCircumscribedRadius()
    {
        double diagonal = Math.Sqrt(sideA * sideA + sideB * sideB);
        return diagonal / 2.0;
    }

    // 5) Проверка, является ли прямоугольник квадратом
    public bool IsSquare()
    {
        // Используем небольшую погрешность для сравнения вещественных чисел
        return Math.Abs(sideA - sideB) < 1e-10;
    }

    // 6) Произведение прямоугольника на число
    public Rectangle MultiplyByNumber(double multiplier)
    {
        if (multiplier <= 0)
            throw new ArgumentException("Множитель должен быть положительным");
        
        // Точка A не изменяется, стороны умножаются на число
        return new Rectangle(pointA.X, pointA.Y, sideA * multiplier, sideB * multiplier);
    }

    // 7) Проверка равенства прямоугольников (с учетом параллельного переноса)
    public bool Equals(Rectangle other)
    {
        if (other == null) return false;
        
        // Прямоугольники равны, если имеют одинаковые размеры
        // (координаты могут отличаться из-за параллельного переноса)
        return Math.Abs(sideA - other.sideA) < 1e-10 && 
               Math.Abs(sideB - other.sideB) < 1e-10;
    }

    // 8) Проверка, расположен ли прямоугольник полностью в первой четверти
    public bool IsInFirstQuadrant()
    {
        // Первая четверть: x >= 0, y >= 0
        // Проверяем все четыре угла
        return pointA.X >= 0 && pointA.Y >= 0 && // левый нижний
               (pointA.X + sideA) >= 0 && pointA.Y >= 0 && // правый нижний
               (pointA.X + sideA) >= 0 && (pointA.Y + sideB) >= 0 && // правый верхний
               pointA.X >= 0 && (pointA.Y + sideB) >= 0; // левый верхний
    }

    // 9) Проверка пересечения с другим прямоугольником
    public bool IntersectsWith(Rectangle other)
    {
        if (other == null) return false;
        
        // Границы текущего прямоугольника
        double left1 = pointA.X;
        double right1 = pointA.X + sideA;
        double bottom1 = pointA.Y;
        double top1 = pointA.Y + sideB;
        
        // Границы другого прямоугольника
        double left2 = other.pointA.X;
        double right2 = other.pointA.X + other.sideA;
        double bottom2 = other.pointA.Y;
        double top2 = other.pointA.Y + other.sideB;
        
        // Прямоугольники НЕ пересекаются, если один полностью слева, справа, сверху или снизу от другого
        return !(right1 <= left2 || right2 <= left1 || top1 <= bottom2 || top2 <= bottom1);
    }

    // 10*) Проверка пересечения с прямой
    // Прямая задается уравнением ax + by + c = 0
    public bool IntersectsWithLine(double a, double b, double c)
    {
        // Вычисляем значения функции прямой в четырех углах прямоугольника
        double[] corners = new double[4];
        
        corners[0] = a * pointA.X + b * pointA.Y + c; // левый нижний
        corners[1] = a * (pointA.X + sideA) + b * pointA.Y + c; // правый нижний
        corners[2] = a * (pointA.X + sideA) + b * (pointA.Y + sideB) + c; // правый верхний
        corners[3] = a * pointA.X + b * (pointA.Y + sideB) + c; // левый верхний
        
        // Если есть углы с разными знаками, то прямая пересекает прямоугольник
        bool hasPositive = false;
        bool hasNegative = false;
        
        foreach (double value in corners)
        {
            if (Math.Abs(value) < 1e-10) // Угол лежит на прямой
                return true;
            
            if (value > 0) hasPositive = true;
            if (value < 0) hasNegative = true;
        }
        
        return hasPositive && hasNegative;
    }

    // Дополнительные служебные методы
    public Point GetPointA() => new Point(pointA.X, pointA.Y);
    public double GetSideA() => sideA;
    public double GetSideB() => sideB;
}

// Класс для демонстрации работы
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Демонстрация работы с классом 'Прямоугольник'");
        Console.WriteLine("=".PadRight(50, '='));
        
        try
        {
            // Создание прямоугольников различными способами
            Rectangle rect1 = new Rectangle(); // по умолчанию
            Console.WriteLine("Прямоугольник по умолчанию:");
            rect1.Output();
            
            Console.WriteLine("\n" + "=".PadRight(50, '='));
            
            // Создание прямоугольника с вводом данных
            Rectangle rect2 = new Rectangle();
            rect2.Input();
            rect2.Output();
            
            Console.WriteLine("\n=== Тестирование методов ===");
            
            // Площадь
            Console.WriteLine($"Площадь: {rect2.GetArea():F2}");
            
            // Радиус описанной окружности
            Console.WriteLine($"Радиус описанной окружности: {rect2.GetCircumscribedRadius():F2}");
            
            // Проверка на квадрат
            Console.WriteLine($"Является квадратом: {(rect2.IsSquare() ? "Да" : "Нет")}");
            
            // Произведение на число
            Console.Write("Введите число для умножения размеров: ");
            double multiplier = Convert.ToDouble(Console.ReadLine());
            Rectangle multipliedRect = rect2.MultiplyByNumber(multiplier);
            Console.WriteLine("Результат умножения:");
            multipliedRect.Output();
            
            // Проверка расположения в первой четверти
            Console.WriteLine($"Расположен в первой четверти: {(rect2.IsInFirstQuadrant() ? "Да" : "Нет")}");
            
            // Создание второго прямоугольника для проверки пересечения
            Console.WriteLine("\nВведите данные второго прямоугольника для проверки пересечения:");
            Rectangle rect3 = new Rectangle();
            rect3.Input();
            
            Console.WriteLine($"Прямоугольники пересекаются: {(rect2.IntersectsWith(rect3) ? "Да" : "Нет")}");
            Console.WriteLine($"Прямоугольники равны: {(rect2.Equals(rect3) ? "Да" : "Нет")}");
            
            // Проверка пересечения с прямой
            Console.WriteLine("\nПроверка пересечения с прямой ax + by + c = 0:");
            Console.Write("Введите коэффициент a: ");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите коэффициент b: ");
            double b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите коэффициент c: ");
            double c = Convert.ToDouble(Console.ReadLine());
            
            Console.WriteLine($"Прямая пересекает прямоугольник: {(rect2.IntersectsWithLine(a, b, c) ? "Да" : "Нет")}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        
        Console.WriteLine("\nНажмите любую клавишу для завершения...");
        Console.ReadKey();
    }
}
