using System;
using System.IO;
using NUnit.Framework;

namespace TrainingProject
{
    [TestFixture]
    class ProgramTest
    {
        [Test]
        public void TestPrintColumnValues()
        {
            using (var stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                Program.PrintColumnValues("Names", new[] { "Jack", "Greg", "Richard" });
                var output = stringWriter.ToString();
                StringAssert.StartsWith("Names:", output);
                StringAssert.Contains("Jack", output);
                StringAssert.Contains("Greg", output);
                StringAssert.Contains("Richard", output);
            }
        }
    }
}
