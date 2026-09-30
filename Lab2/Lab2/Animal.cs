using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    // Абстрактный базовый класс для всех животных
    public abstract class Animal
    {
        // Инкапсуляция (данные хранятся в закрытых полях)
        private readonly string _name;
        private readonly int _age;
        private readonly double _dailyRation;

        public string Name => _name;
        public int Age => _age;
        public double DailyRation => _dailyRation;


        public Animal(string name, int age, double dailyRation)
        {
            _name = name;
            _age = age;
            _dailyRation = dailyRation;

            // Проверка корректности данных
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age));

            if (dailyRation <= 0)
                throw new ArgumentOutOfRangeException(nameof(dailyRation));
        }

        // Каждый класс-наследник самостоятельно определяет питание.
        public abstract string GetDiet();

        public virtual string GetSound()
        {
            return "Издаёт звук";
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {Name}, возраст {Age} лет, " +
                   $"рацион {DailyRation:F1} кг/день";
        }
    }
}
