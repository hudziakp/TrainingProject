using System;
using System.Collections.Generic;
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
        
            var read = ReadDataFromFile(ConfigurationManager.AppSettings["Path"]);
            PrintDataInConsole(read);

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

       
        public static List<String> ReadDataFromFile(String path)
        {           
            var line = new List<String>();
            try
            {   
                using (StreamReader sr = new StreamReader(path))
                {                  
                    while (sr.Peek() >= 0)
                    {
                        line.Add(sr.ReadLine());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return line;
        }

        public static void PrintDataInConsole(List<String> listString)
        {
            var headerString = listString.FirstOrDefault();
            var column = headerString.Split('|');
            listString.Remove(headerString);
            var rows = new List<String[]>();
            foreach(var row in listString)
            {
                rows.Add(row.Split('|'));
            }
            for(int i = 0; i< column.Count(); i++)
            {
                PrintColumnValues(column[i], rows.Select(row =>row[i]).ToArray());
            }
        }

    }
}
