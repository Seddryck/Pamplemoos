using System;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace Pamplemoos.Parser
{
    class CommonParser
    {
        public CultureInfo Culture { get; private set; }
        public string File { get; private set; }
        private XmlDocument Document { get; set; }
        public XmlElement Root
        {
            get
            {
                return Document.DocumentElement;
            }
        }
          
        public CommonParser(string file, string culture)
        {
            File = file;
            Culture = CultureInfo.CreateSpecificCulture(culture);
            Document = new XmlDocument();
        }

        public void Initialize()
        {
            // Load the document and set the root element.
            Document.Load(File);
        }

        public void Initialize(string content)
        {
            Document.LoadXml(content);
        }


        protected internal DateTime ParseDate(string date)
        {
            return DateTime.Parse(date, Culture.DateTimeFormat);
        }

        protected internal TimeSpan ParseTime(string time)
        {
            var dateTime = DateTime.Parse(time, Culture.DateTimeFormat);
            return dateTime.TimeOfDay;
        }

    }
}
 