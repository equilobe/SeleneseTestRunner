using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleneseTestRunner.Tests;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;

namespace SeleneseTestRunner.Suites
{
    public class SuiteExecutor
    {
        static Dictionary<string, string> _storedValues;
        public static string BaseUrl { get; private set; }

        public static SuiteResult Execute(string suitePath, string baseUrl, string browserName)
        {
            BaseUrl = baseUrl;
            _storedValues = new Dictionary<string, string>();

            var result = new SuiteResult();
            var suite = SuiteLoader.LoadFromFile(suitePath);
            result.Name = suite.Name;

            using (var driver = (IWebDriver)Activator.CreateInstance(GetWebDriver(browserName)))
            {
                driver.Navigate().GoToUrl(baseUrl);

                foreach (var test in suite.Tests)
                {
                    var testResult = TestExecutor.ExecuteTest(driver, test);
                    result.TestResults.Add(testResult);
                }
            }

            return result;
        }

        static Type GetWebDriver(string browserName)
        {
            var browsers = typeof(OpenQA.Selenium.Chrome.ChromeDriver).Assembly.GetTypes()
                .Where(t => typeof(IWebDriver).IsAssignableFrom(t))
                .Where(t => t.IsClass)
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
                .ToDictionary(t => GetBrowserNameFromTypeName(t.Name), t => t);

            return browsers[browserName.ToLower()];
        }

        static string GetBrowserNameFromTypeName(string typeName)
        {
            return typeName.Replace("Driver", "").ToLower();
        }

        public static string GetStoredValue(string key)
        {
            if (_storedValues.ContainsKey(key))
                return _storedValues[key];

            return string.Empty;
        }

        public static void StoreValue(string key, string value)
        {
            if (_storedValues.ContainsKey(key))
                _storedValues[key] = value;
            else
                _storedValues.Add(key, value);
        }
    }
}
