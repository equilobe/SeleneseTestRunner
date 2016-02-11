using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleneseTestRunner.Commands;
using SeleneseTestRunner.Suites;
using System.Text.RegularExpressions;

namespace SeleneseTestRunner.Commands
{
    class CommandExecutor
    {
        static readonly Dictionary<string, ICommand> Commands;
        static CommandExecutor()
        {
            var commandType = typeof(ICommand);

            Commands = typeof(CommandExecutor).Assembly.GetTypes()
                .Where(t => commandType.IsAssignableFrom(t))
                .Where(t => t.IsClass)
                .Where(t => !t.IsAbstract)
                .ToDictionary(t => GetCommandNameFromTypeName(t.Name), t => (ICommand)Activator.CreateInstance(t));
        }

        static string GetCommandNameFromTypeName(string typeName)
        {
            return typeName.Replace("Command", "").ToLower();
        }

        public static CommandResult Execute(IWebDriver driver, CommandDesc command)
        {
            var lowerName = command.Name.ToLower();
            if (!Commands.ContainsKey(lowerName))
                return new CommandResult { Command = command, IsSkipped = true };

            command.Parameter = SetVariables(command.Parameter);
            command.Selector = SetVariables(command.Selector);

            return Commands[lowerName].Execute(driver, command);
        }

        private static string SetVariables(string input)
        {
            var regex = @"(?<=\$\{)[^}]*(?=\})";

            var matches = new List<Match>();
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);
            while (match.Success)
            {
                matches.Add(match);
                match = match.NextMatch();
            }

            var distinctVariables = matches.Select(t => t.Value).Distinct().ToList();
            foreach(var variable in distinctVariables)
            {
                var value = SuiteExecutor.GetStoredValue(variable);
                var key = "${" + variable + "}";
                input = input.Replace(key, value);
            }

            return input;
        }
    }


}
