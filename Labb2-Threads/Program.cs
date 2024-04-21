namespace Labb2_Threads
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Skapar 3 car objekt med namn och hastighet
            Car car1 = new Car("Isac", 120);
            Car car2 = new Car("Anas", 120);
            Car car3 = new Car("Reidar", 120);

            // Skapar lista för att hålla car objekten
            List<Car> cars = new List<Car>
            {
                car1,
                car2,
                car3
            };

            // Skapar race objekt samt startar racet med listan av cars
            Race race = new Race();
            race.StartRace(cars);

        }
    }
}