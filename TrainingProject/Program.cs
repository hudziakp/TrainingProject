using System;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Collections.Generic;

namespace TrainingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string> li = LoadDataFromAllCSV();
            //foreach (var item in li)
            //{
            //    Console.WriteLine(item);
            //}

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

        public static List<string> LoadDataFromAllCSV()
        {
            List<string> listA = new List<string>();
            string dataPath = ConfigurationManager.AppSettings["DataPath"];
            if(!String.IsNullOrEmpty(dataPath))
            {
                var files = Directory.EnumerateFiles(dataPath, "*.csv");
                
                foreach (string file in files)
                {
                    using (var reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split('|');

                            for (int i = 0; i < values.Length; i++)
                            {
                                listA.Add(values[i]);
                            }
                        }
                    }
                }
            }

            return listA;
        }


    }
}
