using Lab2;

class Program
{
    static void Main()
    {
        // Создаём объект зоопарка
        Zoo zoo = new Zoo();

        // Создаём 5 животных разных классов
        Predator lion = new Predator(
            "Лев",
            7,
            8.5,
            "мясо");

        Predator tiger = new Predator(
            "Тигр",
            5,
            9.0,
            "мясо");

        Herbivore elephant = new Herbivore(
            "Слон",
            10,
            25.0,
            "трава");

        Herbivore giraffe = new Herbivore(
            "Жираф",
            6,
            12.0,
            "листья");

        Bird eagle = new Bird(
            "Орёл",
            4,
            1.5,
            true);

        // Добавляем животных в зоопарк
        zoo.AddAnimal(lion);
        zoo.AddAnimal(tiger);
        zoo.AddAnimal(elephant);
        zoo.AddAnimal(giraffe);
        zoo.AddAnimal(eagle);

        // Выводим информацию обо всех животных
        Console.WriteLine("ЖИВОТНЫЕ ЗООПАРКА");
        zoo.ShowAnimals();

        // Рассчитываем суммарный рацион зоопарка.
        Console.WriteLine("СУММАРНЫЙ РАЦИОН");
        Console.WriteLine($"Всего животных: {zoo.Count}");
        Console.WriteLine($"Общий рацион: {zoo.GetTotalRation():F1} кг/день");
    }
}