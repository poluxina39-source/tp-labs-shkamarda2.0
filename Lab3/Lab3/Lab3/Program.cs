using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь к папке: ");
        string folder = Console.ReadLine()!;

        if (!Directory.Exists(folder))
        {
            Console.WriteLine("Папка не существует.");
            return;
        }

        try
        {
            var files = Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories)
                .Select(p => new FileInfo(p));

            var top = files
                .GroupBy(f => f.Extension.ToLower())
                .OrderByDescending(g => g.Count())
                .Take(5);

            Console.WriteLine();
            Console.WriteLine("Топ-5 расширений:");

            foreach (var g in top)
            {
                long totalSize = g.Sum(f => f.Length);

                Console.WriteLine(
                    $"{(g.Key == "" ? "(без расширения)" : g.Key),-15} " +
                    $"{g.Count(),5} файлов   " +
                    $"{totalSize / 1024.0:F2} КБ"
                );
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Нет доступа к указанной папке.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}