using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0055();
            Welcome8636();
        }

        private static void Welcome0055()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"{name}, welcome tomy first cobsole application");
        }

        static partial void Welcome8636();
    }
}