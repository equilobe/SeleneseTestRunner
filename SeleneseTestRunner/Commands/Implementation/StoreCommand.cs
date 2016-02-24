using OpenQA.Selenium;
using SeleneseTestRunner.Suites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands.Implementation
{
    public class StoreCommand : ICommand
    {
        public CommandResult Execute(IWebDriver driver, CommandDesc command)
        {
            try
            {
                var result = new CommandResult { Command = command };
                SuiteExecutor.StoreValue(command.Parameter, command.Selector);
                return result;
            }
            catch (Exception ex)
            {
                return new CommandResult
                {
                    Command = command,
                    HasError = true,
                    Comments = ex.Message
                };
            }
        }
    }
}
