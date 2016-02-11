using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleneseTestRunner.Tests;

namespace SeleneseTestRunner.Suites
{
    class SuiteExecutor
    {
        static Dictionary<string, string> _storedValues;

        public static SuiteResult Execute(string suitePath, string baseUrl)
        {
            _storedValues = new Dictionary<string, string>();

            var result = new SuiteResult();
            var suite = SuiteLoader.LoadFromFile(suitePath);
            result.Name = suite.Name;

            using (IWebDriver driver = new ChromeDriver())
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
