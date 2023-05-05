using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BreakfastEX
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Breakfast();
            Console.ReadKey();
        }
        public static async Task Breakfast()
        {

            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");


            // avvia tre tasks per preparare la colazione: 
            var eggsTask = FryEggsAsync(2);// Ci vogliono 20 secondi 
            var baconTask = FryBaconAsync(3);// Ci vogliono 15 secondi 
            var toastTask = MakeToastWithButterAndJamAsync(2); // Ci vogliono 7 secondi  

            var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
            while (breakfastTasks.Count > 0)
            {
                Task finishedTask = await Task.WhenAny(breakfastTasks);
                if (finishedTask == eggsTask)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Eggs are ready");
                }
                else if (finishedTask == baconTask)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bacon is ready");
                }
                else if (finishedTask == toastTask)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Toast is ready");
                }
                Console.ResetColor();
                await finishedTask;
                breakfastTasks.Remove(finishedTask);
            }

            //var eggs = await eggsTask; // terzo ad uscire  
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("eggs are ready");



            //var bacon = await baconTask;// secondo ad uscire 
            //Console.ForegroundColor = ConsoleColor.Green;

            //Console.WriteLine("bacon is ready");

            //var toast = await toastTask; // primo ad uscire 
            //Console.ForegroundColor = ConsoleColor.Magenta;

            //Console.WriteLine("toast is ready");
            //Console.ResetColor();

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");

        }
        static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("MakeToastWithButterAndJamAsync".ToUpper());
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("\n\n");

            return toast;
        }
        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }
        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");
        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");
        private static async Task<Toast> ToastBreadAsync(int slices)
        {


            Console.WriteLine("ToastBreadAsync".ToUpper());
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            await Task.Delay(7000);
            Console.WriteLine("Remove toast from toaster");

            return new Toast();
        }
        private static async Task<Bacon> FryBaconAsync(int slices)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("FryBaconAsync".ToUpper());
            Console.WriteLine($"putting {slices} slices of bacon in the pan");
            Console.WriteLine("cooking first side of bacon...");
            await Task.Delay(5000);
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("flipping a slice of bacon");
            }
            Console.WriteLine("cooking the second side of bacon...");
            await Task.Delay(5000);
            Console.WriteLine("Put bacon on plate");
            Console.WriteLine("\n\n");

            return new Bacon();
        }
        private static async Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("FryEggsAsync".ToUpper());
            Console.WriteLine("Warming the egg pan...");
            await Task.Delay(10000);

            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");

            await Task.Delay(10000);
            Console.WriteLine("Put eggs on plate");
            Console.WriteLine("\n\n");

            return new Egg();
        }
        private static Coffee PourCoffee()
        {
            Console.WriteLine("\n\n");
            Console.WriteLine("PourCoffee".ToUpper());

            Console.WriteLine("Pouring coffee");
            Console.WriteLine("\n\n");

            return new Coffee();
        }
    }
    internal class Bacon { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }
} 

    

