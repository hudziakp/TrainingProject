using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingProject
{
    public class ErrorHandler
    {
        public ErrorType ErrorType { get; set; }
        public string Message { get; set; }
        public ErrorHandler(ErrorType type, string message)
        {
            ErrorType = type;
            Message = message;
        }

        public void PrintError()
        {
            Console.WriteLine($"{ErrorType}: \n {Message}, ");
        }
    }
}
