using System;

namespace MathApp
{
    public class ConsoleView : IView
    {
        public void Warn(string message)
        {
            Console.WriteLine("!!!!!!!!!");
            Console.WriteLine("\t" + message);
            Console.WriteLine("!!!!!!!!!");
        }

        public string RequestNumber()
        {
            Console.WriteLine("Please enter a number and press <Enter>:");
            Console.Write("\t");
            return Console.ReadLine();
        }

        public void ShowResult(string result)
        {
            Console.WriteLine("The result is: ");
            Console.WriteLine("\t" + result);
            Console.WriteLine();
        }

        public void Quit()
        {
            Environment.Exit(0);
        }
    }
}