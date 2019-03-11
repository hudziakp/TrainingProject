using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace TrainingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

           // p.ReadDataFromFile(@"k:\test.txt");
            p.ReadDataFromFile();
            Console.ReadKey();
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

       
        public void ReadDataFromFile()
        {
            var connectionString = ConfigurationManager.AppSettings["Path"];
            try
            {   
                using (StreamReader sr = new StreamReader(connectionString))
                {
                    
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
