using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SeleneseTestRunner.Commands;

namespace SeleneseTestRunner.Tests
{
    class TestLoader
    {

        XmlNamespaceManager nsmgr;

        public TestDesc LoadFromFile(string filePath)
        {
            var doc = new XmlDocument();
            doc.Load(filePath);

            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.w3.org/1999/xhtml");

            var title = doc.DocumentElement.SelectSingleNode("descendant::bk:title", nsmgr).InnerText;

            var tbody = doc.DocumentElement.SelectSingleNode("descendant::bk:tbody", nsmgr);
            var rows = tbody.SelectNodes("descendant::bk:tr", nsmgr);

            var commands = rows.OfType<XmlNode>()
                               .Select(GetCommandFromRow);

            return new TestDesc
            {
                Name = title,
                Commands = commands
            };
        }

        CommandDesc GetCommandFromRow(XmlNode node)
        {
            var cells = node.SelectNodes("descendant::bk:td", nsmgr).OfType<XmlNode>();
            if (cells.Count() != 3)
                throw new ArgumentOutOfRangeException("Each row must have 3 cells!");

            return new CommandDesc
            {
                Name = cells.First().InnerText,
                Selector = cells.Skip(1).First().InnerText,
                Parameter = cells.Last().InnerText
            };
        }

    }
}
