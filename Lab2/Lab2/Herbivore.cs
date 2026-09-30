using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    // Травоядное
    public class Herbivore : Animal
    {
        // Закрытое поле с видом корма
        private readonly string _food;

        public string Food => _food;

        public Herbivore(string name, int age, double dailyRation, string food) : base(name, age, dailyRation)
        {
            _food = food;

            if (string.IsNullOrWhiteSpace(food))
                throw new ArgumentException(nameof(food));
        }

        public override string GetDiet()
        {
            return $"травоядное, корм: {Food}";
        }

        public override string GetSound()
        {
            return "Издаёт звук травоядного";
        }

        public override string ToString()
        {
            return $"Травоядное: {Name}, возраст {Age} лет, " +
                   $"рацион {DailyRation:F1} кг/день, корм: {Food}";
        }
    }
}
