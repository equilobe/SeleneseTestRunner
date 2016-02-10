using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

    class SelectCommand : SingleElementCommand
    {
        protected override void Execute(IWebElement element, CommandDesc command)
        {
            var selectElement = new SelectElement(element);

            var lowerCommand = command.Parameter.ToLower();
            if (lowerCommand.StartsWith("label="))
            {
                selectElement.SelectByText(command.Parameter.Substring(6));
                return;
            }

            if (lowerCommand.StartsWith("value="))
            {
                selectElement.SelectByValue(command.Parameter.Substring(6));
                return;
            }

            if (lowerCommand.StartsWith("index="))
            {
                selectElement.SelectByIndex(int.Parse(command.Parameter.Substring(6)));
                return;
            }

            if (lowerCommand.StartsWith("id="))
            {
                //todo
                return;
            }

            selectElement.SelectByText(command.Parameter);
        }
    }

    class UncheckCommand : SingleElementCommand
    {
        protected override void Execute(IWebElement element, CommandDesc command)
        {
            if (element.Selected)
            {
                element.Click();
            }
        }
    }

    class CheckCommand : SingleElementCommand
    {
        protected override void Execute(IWebElement element, CommandDesc command)
        {
            if (!element.Selected)
            {
                element.Click();
            }
        }
    }
}
