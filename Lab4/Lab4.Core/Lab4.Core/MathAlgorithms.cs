namespace Lab4.Core;
public static class MathAlgorithms
{
    public static long Factorial(int n)
    {
        long result = 1;

        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    public static string Fibonacci(int n)
    {
        long first = 0;
        long second = 1;

        string result = "";

        for (int i = 0; i <= n; i++)
        {
            if (i == 0)
            {
                result += "0";
            }
            else if (i == 1)
            {
                result += ", 1";
            }
            else
            {
                long next = first + second;
                first = second;
                second = next;

                result += ", " + next;
            }
        }

        return result;
    }

    public static double CalculateFunction(double x)
    {
        if (x <= 0 || Math.Log(4 / x) < 0)
        {
            throw new ArgumentException(
                "Функция не определена. Необходимо, чтобы 0 < x <= 4."
            );
        }

        double result = Math.Sqrt(Math.Log(4 / x))
                     - 1 / x
                     - Math.Exp(Math.Sin(x));

        return result;
    }

    public static double TaylorArctg(double x, double epsilon, out int terms)
    {
        double sum = 0;
        double term = x;
        int n = 0;

        while (Math.Abs(term) > epsilon)
        {
            sum += term;
            n++;

            term = Math.Pow(x, 2 * n + 1) / (2 * n + 1);

            if (n % 2 == 1)
            {
                term = -term;
            }
        }

        terms = n;

        return sum;
    }
}