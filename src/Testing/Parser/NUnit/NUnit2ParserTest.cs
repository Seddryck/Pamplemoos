using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Pamplemoos.Parser.NUnit;

namespace Pamplemoos.Testing.Parser.NUnit
{
    [TestFixture]
    public class NUnit2ParserTest
    {
        private string ReadXmlContent(string filename)
        {
            var fileContent = string.Empty;
            // A Stream is needed to read the XML document.
            using (var stream = Assembly.GetExecutingAssembly()
                                           .GetManifestResourceStream("Pamplemoos.Testing.Resources." + filename))
                using (var reader = new StreamReader(stream))
                    fileContent = reader.ReadToEnd();
            return fileContent;
        }

        [Test]
        public void GetId_SampleFile_CorrectValue()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));
            
            var result = parser.GetId();
            Assert.That(result, Is.EqualTo("2010-10-18T13:23:35"));
        }

        [Test]
        public void GetExecution_SampleFile_CorrectValue()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));

            var result = parser.GetExecution();
            Assert.That(result, Is.EqualTo(new DateTime(2010, 10, 18, 13, 23, 35)));
        }

        [Test]
        public void GetSession_SampleFile_CorrectHeader()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));

            var session = parser.GetSession();
            Assert.That(session, Is.Not.Null);
            Assert.That(session.Execution, Is.EqualTo(new DateTime(2010, 10, 18, 13, 23, 35)));
            Assert.That(session.Id, Is.EqualTo("2010-10-18T13:23:35"));
        }

        [Test]
        public void GetSession_SampleFile_CorrectChildrenSuites()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));

            var session = parser.GetSession();
            Assert.That(session, Is.Not.Null);
            Assert.That(session.Execution, Is.EqualTo(new DateTime(2010, 10, 18, 13, 23, 35)));
            Assert.That(session.Id, Is.EqualTo("2010-10-18T13:23:35"));

            Assert.That(session.Suites, Is.Not.Null);
            Assert.That(session.Suites, Has.Count.EqualTo(1));
            Assert.That(session.Suites[0].Name, Is.EqualTo("/home/charlie/Dev/NUnit/nunit-2.5/work/src/bin/Debug/tests/mock-assembly.dll"));
            Assert.That(session.Suites[0].Suites, Has.Count.EqualTo(1));

            var suites = session.Suites[0].Suites[0].Suites[0].Suites[0].Suites;
            Assert.That(suites, Has.Count.EqualTo(1));
        }

        [Test]
        public void GetSession_SampleFile_CorrectTests()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));

            var session = parser.GetSession();

            var suites = session.Suites[0].Suites[0].Suites[0].Suites[0].Suites;
            Assert.That(suites, Has.Count.EqualTo(1));
            Assert.That(suites[0].Tests, Has.Count.EqualTo(10));
        }

        [Test]
        public void GetSession_SampleFile_CorrectName()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));

            var session = parser.GetSession();

            var tests = session.Suites[0].Suites[0].Suites[0].Suites[0].Suites[0].Tests;
            Assert.That(tests[0].Name, Is.EqualTo("NUnit.Tests.Assemblies.MockTestFixture.FailingTest"));
            Assert.That(tests[1].Name, Is.EqualTo("NUnit.Tests.Assemblies.MockTestFixture.InconclusiveTest"));
            Assert.That(tests[2].Name, Is.EqualTo("NUnit.Tests.Assemblies.MockTestFixture.MockTest1"));
        }

        [Test]
        public void GetSession_SampleFile_CorrectSuccess()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));

            var session = parser.GetSession();

            var tests = session.Suites[0].Suites[0].Suites[0].Suites[0].Suites[0].Tests;
            Assert.That(tests[0].Result.IsSuccess, Is.EqualTo(false));
            Assert.That(tests[1].Result.IsSuccess, Is.EqualTo(false));
            Assert.That(tests[2].Result.IsSuccess, Is.EqualTo(true));
        }

        [Test]
        public void GetTest_SampleFile_CorrectTest()
        {
            var parser = new NUnit2Parser(string.Empty);
            parser.Initialize(ReadXmlContent("NUnit2-Result.xml"));

            var test = parser.GetTest("NUnit.Tests.ParameterizedFixture(5).Test1");

            Assert.That(test.Result.IsSuccess, Is.EqualTo(true));
            Assert.That(test.Name, Is.EqualTo("NUnit.Tests.ParameterizedFixture(5).Test1"));

        }
    }
}
