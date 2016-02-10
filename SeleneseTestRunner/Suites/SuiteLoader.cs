using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SeleneseTestRunner.Suites
{
    class SuiteLoader
    {
        public static SuiteDesc LoadFromFile(string filePath)
        {
            var doc = new XmlDocument();
            doc.Load(filePath);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.w3.org/1999/xhtml");

            var testsPath = new FileInfo(filePath).DirectoryName;

            var rows = doc.DocumentElement.SelectNodes("descendant::bk:tbody/descendant::bk:tr/descendant::bk:a", nsmgr);
            var tests = rows.OfType<XmlNode>()
                .Select(t => new TestLocation
                {
                    Name = t.InnerText,
                    Path = Path.Combine(testsPath, t.SelectSingleNode("@href").Value)
                });

            return new SuiteDesc
            {
                Name = Path.GetFileNameWithoutExtension(filePath),
                Tests = tests
            };
        }
    }
}
