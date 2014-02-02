using System;
using System.IO;
using System.Linq;
using Pamplemoos.Core;
using Pamplemoos.Parser;

namespace Pamplemoos.Repository.FileSystem
{
    class TestRepository : ITestRepository
    {
        public string RootDirectory { get; protected set; }
        protected ParserFactory Factory { get; set; }

        public TestRepository()
        {
            Factory = new ParserFactory();
        }

        public MultipleExecutedTest GetTest(string Id)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public ExecutedTest GetTest(string sessionId, string id)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public ExecutedTest GetLastExecution(string id)
        {

            var lastSessionExecution = DateTime.MinValue;
            var lastSessionFile = string.Empty;

            foreach (var file in Directory.EnumerateFiles(RootDirectory, "*.xml"))
            {
                var fileParser = Factory.GetInstance(file);
                var fileExecution = fileParser.GetExecution();
                if (fileExecution > lastSessionExecution)
                    lastSessionFile = file;
            }

            ExecutedTest test = null;
            if (string.IsNullOrEmpty(lastSessionFile))
                return null;

            var parser = Factory.GetInstance(lastSessionFile);
            test = parser.GetTest(id);
            return test;
        }

        public MultipleExecutedTest GetLastExecutions(string id, int count)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }


        MultipleExecutedTest ITestRepository.GetLastExecution(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
