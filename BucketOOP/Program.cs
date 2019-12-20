using System;
using System.Threading;

namespace BucketOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            BucketScenario();
            WaitAndClear();
            OilBarrelScenario();
            WaterTankScenario();
        }

        private static void WaterTankScenario()
        {
            Console.Title = "Watertank Scenario";

            Console.WriteLine("Creating a default Watertank:");
            WaterTank tank = new WaterTank();
            Console.WriteLine(tank);

            WaitAndClear();

            Console.WriteLine("Creating a large water tank:");
            WaterTank largetank = new WaterTank(WaterTank.Sizes.Large);
            Console.WriteLine(largetank);

            WaitAndClear();

            Console.WriteLine("Creating a medium water tank:");
            WaterTank mediumtank = new WaterTank(WaterTank.Sizes.Medium);
            Console.WriteLine(mediumtank);

            WaitAndClear();

            uint x = 100;
            Console.WriteLine($"Creating a large tank with {x} units:");
            WaterTank large = new WaterTank(WaterTank.Sizes.Large, x);
            Console.WriteLine(large);

            WaitAndClear();

            tank.Full += Container_Full;
            tank.AcceptOverflow = false;
            Console.WriteLine("Pouring the contents of one tank into the other without overflow:");
            Console.WriteLine($"Filling\n\n{tank}\n\nWith\n\n{large}\n");
            tank.FillWith(large);
            Console.WriteLine($"Filled watertank:\n\n{tank}\n\nEmptied watertank:\n\n{large}");

            WaitAndClear();

            tank.AcceptOverflow = true;
            Console.WriteLine("Pouring the contents of one tnak into the other with overflow:");
            Console.WriteLine($"Filling\n\n{tank}\n\nWith\n\n{large}\n");
            tank.FillWith(large);
            Console.WriteLine($"Filled watertank:\n\n{tank}\n\nEmptied watertank:\n\n{large}");
        }
        static void BucketScenario()
        {
            Console.Title = "Bucket Scenario";

            Bucket bucket1 = new Bucket();
            Console.WriteLine("Creating default bucket:");
            Console.WriteLine(bucket1);

            WaitAndClear();

            Console.WriteLine("Attempting to create a bucket that is smaller than 10:");
            try
            {
                Bucket wrongBucket = new Bucket(8);
            }
            catch (ContainerException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            WaitAndClear();

            Bucket bucket2 = new Bucket(10, 5);
            Console.WriteLine($"Creating a bucket of size {bucket2.Size} that has {bucket2.Volume} units in it:");
            Console.WriteLine(bucket2);

            WaitAndClear();

            Bucket bucket3 = new Bucket(20, 5);
            Console.WriteLine($"Creating a bucket of size {bucket3.Size} that has {bucket3.Volume} units in it:");
            Console.WriteLine(bucket3);

            WaitAndClear();

            Console.WriteLine($"Adding to the first bucket until it is full.");
            Console.WriteLine($"{bucket1}");
            bucket1.Full += Container_Full;
            while (!bucket1.IsFull)
            {
                //Make sure we only write the "current" bucket1
                Console.CursorTop -= 3;
                Console.CursorLeft = 0;
                //Add 1 to the bucket and print.
                bucket1.Fill(1);
                Console.WriteLine($"{bucket1}");
                Thread.Sleep(500);
            }

            WaitAndClear();

            bucket3.Full += Container_Full;
            Console.WriteLine("Filling with another bucket:");
            Console.WriteLine($"Filling\n\n{bucket3}\n\nWith\n\n{bucket1}\n");
            bucket3.FillWith(bucket1);
            Console.WriteLine($"Filled bucket:\n\n{bucket3}\n\nEmptied bucket:\n\n{bucket1}");

            WaitAndClear();

            Console.WriteLine("Filling while not accepting overflow:");
            bucket3.AcceptOverflow = false;
            Console.WriteLine($"Filling\n\n{bucket3}\n\nWith\n\n{bucket2}\n");
            bucket3.FillWith(bucket2);
            Console.WriteLine($"Filled bucket:\n\n{bucket3}\n\nEmptied bucket:\n\n{bucket2}");

            WaitAndClear();

            Console.WriteLine("Checking how much empty units are left in a bucket:");
            Console.WriteLine($"{bucket2}\nHas {bucket2.Remainder} empty units left");

            WaitAndClear();

            Console.WriteLine("Filling while accepting overflow:");
            bucket3.AcceptOverflow = true;
            Console.WriteLine($"Filling\n\n{bucket3}\n\nWith\n\n{bucket2}\n");
            bucket3.FillWith(bucket2);
            Console.WriteLine($"Filled bucket:\n\n{bucket3}\n\nEmptied bucket:\n\n{bucket2}");

            WaitAndClear();

            Console.WriteLine("Emptying a bucket till it is empty");
            Console.WriteLine(bucket3);
            while (!bucket3.IsEmpty)
            {
                //Make sure we only write the "current" bucket1
                if(bucket3.Volume == 10)
                {
                    Console.CursorTop -= 1;
                    Console.CursorLeft = 0;
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.CursorTop += 1;
                }
                Console.CursorTop -= 3;
                Console.CursorLeft = 0;
                //Remove 1 from the bucket and print.                
                bucket3.Empty(1);
                Console.WriteLine($"{bucket3}");
                Thread.Sleep(500);
            }

            WaitAndClear();

            bucket2.Volume = bucket2.Size;
            Console.WriteLine("Refilling one bucket to full and then emptying it with Empty()");
            Console.WriteLine($"Before:\n{bucket2}");
            bucket2.Empty();
            Console.WriteLine($"After:\n{bucket2}");
        }
        static void OilBarrelScenario()
        {
            Console.Title = "Oilbarrel Scenario";

            Console.WriteLine("Creating a default oilbarrel:");
            OilBarrel barrel1 = new OilBarrel();
            Console.WriteLine(barrel1);

            WaitAndClear();

            uint x = 100;
            Console.WriteLine($"Creating an oilbarrel with {x} units in it:");
            OilBarrel barrel2 = new OilBarrel(x);
            Console.WriteLine(barrel2);

            WaitAndClear();

            x = 50;
            Console.WriteLine($"Taking {x} units out of the oilbarrel:");
            barrel2.Empty(50);
            Console.WriteLine(barrel2);

            WaitAndClear();

            barrel2.Full += Container_Full;
            Console.WriteLine("Filling an oilbarrel with 9 units at a time:");
            Console.WriteLine(barrel2);
            while (!barrel2.IsFull)
            {
                //Make sure we only write the "current" barrel2
                Console.CursorTop -= 3;
                Console.CursorLeft = 0;
                //Add 9 to the barrel and print.
                barrel2.Fill(9);
                Console.WriteLine($"{barrel2}");
                Thread.Sleep(500);
            }

            WaitAndClear();

            
            while (!barrel2.IsEmpty)
            {
                Console.Clear();
                Console.WriteLine("Emptying a barrel with 10 units at a time:");
                Console.WriteLine($"{barrel2}");

                barrel2.Empty(10);
                Thread.Sleep(500);                
            }
        }
        static void WaitAndClear()
        {
            Console.ReadKey();
            //Thread.Sleep(1000);
            Console.Clear();
        }

        private static void Container_Full(object sender, ContainerEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (e.Overflow > 0)
            {
                Console.WriteLine($"{sender.GetType().Name} is overflowing by: {e.Overflow}.");
            }
            else
            {
                Console.WriteLine($"{sender.GetType().Name} is full.");
            }
            Console.ResetColor();
        }
    }
}
