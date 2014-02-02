using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pamplemoos.Core
{
    public class Session
    {
        public Session()
        {
            Suites = new List<Suite>();
        }

        public string Id { get; set; }
        public DateTime Execution { get; set; }
        public IList<Suite> Suites { get; set; }

        
    }
}
