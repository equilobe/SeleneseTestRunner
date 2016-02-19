using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands.Implementation
{
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
}
