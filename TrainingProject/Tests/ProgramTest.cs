using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace TrainingProject.Tests
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


        [Test]
        public void FindAllCsvFilePathsTest()
        {
            var paths = Program.FindAllCsvFilePaths();
            paths.Should().HaveCount(2);
            paths.Should().Contain(path => path.Contains("SampleData.csv"));
            paths.Should().OnlyContain(path => Path.IsPathRooted(path));
        }

        [Test]
        public void ReadDataFromFileTest()
        {
            var paths = Program.FindAllCsvFilePaths();
            var sampleDataPath = paths.FirstOrDefault(path => path.Contains("SampleData.csv"));

            var dataFromFile = Program.ReadDataFromFile(sampleDataPath);
            dataFromFile.Should().HaveCount(4);
            dataFromFile.Should().ContainSingle(line => line.Contains("|Occupation|"));
            dataFromFile.Should().ContainSingle(line => line.Contains("Seller"));
            dataFromFile.Should().OnlyHaveUniqueItems();
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
