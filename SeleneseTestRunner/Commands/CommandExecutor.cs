using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleneseTestRunner.Commands;

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

            return Commands[lowerName].Execute(driver, command);
        }
    }


}
