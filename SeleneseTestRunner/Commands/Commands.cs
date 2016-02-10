using System;
using OpenQA.Selenium;

namespace SeleneseTestRunner.Commands
{
    class ClickCommand : SingleElementCommand
    {
        protected override void Execute(IWebElement element, CommandDesc command)
        {
            element.Click();
        }
    }

    class TypeCommand : SingleElementCommand
    {
        protected override void Execute(IWebElement element, CommandDesc command)
        {
            element.SendKeys(command.Parameter);
        }
    }

    class AssertElementPresentCommand : SingleElementCommand
    {
        protected override void Execute(IWebElement element, CommandDesc command)
        {
        }
    }

    class VerifyElementPresentCommand : AssertElementPresentCommand
    {

    }

    class OpenCommand : NavigateToUrlCommand
    {

    }
}
