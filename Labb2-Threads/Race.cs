using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Labb2_Threads
{
    internal class Race
    {
        private int finishedCount = 0;
        private readonly object lockObject = new object();
        private bool raceFinished = false;
        private List<string> finishedCarNames = new List<string>();




        public void StartRace(List<Car> cars) // Metod vid start av loppet
        {
            Console.WriteLine("\u001b[31mReady....\u001b[0m");
            Thread.Sleep(1000);
            Console.WriteLine("\u001b[33mSet....\u001b[0m");
            Thread.Sleep(1000);
            Console.WriteLine("\u001b[32mGO!!!!!\u001b[0m");
            Thread.Sleep(1000);

            Console.WriteLine("\u001b[32mTHE RACE HAS STARTED\u001b[0m");
            Console.WriteLine("\u001b[33mPress ENTER to see the status of the race\u001b[0m");

            Thread statusThread = new Thread(() => Status(cars)); // Skapa och starta en tråd för att hantera status på lopp
            statusThread.Start();

            List<Thread> threads = new List<Thread>();  // Lista för att lagra trådar som hanterar problemen för varje bil

            // För varje bil, lägg till en händelselyssnare för när den avslutar loppet
            // och skapa en tråd för att hantera eventuella problem
            foreach (Car car in cars)
            {
                car.FinishedRace += RaceWinner;
                Thread thread = new Thread(car.Problems);
                threads.Add(thread);
                thread.Start();
            }

            // Vänta på att alla trådar ska avslutas
            foreach (Thread item in threads)
            {
                item.Join();
            }

        }

        // Metod för att hantera händelsen när en bil avslutar loppet
        private void RaceWinner(object sender, EventArgs e)
        {
            lock (lockObject) // Låsning för att unvika två eller flera trådar att komma åt samma data
            {
                if (!raceFinished) // Kontrollera om loppet inte redan är avslutat

                {
                    Car finishedCar = (Car)sender;
                    finishedCarNames.Add(finishedCar.Name);

                    finishedCount++;
                    if (finishedCount == 3) // Kontrollera om alla bilar har avslutat loppet

                    {
                        raceFinished = true;

                        if (finishedCarNames.Count >= 3) // Skriv ut vinnaren och de förlorande bilarna

                        {
                            Console.WriteLine("\u001b[32m--------------- THE WINNER IS: " + finishedCarNames[0] + " ---------------\u001b[0m");
                            Console.WriteLine("\u001b[31mLosers (2nd and 3rd place, in order): " + string.Join(", ", finishedCarNames.GetRange(1, 2)) + "\u001b[0m");
                            Console.WriteLine("All cars have finished the race. Exiting...");
                        }

                        Environment.Exit(0); 
                    }
                }
            }
        }


        // Metod för att visa status av loppet
        private void Status(List<Car> cars)
        {
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("\u001b[33mCurrent race status:\u001b[0m");
                    foreach (Car car in cars)
                    {
                        Console.WriteLine($"\u001b[33m{car.Name}: Has driven: {car.Distance} KM, The car speed is: {car.Speed} KM/H\u001b[0m");
                    }


                }

            }
        }
    }
}