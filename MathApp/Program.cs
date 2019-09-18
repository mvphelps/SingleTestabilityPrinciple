using System;

namespace MathApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintUsage();

            var view = new ConsoleView();
            var p = new Presenter(view);
            while (true)
                p.Run();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Welcome to Math App!");
            Console.WriteLine("Enter q to quit.");
        }
    }
}