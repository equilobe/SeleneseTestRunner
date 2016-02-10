using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleneseTestRunner.Commands;
using SeleneseTestRunner.Tests;
using SeleneseTestRunner.Suites;

namespace SeleneseTestRunner.Stats
{
    class TestStats
    {

        public static void ShowTestStats(string path)
        {
            IEnumerable<CommandDesc> allCommands = GetAllCommands(path);

            ShowCommandStats(allCommands);

            ShowSelectorStats(allCommands);
        }

        private static IEnumerable<CommandDesc> GetAllCommands(string path)
        {
            var suites = Directory.GetFiles(path, "*Suite.html", SearchOption.AllDirectories);
            return suites.SelectMany(s =>
            {
                var suite = SuiteLoader.LoadFromFile(s);
                return suite.Tests.SelectMany(t =>
                {
                    try
                    {
                        return new TestLoader().LoadFromFile(t.Path).Commands.ToArray();
                    }
                    catch
                    {
                        return Enumerable.Empty<CommandDesc>();
                    }
                });
            });
        }

        private static void ShowSelectorStats(IEnumerable<CommandDesc> allCommands)
        {
            var selectors = allCommands.Select(command => command.Selector);


            var seletorPrefixes = new string[] { "css=", "//", "name=", "xpath=", "link=", "id=" };

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

        private static void ShowCommandStats(IEnumerable<CommandDesc> allCommands)
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
}
