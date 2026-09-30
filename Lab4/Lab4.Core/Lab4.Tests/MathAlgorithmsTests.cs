using Lab4.Core;

namespace Lab4.Tests;

public class MathAlgorithmsTests
{
    // =========================================================
    // ТЕСТЫ ФАКТОРИАЛА
    // =========================================================

    [Fact]
    public void Factorial_OfZero_ReturnsOne()
    {
        // Arrange
        int n = 0;

        // Act
        long result = MathAlgorithms.Factorial(n);

        // Assert
        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(5, 120)]
    [InlineData(10, 3628800)]
    public void Factorial_OfValidNumber_ReturnsExpected(int n, long expected)
    {
        // Arrange
        // Данные уже переданы через InlineData

        // Act
        long result = MathAlgorithms.Factorial(n);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Factorial_OfTwenty_ReturnsMaximumLongFactorial()
    {
        // Arrange
        int n = 20;

        // Act
        long result = MathAlgorithms.Factorial(n);

        // Assert
        Assert.Equal(2432902008176640000, result);
    }

    // =========================================================
    // ТЕСТЫ ЧИСЕЛ ФИБОНАЧЧИ
    // =========================================================

    [Fact]
    public void Fibonacci_OfZero_ReturnsZero()
    {
        // Arrange
        int n = 0;

        // Act
        string result = MathAlgorithms.Fibonacci(n);

        // Assert
        Assert.Equal("0", result);
    }

    [Fact]
    public void Fibonacci_FirstSixNumbers_ReturnsExpectedSequence()
    {
        // Arrange
        int n = 5;

        // Act
        string result = MathAlgorithms.Fibonacci(n);

        // Assert
        Assert.Equal("0, 1, 1, 2, 3, 5", result);
    }

    [Theory]
    [InlineData(1, "0, 1")]
    [InlineData(2, "0, 1, 1")]
    [InlineData(3, "0, 1, 1, 2")]
    public void Fibonacci_OfValidNumber_ReturnsExpectedSequence(
        int n,
        string expected)
    {
        // Arrange
        // Данные переданы через InlineData

        // Act
        string result = MathAlgorithms.Fibonacci(n);

        // Assert
        Assert.Equal(expected, result);
    }

    // =========================================================
    // ТЕСТЫ ФУНКЦИИ
    // =========================================================

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    public void CalculateFunction_OfValidX_ReturnsNumber(double x)
    {
        // Arrange
        // Значение x передано через InlineData

        // Act
        double result = MathAlgorithms.CalculateFunction(x);

        // Assert
        Assert.False(double.IsNaN(result));
        Assert.False(double.IsInfinity(result));
    }

    [Fact]
    public void CalculateFunction_AtFour_ReturnsExpected()
    {
        // Arrange
        double x = 4;

        // Act
        double result = MathAlgorithms.CalculateFunction(x);

        // Assert
        double expected = -1.0 / 4 - Math.Exp(Math.Sin(4));
        Assert.Equal(expected, result, 10);
    }

    [Fact]
    public void CalculateFunction_AtZero_ThrowsException()
    {
        // Arrange
        double x = 0;

        // Act + Assert
        Assert.Throws<ArgumentException>(
            () => MathAlgorithms.CalculateFunction(x));
    }

    [Fact]
    public void CalculateFunction_AboveFour_ThrowsException()
    {
        // Arrange
        double x = 5;

        // Act + Assert
        Assert.Throws<ArgumentException>(
            () => MathAlgorithms.CalculateFunction(x));
    }

    // =========================================================
    // ТЕСТЫ РЯДА ТЕЙЛОРА
    // =========================================================

    [Fact]
    public void TaylorArctg_OfZero_ReturnsZero()
    {
        // Arrange
        double x = 0;
        double epsilon = 0.000001;

        // Act
        double result = MathAlgorithms.TaylorArctg(
            x,
            epsilon,
            out int terms);

        // Assert
        Assert.Equal(0, result);
        Assert.Equal(0, terms);
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(-0.5)]
    [InlineData(0.2)]
    public void TaylorArctg_OfValidX_MatchesMathAtan(double x)
    {
        // Arrange
        double epsilon = 0.000001;

        // Act
        double result = MathAlgorithms.TaylorArctg(
            x,
            epsilon,
            out int terms);

        // Assert
        Assert.Equal(Math.Atan(x), result, 1e-5);
        Assert.True(terms > 0);
    }

    // =========================================================
    // ТЕСТЫ КЛАССОВ ИЗ ЛАБОРАТОРНОЙ 2
    // =========================================================

    [Fact]
    public void Zoo_Empty_CountReturnsZero()
    {
        // Arrange
        Zoo zoo = new Zoo();

        // Act
        int count = zoo.Count;

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void Zoo_AddAnimal_IncreasesCount()
    {
        // Arrange
        Zoo zoo = new Zoo();

        Animal lion = new Predator(
            "Лев",
            7,
            8.5,
            "мясо");

        // Act
        zoo.AddAnimal(lion);

        // Assert
        Assert.Equal(1, zoo.Count);
    }

    [Fact]
    public void Zoo_AddAnimals_ReturnsTotalRation()
    {
        // Arrange
        Zoo zoo = new Zoo();

        Animal lion = new Predator(
            "Лев",
            7,
            8.5,
            "мясо");

        Animal elephant = new Herbivore(
            "Слон",
            10,
            25.0,
            "трава");

        // Act
        zoo.AddAnimal(lion);
        zoo.AddAnimal(elephant);

        double result = zoo.GetTotalRation();

        // Assert
        Assert.Equal(33.5, result);
    }

    [Fact]
    public void Animal_NegativeAge_ThrowsException()
    {
        // Arrange
        string name = "Лев";
        int age = -1;
        double dailyRation = 8.5;

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Predator(name, age, dailyRation, "мясо"));
    }

    [Fact]
    public void Animal_ZeroDailyRation_ThrowsException()
    {
        // Arrange
        string name = "Лев";
        int age = 7;
        double dailyRation = 0;

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new Predator(name, age, dailyRation, "мясо"));
    }

    [Fact]
    public void Zoo_AddNullAnimal_ThrowsException()
    {
        // Arrange
        Zoo zoo = new Zoo();

        // Act + Assert
        Assert.Throws<ArgumentNullException>(
            () => zoo.AddAnimal(null!));
    }
}