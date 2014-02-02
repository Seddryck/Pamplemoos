using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pamplemoos.Core
{
    public class ExecutedTest : Test
    {
        public ExecutedTest()
        {
            Result = new Result();
        }

        public Result Result { get; set; }
    }
}
