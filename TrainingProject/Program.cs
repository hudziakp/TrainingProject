using System;
using System.Linq;

namespace TrainingProject
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// Prints Column name and values into the Console
        /// </summary>
        /// <param name="columnName">Column name</param>
        /// <param name="values">String values for column</param>
        internal static void PrintColumnValues(string columnName, string[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            Console.Write($"{columnName}:");
            var lastElement = values.Last();
            foreach (var value in values)
            {
                Console.Write($"{value}");
                if (!value.Equals(lastElement)) Console.Write(",");
            }
            Console.WriteLine("\n-----------------");
        }
    }
}
