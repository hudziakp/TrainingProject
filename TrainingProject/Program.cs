using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TrainingProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var listOfAllFiles = FindAllCsvFilePaths();
                foreach (var path in listOfAllFiles)
                {
                    var dataFromFile = ReadDataFromFile(path);
                    Console.WriteLine($"\n======= {path} =======\n");
                    PrintDataInConsole(dataFromFile);

                }
                Console.ReadKey();
            }
            catch
            {
                var MailHelper = new MailHelper
                {
                    Subject = "Error",
                    Body = "Unexpected Error",
                    Recipient = "alan0999@gmail.com"
                };
                MailHelper.SendMail();
            }
            


        }

        #region Internal Methods
        /// <summary>
        /// Prints Column name and values into the Console
        /// </summary>
        /// <param name="columnName">Column name</param>
        /// <param name="values">String values for column</param>
        internal static void PrintColumnValues(string columnName, string[] values)
        {
            if (!string.IsNullOrWhiteSpace(columnName))
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
        #endregion

        #region Public Method
        public static List<string> ReadDataFromFile(string path)
        {
            var lines = new List<string>();
            try
            {
                using (var sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        lines.Add(sr.ReadLine());
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return lines;
        }

        public static void PrintDataInConsole(List<string> listString)
        {
            var headerString = listString.FirstOrDefault();
            if (!string.IsNullOrEmpty(headerString))
            {
                var column = headerString.Split('|');
                listString.Remove(headerString);
                var rows = new List<string[]>();
                foreach (var row in listString)
                {
                    rows.Add(row.Split('|'));
                }

                for (var i = 0; i < column.Count(); i++)
                {
                    PrintColumnValues(column[i], rows.Select(row => row[i]).ToArray());
                }
            }
        }

        public static List<string> FindAllCsvFilePaths()
        {
            var listOfAllFiles = new List<string>();
            var dataPath = GetFolderPath();
            if (!string.IsNullOrEmpty(dataPath))
            {
                var files = Directory.EnumerateFiles(dataPath, "*.csv");
                foreach (var item in files)
                {
                    string fullPath = Path.GetFullPath(item);
                    listOfAllFiles.Add(fullPath);
                }
            }
            return listOfAllFiles;
        }

        public static string GetFolderPath()
        {
            var codebase = Assembly.GetCallingAssembly().CodeBase;
            var uri = new UriBuilder(codebase);
            var assemblyPath = Uri.UnescapeDataString(uri.Path);
            var path = new FileInfo(assemblyPath).Directory?.FullName;
            return string.IsNullOrEmpty(path) ? null : Path.Combine(path, ConfigurationManager.AppSettings["DataPath"]);
        }
        #endregion

        #region Private
        private static ConsoleColor[] GetConfigurationColor()
        {
            var color = new ConsoleColor[2];
            var headerColor = ConfigurationManager.AppSettings["HeaderColor"];
            var valueColor = ConfigurationManager.AppSettings["ValueColor"];
            color[0] = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), headerColor);
            color[1] = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), valueColor);
            return color;
        }
        #endregion
    }
}
