using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands.Implementation
{
    class AssertTextCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
            var elementText = element.Text.Trim();
            var searchedText = command.Parameter.Trim();

            if (!elementText.Equals(searchedText))
                throw new Exception("Text does not match. Found: \"" + elementText + "\"");
        }
    }
}
