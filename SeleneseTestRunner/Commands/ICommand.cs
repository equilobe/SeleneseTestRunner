using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands
{
    using OpenQA.Selenium;
    interface ICommand
    {
        CommandResult Execute(IWebDriver driver, CommandDesc command);
    }
}
