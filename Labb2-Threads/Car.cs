using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_Threads
{
    internal class Car
    {
        public string Name { get; set; }
        public int Distance { get; set; }
        public int Speed { get; set; }
        public bool Finish { get; set; }
        public event EventHandler FinishedRace; // Event för när en bil går i mål

        public Car(string name, int speed)
        {
            Name = name;
            Distance = 0;
            Speed = speed;
            Finish = false;
        }

        public void Problems() // Metod för att stimulera problem under racet
        {
            while (!Finish && Distance < 3000) // Loopa till bilarna kommer i mål/når 3000
            {
                Thread.Sleep(1000);
                Random random = new Random(); // Skapa en "random number generator"
                int problem = random.Next(1, 51); 

                if (problem == 1)
                {
                    Console.WriteLine($"\u001b[31m{Name}'s has no fuel left! It will take 30 seconds to fill it up! \u001b[0m");
                    Thread.Sleep(30000); // 30sec cooldown
                }
                else if (problem <= 3)
                {
                    Console.WriteLine($"\u001b[31m{Name} has a flat tire! It will take 20 seconds to change tire! \u001b[0m");
                    Thread.Sleep(20000); // 20sec cooldown
                }
                else if (problem <= 5)
                {
                    Console.WriteLine($"\u001b[31mA bird hit the window of {Name}! It will take 10 seconds to clean it off! \u001b[0m");
                    Thread.Sleep(10000); // 10sec cooldown
                }
                else if (problem <= 10)
                {
                    Console.WriteLine($"\u001b[31mThere is a problem with the engine on {Name} car. The car slowed down with 1 KM/H \u001b[0m");
                    Speed--;
                }

                Distance += Speed; // Uppdatera distansen gjord av bilens hastighet


            }

            if (Distance >= 3000) // Kolla om bilen har klarat racet
            {
                Finish = true;
                Console.WriteLine($"\u001b[34m{Name} has finished the race.\u001b[0m");
                FinishedRace?.Invoke(this, EventArgs.Empty); // Anropas såvida den inte är null
            }
        }
    }
}