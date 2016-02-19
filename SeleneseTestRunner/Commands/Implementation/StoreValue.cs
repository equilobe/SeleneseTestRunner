using OpenQA.Selenium;
using SeleneseTestRunner.Suites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands.Implementation
{
    class StoreValueCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            SuiteExecutor.StoreValue(command.Parameter, element.Text);
        }
    }
}
