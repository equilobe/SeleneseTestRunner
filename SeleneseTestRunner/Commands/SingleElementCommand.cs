using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleneseTestRunner.Commands
{
    
    abstract class SingleElementCommand : ICommand
    {
        public CommandResult Execute(IWebDriver driver, CommandDesc command)
        {
            try
            {
                var result = new CommandResult { Command = command };
                var element = GetElement(driver, command, result);

                if (result.HasError)
                    return result;
                Execute(driver, element, command);
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

        private IWebElement GetElement(IWebDriver driver, CommandDesc command, CommandResult result)
        {
            var timeout = GetTimeout(command);
            var elements = driver.GetElements(command.Selector, timeout);
            var count = elements.Count();

            if (count == 0)
            {
                result.HasError = true;
                result.Comments = "Element not found.";
            }
            else if (count > 1)
            {
                result.HasWarning = true;
                result.Comments = "More than one element found";
            }

            return elements.FirstOrDefault();
        }

        private int? GetTimeout(CommandDesc command)
        {
            int timeout;
            if (Int32.TryParse(command.Parameter, out timeout))
                return timeout;

            return null;
        }

        abstract protected void Execute(IWebDriver driver, IWebElement element, CommandDesc command);
    }
}
