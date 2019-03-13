using System;
using System.IO;
using NUnit.Framework;

namespace TrainingProject
{
    [TestFixture]
    class ProgramTest
    {
        private readonly StringWriter _stringWriter = new StringWriter();
        private TextWriter _defaultOutput;

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
            StringAssert.Contains("Test Example",_stringWriter.ToString());
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp");
            _defaultOutput = Console.Out;
            Console.SetOut(_stringWriter);
        }

        [SetUp]
        public void EveryTime()
        {
            Console.Clear();
            var sb =_stringWriter.GetStringBuilder();
            sb.Remove(0, sb.Length);
        }

        [OneTimeTearDown]
        public void Clean()
        {
            Console.SetOut(_defaultOutput);
            _stringWriter.Dispose();
            Console.WriteLine("Clean");
        }
    }
}
