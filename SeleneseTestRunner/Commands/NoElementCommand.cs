using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands
{
    class NoElementCommand : ICommand
    {
        public CommandResult Execute(IWebDriver driver, CommandDesc command)
        {
            try
            {
                var result = new CommandResult { Command = command };

                var timeout = GetTimeout(command);
                var elements = driver.GetElements(command.Selector, timeout);
                var count = elements.Count();

                if (count > 0)
                    throw new Exception("Element was found.");

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

        private int? GetTimeout(CommandDesc command)
        {
            int timeout;
            if (Int32.TryParse(command.Parameter, out timeout))
                return timeout;

            return null;
        }
    }
}
