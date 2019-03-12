using System;
using System.Linq;
using System.Configuration;
namespace TrainingProject
{
    class Program
    {
        static ConsoleColor[] GetConfigurationColor()
        {
            var color = new ConsoleColor[2];
            var HeaderColor = ConfigurationManager.AppSettings["HeaderColor"];
            var ValueColor = ConfigurationManager.AppSettings["ValueColor"];
            color[0] = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), HeaderColor);
            color[1] = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ValueColor);
            return color;
        }

        static void Main(string[] args)
        {
            PrintColumnValues("test", new[] { "abc", "dfads", "asd" });
        }

        /// <summary>
        /// Prints Column name and values into the Console
        /// </summary>
        /// <param name="columnName">Column name</param>
        /// <param name="values">String values for column</param>
        internal static void PrintColumnValues(string columnName, string[] values)
        {
            var color = GetConfigurationColor();
            if (values == null) throw new ArgumentNullException(nameof(values));
            Console.ForegroundColor = color[0];
            Console.Write($"{columnName}:");
            
            var lastElement = values.Last();
            Console.ForegroundColor = color[1];
            foreach (var value in values)
            {
                Console.Write($"{value}");
                if (!value.Equals(lastElement)) Console.Write(",");
            }
            Console.ResetColor();
            Console.WriteLine("\n-----------------"); 
        }
    }
}
