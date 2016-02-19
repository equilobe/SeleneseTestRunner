using OpenQA.Selenium;
using SeleneseTestRunner.Suites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands
{
    class NavigateToUrlCommand : ICommand
    {
        public CommandResult Execute(IWebDriver driver, CommandDesc command)
        {
            try
            {
                var result = new CommandResult { Command = command };
                var url = GetUrl(command.Selector);
                driver.Navigate().GoToUrl(url);
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

        private string GetUrl(string url)
        {
            if (url.StartsWith("/"))
                return SuiteExecutor.BaseUrl + url;

            return url;
        }
    }
}
