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
            Console.WriteLine("\nSelectors:");

            var selectors = allCommands.Select(command => command.Selector);
            var selectorPrefixes = new string[] { "css=", "//", "name=", "xpath=", "link=", "id=" };

            Console.WriteLine("  Known:");
            foreach (var prefix in selectorPrefixes)
            {
                var count = selectors.Where(selector => selector.StartsWith(prefix))
                    .Count();
                Console.WriteLine("    " + prefix + " " + count);
            }

            var otherSelectors = selectors
                .Where(selector => !selectorPrefixes.Any(prefix => selector.StartsWith(prefix)))
                .GroupBy(selector => selector)
                .Where(selector => !string.IsNullOrEmpty(selector.Key));

            Console.WriteLine("  Unknown:");
            foreach (var selector in otherSelectors)
                Console.WriteLine("    " + selector.Key + " " + selector.Count());
        }

        private static void ShowCommandStats(IEnumerable<CommandDesc> allCommands)
        {
            Console.WriteLine("Commands: ");

            var commands = new string[] { "click", "type", "assertElementPresent", "verifyElementPresent", 
                "open", "select", "sendKeys", "clickAt", "assertText", "assertChecked" };

            var distinctCommandNames = allCommands.Select(command => command.Name)
                .GroupBy(name => name)
                .OrderByDescending(group => group.Count());

            var implementedCommands = distinctCommandNames.Where(group => commands.Any(command => group.Key == command));
            var notImplementedCommands = distinctCommandNames.Where(group => !commands.Any(command => group.Key == command));

            Console.WriteLine("  Known:");
            foreach (var commandName in implementedCommands)
                Console.WriteLine("    " + commandName.Key + " " + commandName.Count());

            Console.WriteLine("  Unknown:");
            foreach (var commandName in notImplementedCommands)
                Console.WriteLine("    " + commandName.Key + " " + commandName.Count());

            Console.WriteLine("  Distinct: " + distinctCommandNames.Count());
            Console.WriteLine("  Commands: " + allCommands.Count());
        }
    }
}
