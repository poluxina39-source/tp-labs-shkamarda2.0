using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    // Хищник
    public class Predator : Animal
    {
        // Закрытое поле с видом корма
        private readonly string _food;

        public string Food => _food;

        public Predator(string name, int age, double dailyRation, string food) : base(name, age, dailyRation)
        {
            _food = food;

            if (string.IsNullOrWhiteSpace(food))
                throw new ArgumentException(nameof(food));
        }

        public override string GetDiet()
        {
            return $"хищник, корм: {Food}";
        }

        public override string GetSound()
        {
            return "Рычит";
        }

        public override string ToString()
        {
            return $"Хищник: {Name}, возраст {Age} лет, " +
                   $"рацион {DailyRation:F1} кг/день, корм: {Food}";
        }
    }
}
