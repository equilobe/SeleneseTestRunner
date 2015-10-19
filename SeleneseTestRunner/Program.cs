using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SeleneseTestRunner
{
    class Program
    {

        static void Main(string[] args)
        {
            //ShowTestStats();
            new SeleniumTestRunner().Run();

        }

        private static void ShowTestStats()
        {
            IEnumerable<Command> allCommands = GetAllCommands();

            ShowCommandStats(allCommands);

            ShowSelectorStats(allCommands);
        }

        private static IEnumerable<Command> GetAllCommands()
        {
            var testFiles = Directory.GetFiles(@"C:\Users\badea\Code\BPApp\e2e-tests", "*.html", SearchOption.AllDirectories);

            var allCommands = testFiles.SelectMany(file =>
            {
                try
                {
                    return new CommandLoader().LoadFromFile(file).ToArray();
                }
                catch
                {
                    return Enumerable.Empty<Command>();
                }
            });
            return allCommands;
        }

        private static void ShowSelectorStats(IEnumerable<Command> allCommands)
        {
            var selectors = allCommands.Select(command => command.Selector);


            var seletorPrefixes = new string[] { "css=", "//", "name=", "xpath=", "link=" };

            foreach (var prefix in seletorPrefixes)
            {
                var count = selectors.Where(selector => selector.StartsWith(prefix))
                    .Count();
                Console.WriteLine(prefix + " " + count);
            }

            var otherSelectors = selectors.Where(selector => !seletorPrefixes.Any(prefix => selector.StartsWith(prefix))).Distinct();

            foreach (var selector in otherSelectors)
                Console.WriteLine(selector);
        }

        private static void ShowCommandStats(IEnumerable<Command> allCommands)
        {
            var distinctCommandNames = allCommands.Select(command => command.Name)
                .GroupBy(name => name)
                .OrderBy(group => group.Count());

            foreach (var commandName in distinctCommandNames)
                Console.WriteLine(commandName.Key + " " + commandName.Count());

            Console.WriteLine(distinctCommandNames.Count());

            Console.WriteLine("command count " + allCommands.Count());
        }

    }

    public class CommandLoader
    {

        XmlNamespaceManager nsmgr;

        public IEnumerable<Command> LoadFromFile(string filePath)
        {
            var doc = new XmlDocument();
            doc.Load(filePath);

            nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.w3.org/1999/xhtml");

            var tbody = doc.DocumentElement.SelectSingleNode("descendant::bk:tbody", nsmgr);
            var rows = tbody.SelectNodes("descendant::bk:tr", nsmgr);

            var commands = rows.OfType<XmlNode>()
                               .Select(GetCommandFromRow);
            return commands;
        }

        Command GetCommandFromRow(XmlNode node)
        {
            var cells = node.SelectNodes("descendant::bk:td", nsmgr).OfType<XmlNode>();
            if (cells.Count() != 3)
                throw new ArgumentOutOfRangeException("Each row must have 3 cells!");

            return new Command
            {
                Name = cells.First().InnerText,
                Selector = cells.Skip(1).First().InnerText,
                Parameter = cells.Last().InnerText
            };
        }

    }

    public class Command
    {
        public string Name { get; set; }
        public string Selector { get; set; }
        public string Parameter { get; set; }
    }
}
