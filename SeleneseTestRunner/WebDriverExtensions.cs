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
    static class WebDriverExtensions
    {
        const int DEFAULT_TIMEOUT = 10;

        public static IWebElement[] GetElements(this IWebDriver driver, string selector, int? timeout = null)
        {
            var complexSelector = GetContainsSelector(selector);
            var by = GetBy(complexSelector.SimpleSelector);

            var elements = driver.FindElements(by);

            if (!elements.Any())
                driver.WaitForElement(by, timeout.GetValueOrDefault(DEFAULT_TIMEOUT));

            elements = driver.FindElements(by);

            if (!string.IsNullOrEmpty(complexSelector.ContainedText))
                return elements.Where(e => e.Text.Contains(complexSelector.ContainedText)).ToArray();

            return elements.ToArray();
        }

        static void WaitForElement(this IWebDriver driver, By by, int timeout)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => d.FindElements(by).Any());
        }

        static By GetBy(string selector)
        {
            var lowerSelector = selector.ToLower();
            if (lowerSelector.StartsWith("id="))
                return By.Id(selector.Substring(3));

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

        static ContainsSelector GetContainsSelector(string selector)
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
