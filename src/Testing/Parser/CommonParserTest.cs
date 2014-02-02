using System;
using System.Linq;
using NUnit.Framework;
using Pamplemoos.Parser;

namespace Pamplemoos.Testing.Parser
{
    [TestFixture]
    public class CommonParserTest
    {
        [Test]
        public void ParseDate_EnglishDate_StandardDate()
        {
            var englishDate = "2010-12-15";
            var parser = new CommonParser("x","en-gb");
            var result = parser.ParseDate(englishDate);

            Assert.That(result, Is.EqualTo(new DateTime(2010, 12, 15)));
        }

        [Test]
        public void ParseTime_EnglishTime_StandardTimeSpan()
        {
            var englishTime = "23:53:12";
            var parser = new CommonParser("x", "en-gb");
            var result = parser.ParseTime(englishTime);

            Assert.That(result, Is.EqualTo(new TimeSpan(23,53,12)));
        }
    }
}
 