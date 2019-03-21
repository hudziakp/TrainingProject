using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TrainingProject.Tests
{
    [TestFixture]
    public class ExampleTest
    {

        [Test]
        public void FindingPath()
        {
            var codebase = Assembly.GetCallingAssembly().CodeBase;
            var uri = new UriBuilder(codebase);
            var assemblyPath = Uri.UnescapeDataString(uri.Path);
            var path = new FileInfo(assemblyPath).Directory?.FullName;
            if (string.IsNullOrEmpty(path))
            {
              var result = Path.Combine(path, ConfigurationManager.AppSettings["DataPath"]);
            }
        }


        /// <summary>
        /// Verify if we use positive email we will get positive results
        /// </summary>
        /// <param name="email">Email to Verify</param>
        [Test]
        [TestCase("a@b.eu")]
        [TestCase("test@pcmicorp.com")]
        [TestCase("test1@pcmicorp.com")]
        [TestCase("zuzanna.cos@pcmicorp.com")]
        [TestCase("alamakota@pcmicorp.com")]
        [TestCase("justyna@buziaczek.pl")]
        [TestCase(EmailHandler.SampleEmail)]
        public void VerifyEmailPositive(string email)
        {
            var emailHandler = new EmailHandler(email);
            emailHandler.IsValid.Should().BeTrue();
        }

        [Test]
        [TestCase("jestem.@com")]
        [TestCase("test@com")]
        [TestCase("test ss@pcmicorp.com")]
        public void VerifyEmailNegative(string email)
        {
            var emailHandler = new EmailHandler(email);
            emailHandler.IsValid.Should().BeFalse();
        }


        [Test]
        [TestCase(ErrorType.Info, "Message 1")]
        [TestCase(ErrorType.Warning, "Message 1")]
        [TestCase(ErrorType.SystemFailure, "Message 1")]
        public void VerifyEnums(ErrorType errorType, string message)
        {
            new ErrorHandler(errorType,message).PrintError();            
        }


    }



    public class NotSoGeneric : Generic
    {
        public override void PrintText(string text)
        {
            Console.WriteLine("=============");
            base.PrintText(text);
            Console.WriteLine("=============");
        }
    }



    public class Generic
    {
        private string AddTag(string text ="jhvj", params string[] comments)
        {
            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    Console.WriteLine(comment);
                }
            }
            return $"TAG {text}";
        }


        public virtual void PrintText(string text)
        {
            Console.WriteLine(AddTag(text,"sadas", "dasdas", "dasdas","LAST ONE"));
        }


        public string ChangeText(MyString text)
        {
            text.Update( $" ======== {text.Str} ========== ");
            return text.Str;
        }
    }

    public class MyString
    {
 
        public string Str { get; private set; }

        public static string Cos;

        private static  object _oLock = new object();


        public MyString(string str)
        {
            Str = str;
        }

        public void Update(string str)
        {
            Str = str;
        }


        public void Print(Func<string> param)
        {
            Console.WriteLine(param.Invoke());
        }

    }

    public class Contract
    {
        public string Id { get; set; }
        public string Number { get; set; }
    }

    public class MyErrors
    {
        public string ErrorCore { get; set; }
        public string ErrorSummary { get; set; }
        public string ErrorDetails { get; set; }
    }
}
