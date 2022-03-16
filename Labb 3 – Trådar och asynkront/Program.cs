using System;
using System.Threading;
using System.Threading.Tasks;

namespace Labb_3___Trådar_och_asynkront
{
    class Program
    {
        public static DateTime dt;
        public static DateTime CarDT1;
        public static DateTime CarDT2;
        public static int distance1;
        public static int distance2;

        static void Main(string[] args)
        {
            Car C1 = new Car(1,"McQueen",10, 120);
            Car C2 = new Car(2,"Jackson_Storm", 10, 120);

            Console.WriteLine("Today the following cars will race: {0} vs {1}", C1.CarName, C2.CarName);
            Console.WriteLine("Press Enter To Start The Race!");
            Console.ReadKey();
            Console.WriteLine("Race Starting in:");
            Console.WriteLine("3");
            Thread.Sleep(1000);
            Console.WriteLine("2");
            Thread.Sleep(1000);
            Console.WriteLine("1");
            Thread.Sleep(1000);
            Console.WriteLine("GO");

            ProgressTracker(C1, C2);
        }
        public static void ProgressTracker(Car C1,Car C2)
        {
            Console.WriteLine(C1.CarName + " Has started the race");
            Console.WriteLine(C2.CarName + " Has started the race");
            dt = DateTime.Now;
            bool statsgo = true;
            Task masterTask = Task.Run(() =>
            {
                    if (!Console.KeyAvailable)
                    {
                        while (statsgo)
                        {
                            if (Console.ReadKey().Key == ConsoleKey.A)
                            {
                                printStatistics(0, 16, C1, distance1);
                            }
                            else if (Console.ReadKey().Key == ConsoleKey.D)
                            {
                                printStatistics(0, 24, C2, distance2);
                            }
                        }
                    }
            });
            Task task1 = Task.Run(() =>
            {
                int RaceDistance = 100;
                int EngineDur = 0;
                for (distance1 = C1.Speed; distance1 < RaceDistance; distance1++)
                {
                    Car1(C1, EngineDur, RaceDistance);
                }

                CarDT1 = DateTime.Now;
                statsgo = false;
            });

            Task task2 = Task.Run(() =>
            {
                int RaceDistance = 100;
                int EngineDur = 0;
                for (distance2 = C2.Speed; distance2 < RaceDistance; distance2++)
                {
                    Car2(C2, EngineDur, RaceDistance);
                }

                CarDT2 = DateTime.Now;
                statsgo = false;
            });
            
            task1.Wait();
            task2.Wait();
            masterTask.Wait();


            double TimeFromStart1 = (CarDT1 - dt).Seconds;
            double TimeFromStart2 = (CarDT2 - dt).Seconds;
            Console.Clear();
            Console.WriteLine(C1.CarName + " finished in " + TimeFromStart1 + " Seconds"+"\n"+ C2.CarName + " finished in " + TimeFromStart2 + " Seconds");
            if (TimeFromStart1 < TimeFromStart2)
            {
                Console.WriteLine("\n"+C1.CarName + " Is the winner!!!");
            }
            else if(TimeFromStart1 > TimeFromStart2)
            {
                Console.WriteLine("\n" + C2.CarName + " Is the winner!!!");
            }
            else
            {
                Console.WriteLine("\n" + "Its a draw!!!");
            }
        }
        public static void Car1(Car car1, int EngineDur, int RaceDistance)
        {
            
                int SleepTime = 0;
                double elapsedtime = (DateTime.Now - dt).Seconds;
                if (elapsedtime == 30)
                {
                    bool engineproblem = Event(car1, 18);
                    if (engineproblem == true)
                    {
                        car1.FSpeed = car1.FSpeed -1;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Random rnd = new Random();
                    int random = rnd.Next(0, 50);
                    if (random == 1) // 1%
                    {
                        lock (_sync) { PrintFaults(0, 18, car1, "Slut på bensin!"); }
                        SleepTime = SleepTime + 3000;
                    }
                    else if (random >= 2 && random <= 3) // 2%
                    {
                        lock (_sync) { PrintFaults(0, 18, car1, "Punka!"); }
                        SleepTime = SleepTime + 2000;
                    }
                    else if (random >= 4 && random <= 9) // 5%
                    {
                        lock (_sync) { PrintFaults(0, 18, car1, "Fågel!"); }
                        SleepTime = SleepTime + 1000;
                    }
                    else if (random >= 10 && random <= 20)
                    {
                        lock (_sync) { PrintFaults(0, 18, car1, "Motorfel!"); }
                        car1.FSpeed = car1.FSpeed -1;
                    }
                    else
                    {

                    }
                }

                Thread.Sleep(100);
                Thread.Sleep(SleepTime);
                car1.Speed = car1.Speed + EngineDur;
                lock (_sync) {PrintFaults(0, 18, car1, "                          "); }
                lock (_sync) { PrintCarAt(distance1, 20, car1); }
        }
    
        public static void Car2(Car car2, int EngineDur, int RaceDistance)
        {


                int SleepTime = 0;
                double elapsedtime = (DateTime.Now - dt).Seconds;
                if (elapsedtime == 30)
                {
                    bool engineproblem = Event(car2, 26);
                    if (engineproblem == true)
                    {
                        car2.FSpeed = car2.FSpeed -1;
                    }
                    else
                    {

                    }
                }
                else
                {
                    Random rnd = new Random();
                    int random = rnd.Next(0, 50);
                    if (random == 1) // 1%
                    {
                        lock (_sync) { PrintFaults(0, 26, car2, "Slut på bensin!"); }
                        SleepTime = SleepTime + 3000;
                    }
                    else if (random >= 2 && random <= 3) // 2%
                    {
                        lock (_sync) { PrintFaults(0, 26, car2, "Punka!"); }
                        SleepTime = SleepTime + 2000;
                    }
                    else if (random >= 4 && random <= 9) // 5%
                    {
                        lock (_sync) { PrintFaults(0, 26, car2, "Fågel!"); }

                        SleepTime = SleepTime + 1000;
                    }
                    else if (random >= 10 && random <= 20)
                    {
                        lock (_sync) { PrintFaults(0, 26, car2, "Motorfel!"); }
                        car2.FSpeed = car2.FSpeed -1;
                    }
                    else
                    {

                    }
                }

                Thread.Sleep(100);
                Thread.Sleep(SleepTime);
                car2.Speed = car2.Speed + EngineDur;
                lock (_sync) { PrintFaults(0, 26, car2, "                          "); }
                lock (_sync) { PrintCarAt(distance2, 28, car2); }
        }
        public static bool Event(Car car, int position)
        {
            Random rnd = new Random();
            int random = rnd.Next(0, 20);
            if (random == 1) // 1%
            {
                lock (_sync) { PrintFaults(0, position, car, "Slut på bensin!"); }
                Thread.Sleep(3000);
            }
            else if (random >= 2 && random <= 3) // 2%
            {
                lock (_sync) { PrintFaults(0, position, car, "Punka!"); }
                Thread.Sleep(2000);
            }
            else if (random >= 4 && random <= 9) // 5%
            {
                lock (_sync) { PrintFaults(0, position, car, "Fågel!"); }
                Thread.Sleep(1000);
            }
            else if (random >= 10 && random <= 20)
            {
                lock (_sync) { PrintFaults(0, position, car, "Motorfel!"); }
                return true;
            }
            return false;

        }
        private static object _sync = new object();
        public static void PrintCarAt(int xCol, int yRow, Car car)
        {
            try
            {
                Console.SetCursorPosition(xCol, yRow);
                Console.WriteLine(@".-'-" + car.CarId + "-`-._");
                Console.SetCursorPosition(xCol, yRow +1);
                Console.WriteLine(@"'-O---O--'");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        public static void PrintFaults(int xCol, int yRow, Car car, string carError)
        {
            try
            {
                Console.SetCursorPosition(xCol, yRow);
                Console.WriteLine(car.CarName+ " " + carError);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        public static void printStatistics(int xCol, int yRow, Car car, int distance)
        {
            try
            {
                Console.SetCursorPosition(xCol, yRow);
                Console.WriteLine($"{car.CarName} is traveling at the speed {car.FSpeed}KM/H and has drived {distance}M");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
