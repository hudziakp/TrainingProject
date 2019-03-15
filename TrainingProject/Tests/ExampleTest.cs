using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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


        [Test]
        public void GenericTest()
        {
            var gen = new NotSoGeneric();
            var str = new MyString("Something");

            str.Print(() =>
            {
                var s = "Another Text";
                gen.PrintText(str.Str);
                return s;
            });

            str.Print(Aaaa);
        }

        public static string Aaaa()
        {
            var s = "Another Text";
            return s;

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
        private string AddTag(string text ="jhvj", params string[] reso)
        {
            if (reso != null)
            {
                foreach (var r in reso)
                {
                    Console.WriteLine(r);
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
