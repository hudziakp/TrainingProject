using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
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
            if (string.IsNullOrEmpty(path)) ;
            Path.Combine(path, ConfigurationManager.AppSettings["DataPath"]);

        }

    }
}
