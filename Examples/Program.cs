using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calendar Example:");
            new Calendar.Example();

            Console.WriteLine("\nFormatting Example:");
            new Formatting.Example();

            Console.WriteLine("\nPersistence Example:");
            new Persistence.Example();

            Console.WriteLine("\nExtending Example:");
            new Extending.Example();

            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
