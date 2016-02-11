using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using SeleneseTestRunner.Suites;

namespace SeleneseTestRunner.Commands
{
    class ClickCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            element.Click();
        }
    }

    class ClickAtCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            var action = new Actions(driver);
            action.MoveToElement(element).Click().Perform();
        }
    }

    class TypeCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            element.SendKeys(command.Parameter);
        }
    }

    class AssertElementPresentCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
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
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
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

            selectElement.SelectByText(command.Parameter);
        }
    }

    class UncheckCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            if (element.Selected)
            {
                element.Click();
            }
        }
    }

    class CheckCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            if (!element.Selected)
            {
                element.Click();
            }
        }
    }

    class SendKeysCommand : TypeCommand
    {

    }
    
    class AssertTextCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            if (!element.Text.Equals(command.Parameter))
                throw new Exception("Text does not match");
        }
    }

    class AssertCheckedCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            if (!element.Selected)
                throw new Exception("Element is not checked");
        }
    }

    class AssertNotTextCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            if (element.Text.Equals(command.Parameter))
                throw new Exception("Text does match");
        }
    }

    class StoreValueCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            SuiteExecutor.StoreValue(command.Parameter, element.Text);
        }
    }
}
