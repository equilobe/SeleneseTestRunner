using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;

namespace SeleneseTestRunner
{
    class SeleniumTestRunner
    {

        IWebDriver driver;

        public void Run()
        {
            using (driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://test.boardprospects.com");

                var element = GetElement("css=.btn:contains(\"Join as an Individual\")");
                
                Console.WriteLine(element.TagName);
            }
        }


        private IWebElement GetElement(string selector)
        {
            var complexSelector = GetContainsSelector(selector);
            var by = GetBy(complexSelector.SimpleSelector);

            var elements = driver.FindElements(by);

            if (!elements.Any())
                WaitForElement(by);

            elements = driver.FindElements(by);

            if (!string.IsNullOrEmpty(complexSelector.ContainedText))
                return elements.Single(e => e.Text.Contains(complexSelector.ContainedText));

            return elements.Single();
        }

        void WaitForElement(By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            wait.Until(d => d.FindElements(by).Any());
        }

        static By GetBy(string selector)
        {
            var lowerSelector = selector.ToLower();
            if (lowerSelector.StartsWith("css="))
                return By.CssSelector(selector.Substring(4));

            if (lowerSelector.StartsWith("name="))
                return By.Name(selector.Substring(5));

            if (lowerSelector.StartsWith("link="))
                return By.LinkText(selector.Substring(5));

            if (lowerSelector.StartsWith("xpath="))
                return By.XPath(selector.Substring(6));


            return By.XPath(selector);
        }

        ContainsSelector GetContainsSelector(string selector)
        {
            if (selector.ToLower().StartsWith("css=") && selector.Contains(":contains("))
                return GetCssContainsSelector(selector);

            return new ContainsSelector
            {
                SimpleSelector = selector
            };
        }

        private static ContainsSelector GetCssContainsSelector(string selector)
        {
            var regex = new Regex(@"(.*)\:contains\([""'](.*)[""']\)");
            var result = regex.Match(selector);
            return new ContainsSelector
            {
                SimpleSelector = result.Groups[1].Value,
                ContainedText = result.Groups[2].Value
            };
        }

        class ContainsSelector
        {
            public string SimpleSelector { get; set; }

            public string ContainedText { get; set; }
        }
    }
}
