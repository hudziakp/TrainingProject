using System;
using System.IO;
using NUnit.Framework;

namespace TrainingProject
{
    [TestFixture]
    class ProgramTest
    {
        private readonly StringWriter _stringWriter = new StringWriter();

        [Test]
        public void TestPrintColumnValues()
        {
                Program.PrintColumnValues("Names", new[] { "Jack", "Greg", "Richard" });
                var output = _stringWriter.ToString();
                StringAssert.StartsWith("Names:", output);
                StringAssert.Contains("Jack", output);
                StringAssert.Contains("Greg", output);
                StringAssert.Contains("Richard", output);
           
        }

        [Test]
        public void ExampleTest()
        {
            Console.WriteLine("Test Example");
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp");
            Console.SetOut(_stringWriter);
        }

        [SetUp]
        public void EveryTime()
        {
            //Clears console output
            Console.Clear();
            var sb =_stringWriter.GetStringBuilder();
            sb.Remove(0, sb.Length);
        }

        [OneTimeTearDown]
        public void Clean()
        {
            _stringWriter.Dispose();
            Console.WriteLine("Clean");
        }
    }
}
