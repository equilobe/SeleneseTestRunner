using OpenQA.Selenium;
using SeleneseTestRunner.Suites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands.Implementation
{
    class StoreAttributeCommand : ICommand
    {
        public CommandResult Execute(IWebDriver driver, CommandDesc command)
        {
            try
            {
                var result = new CommandResult { Command = command };

                var indexOfSeparator = command.Selector.LastIndexOf("@");
                var selector = command.Selector.Substring(0, indexOfSeparator);
                var attribute = command.Selector.Substring(indexOfSeparator + 1);

                var element = GetElement(driver, selector, result);

                if (result.HasError)
                    return result;

                var attributeValue = element.GetAttribute(attribute);
                SuiteExecutor.StoreValue(command.Parameter, attributeValue);

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

        private IWebElement GetElement(IWebDriver driver, string selector, CommandResult result)
        {
            var elements = driver.GetElements(selector);
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
    }
}
