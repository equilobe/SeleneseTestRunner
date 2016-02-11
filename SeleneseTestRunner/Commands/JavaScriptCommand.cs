using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands
{
    abstract class JavaScriptCommand : ICommand
    {
        public CommandResult Execute(IWebDriver driver, CommandDesc command)
        {
            try
            {
                var result = new CommandResult { Command = command };
                
                var value = ((IJavaScriptExecutor)driver).ExecuteScript(command.Selector);
                Execute(value, command);

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

        abstract protected void Execute(object scriptResult, CommandDesc command);
    }
}
