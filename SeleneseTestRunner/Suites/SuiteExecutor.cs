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
        public static SuiteResult Execute(string suitePath, string baseUrl)
        {
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
    }


}
