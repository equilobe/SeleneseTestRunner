﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands.Implementation
{
    class WaitForElementPresentCommand : SingleElementCommand
    {
        protected override void Execute(IWebDriver driver, IWebElement element, CommandDesc command)
        {
        }
    }
}
