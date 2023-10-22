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
            Console.WriteLine($"{name}, welcome to my first console application");
        }

        static partial void Welcome8636();
    }
    private static void Welcome8636();
}