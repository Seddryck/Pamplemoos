using System;
using System.Linq;
using Pamplemoos.Core;

namespace Pamplemoos.Parser
{
    public interface IFileParser
    {
        void Initialize();
        void Initialize(string content);
        DateTime GetExecution();
        Session GetSession();
        string GetId();

        ExecutedTest GetTest(string id);
    }
}
