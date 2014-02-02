using System;
using System.Collections.Generic;
using System.Linq;
using Pamplemoos.Core;

namespace Pamplemoos.Repository
{
    interface ITestRepository
    {
        MultipleExecutedTest GetTest(string Id);
        MultipleExecutedTest GetLastExecution(string Id);
        MultipleExecutedTest GetLastExecutions(string Id, int count);
    }
}
