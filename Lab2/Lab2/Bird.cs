using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    // птица
    public class Bird : Animal
    {
        public bool CanFly { get; }

        public Bird(string name, int age, double dailyRation, bool canFly) : base(name, age, dailyRation)
        {
            CanFly = canFly;
        }

        public override string GetDiet()
        {
            return "птица, питание зерном и кормом";
        }

        public override string GetSound()
        {
            return "Щебечет";
        }

        public override string ToString()
        {
            string flight = CanFly ? "летает" : "не летает";

            return $"Птица: {Name}, возраст {Age} лет, " +
                   $"рацион {DailyRation:F1} кг/день, {flight}";
        }
    }
}
