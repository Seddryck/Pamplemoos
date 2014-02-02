using System;
using System.Linq;
using Pamplemoos.Parser.NUnit;

namespace Pamplemoos.Parser
{
    public class ParserFactory
    {
        public IFileParser GetInstance(string file)
        {
            return new NUnit2Parser(file);
        }
    }
}
