using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pamplemoos.Core
{
    public class Test
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}
