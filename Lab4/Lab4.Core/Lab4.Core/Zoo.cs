using Lab4.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4.Core
{
    // Класс Zoo хранит животных зоопарка
    public class Zoo
    {
        private readonly List<Animal> _animals = new();

        // Добавление животного в зоопарк
        public void AddAnimal(Animal animal)
        {
            if (animal == null)
                throw new ArgumentNullException(nameof(animal));

            _animals.Add(animal);
        }

        // Возвращаем количество животных
        public int Count => _animals.Count;

        // Расчёт суммарного рациона всех животных
        public double GetTotalRation()
        {
            double total = 0;

            foreach (Animal animal in _animals)
            {
                total += animal.DailyRation;
            }

            return total;
        }

        // Вывод информации обо всех животных (полифорнизм, каждый обьект самостоятельно использует свой переопределенный вывод информации ToString())
        public void ShowAnimals()
        {
            foreach (Animal animal in _animals)
            {
                Console.WriteLine(animal);
                Console.WriteLine($"Питание: {animal.GetDiet()}");
                Console.WriteLine($"Звук: {animal.GetSound()}");
                Console.WriteLine();
            }
        }
    }
}
