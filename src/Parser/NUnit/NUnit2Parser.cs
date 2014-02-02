using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Pamplemoos.Core;

namespace Pamplemoos.Parser.NUnit
{
    class NUnit2Parser : CommonParser, IFileParser
    {       
        public NUnit2Parser(string file)
            : base(file, "en-gb")
        {
        }

        public string GetId()
        {
            var dateTime = GetExecution();
            return dateTime.ToString("s");
        }

        public DateTime GetExecution()
        {
            // Select and display the first node date and time
            var date = ParseDate(Root.Attributes.GetNamedItem("date").Value);
            var time = ParseTime(Root.Attributes.GetNamedItem("time").Value);

            return date.Add(time);
        }

        public Session GetSession()
        {
            var session = new Session();
            session.Execution = GetExecution();
            session.Id = GetId();

            GetChildrenSuites(session.Suites, Root, "/test-results/test-suite");

            return session;
        }

        public ExecutedTest GetTest(string id)
        {
            var testNode = Root.SelectSingleNode(string.Format("//test-case[@name='{0}']", id));
            if (testNode != null)
                return InstantiateTest(testNode);
            return null;
        }

        protected void GetChildrenSuites(IList<Suite> suites, XmlNode currentNode, string path)
        {
            foreach (XmlNode node in currentNode.SelectNodes(path))
            {
                var childSuite = new Suite();
                childSuite.Name = node.Attributes.GetNamedItem("name").Value;
                suites.Add(childSuite);
                GetChildrenTests(childSuite, node, "results/test-case");
                GetChildrenSuites(childSuite.Suites, node, "results/test-suite");
            }
        }

        protected void GetChildrenTests(Suite suite, XmlNode currentNode, string path)
        {
            foreach (XmlNode node in currentNode.SelectNodes(path))
            {
                var childTest = InstantiateTest(node);
                suite.Tests.Add(childTest);
            }
        }

        private ExecutedTest InstantiateTest(XmlNode node)
        {
            var childTest = new ExecutedTest();
            childTest.Name = node.Attributes.GetNamedItem("name").Value;
            var successAttribute = node.Attributes.GetNamedItem("success");
            if (successAttribute != null)
                childTest.Result.IsSuccess = Boolean.Parse(successAttribute.Value);
            return childTest;
        }
    }
}
