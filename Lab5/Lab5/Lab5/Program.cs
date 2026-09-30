using System;
using System.Diagnostics;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Шифр Виженера");
        Console.WriteLine();

        // Ввод исходного текста
        Console.Write("Введите текст: ");
        string text = Console.ReadLine()!.ToUpper();

        // Ввод ключа для проверки шифрования
        Console.Write("Введите ключ: ");
        string key = Console.ReadLine()!.ToUpper();

        // Шифрование
        string encrypted = VigenereEncrypt(text, key);

        Console.WriteLine();
        Console.WriteLine("Зашифрованный текст:");
        Console.WriteLine(encrypted);

        // Дешифрование
        string decrypted = VigenereDecrypt(encrypted, key);

        Console.WriteLine();
        Console.WriteLine("Расшифрованный текст:");
        Console.WriteLine(decrypted);

        Console.WriteLine();
        Console.WriteLine("Взлом ключа перебором");

        // Известное слово
        Console.Write("Введите известное слово: ");
        string knownWord = Console.ReadLine()!.ToUpper();

        // Длина неизвестного ключа
        Console.Write("Введите длину ключа: ");
        int keyLength = int.Parse(Console.ReadLine()!);

        // Количество потоков
        Console.Write("Введите количество потоков: ");
        int threadCount = int.Parse(Console.ReadLine()!);

        Console.WriteLine();

        // Последовательный перебор
        Stopwatch sw = Stopwatch.StartNew();

        string sequentialKey = BruteForceSequential(encrypted, knownWord, keyLength);
        sw.Stop();

        long sequentialTime = sw.ElapsedMilliseconds;

        Console.WriteLine("Последовательный вариант:");
        Console.WriteLine($"Найденный ключ: {sequentialKey}");
        Console.WriteLine($"Время: {sequentialTime} мс");

        // Параллельный перебор
        sw.Restart();

        string parallelKey = BruteForceParallel(encrypted, knownWord, keyLength, threadCount);
        sw.Stop();

        long parallelTime = sw.ElapsedMilliseconds;

        Console.WriteLine();
        Console.WriteLine("Параллельный вариант:");
        Console.WriteLine($"Найденный ключ: {parallelKey}");
        Console.WriteLine($"Время: {parallelTime} мс");

        // Проверка совпадения результатов
        Console.WriteLine();
        Console.WriteLine("Проверка результатов:");

        if (sequentialKey == parallelKey)
        {
            Console.WriteLine("Результаты совпадают.");
        }
        else
        {
            Console.WriteLine("Результаты НЕ совпадают.");
        }

        // Расчёт ускорения
        double speedup = 0;

        if (parallelTime > 0)
        {
            speedup = (double)sequentialTime / parallelTime;
        }

        // Таблица замеров
        Console.WriteLine();
        Console.WriteLine("Таблица замеров");
        Console.WriteLine();

        Console.WriteLine(
            "{0,-20} {1,-20} {2,-20} {3,-15}",
            "Вариант",
            "Время, мс",
            "Потоки",
            "Ускорение");

        Console.WriteLine(
            "{0,-20} {1,-20} {2,-20} {3,-15}",
            "Последовательный",
            sequentialTime,
            1,
            "1,00");

        Console.WriteLine(
            "{0,-20} {1,-20} {2,-20} {3,-15:F2}",
            "Параллельный",
            parallelTime,
            threadCount,
            speedup);
    }

    // ШИФРОВАНИЕ ВИЖЕНЕРА
    static string VigenereEncrypt(string text, string key)
    {
        string result = "";

        int keyIndex = 0;

        for (int i = 0; i < text.Length; i++)
        {
            char symbol = text[i];

            if (symbol >= 'A' && symbol <= 'Z')
            {
                int textCode = symbol - 'A';
                int keyCode = key[keyIndex % key.Length] - 'A';

                int encryptedCode = (textCode + keyCode) % 26;

                result += (char)('A' + encryptedCode);

                keyIndex++;
            }
            else
            {
                result += symbol;
            }
        }

        return result;
    }

    // ДЕШИФРОВАНИЕ ВИЖЕНЕРА
    static string VigenereDecrypt(string text, string key)
    {
        string result = "";

        int keyIndex = 0;

        for (int i = 0; i < text.Length; i++)
        {
            char symbol = text[i];

            if (symbol >= 'A' && symbol <= 'Z')
            {
                int textCode = symbol - 'A';
                int keyCode = key[keyIndex % key.Length] - 'A';

                int decryptedCode = (textCode - keyCode + 26) % 26;

                result += (char)('A' + decryptedCode);

                keyIndex++;
            }
            else
            {
                result += symbol;
            }
        }

        return result;
    }

    // ПОСЛЕДОВАТЕЛЬНЫЙ ПЕРЕБОР
    static string BruteForceSequential(
        string encryptedText,
        string knownWord,
        int keyLength)
    {
        long totalKeys = 1;

        for (int i = 0; i < keyLength; i++)
        {
            totalKeys *= 26;
        }

        for (long number = 0; number < totalKeys; number++)
        {
            string key = NumberToKey(number, keyLength);

            string decrypted = VigenereDecrypt(
                encryptedText,
                key);

            if (decrypted.Contains(knownWord))
            {
                return key;
            }
        }

        return "НЕ НАЙДЕН";
    }

    // ПАРАЛЛЕЛЬНЫЙ ПЕРЕБОР
    static string BruteForceParallel(
        string encryptedText,
        string knownWord,
        int keyLength,
        int threadCount)
    {
        long totalKeys = 1;

        for (int i = 0; i < keyLength; i++)
        {
            totalKeys *= 26;
        }

        string foundKey = "НЕ НАЙДЕН";

        object lockObject = new object();

        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = threadCount
        };

        Parallel.For(
            0L,
            totalKeys,
            options,
            (number, state) =>
            {
                string key = NumberToKey(number, keyLength);

                string decrypted = VigenereDecrypt(
                    encryptedText,
                    key);

                if (decrypted.Contains(knownWord))
                {
                    lock (lockObject)
                    {
                        if (foundKey == "НЕ НАЙДЕН")
                        {
                            foundKey = key;
                            state.Stop();
                        }
                    }
                }
            });

        return foundKey;
    }

    // ПРЕОБРАЗОВАНИЕ ЧИСЛА В КЛЮЧ
    static string NumberToKey(long number, int keyLength)
    {
        char[] key = new char[keyLength];

        for (int i = keyLength - 1; i >= 0; i--)
        {
            key[i] = (char)('A' + number % 26);
            number /= 26;
        }

        return new string(key);
    }
}