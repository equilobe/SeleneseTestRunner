using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleneseTestRunner.Executors
{
    class SuiteExecutor
    {
        public static void Execute(string suitePath, string baseUrl)
        {

            var tests = new string[]
            {
                @"C:\Users\badea\Code\BPApp\e2e-tests\MemberTests\Authentication\LogIn.html",
                @"C:\Users\badea\Code\BPApp\e2e-tests\MemberTests\GeneralTests\BrowseThroughApp.html",
                @"C:\Users\badea\Code\BPApp\e2e-tests\MemberTests\Authentication\LogOut.html"
            };

            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://test.boardprospects.com");

                foreach (var test in tests)
                {
                    var testResult = TestExecutor.ExecuteTest(driver, test);
                }
            }
        }       
    }

    class SuiteResult
    {
        public SuiteResult()
        {
            TestResults = new List<TestResult>();
        }

        public List<TestResult> TestResults { get; set; }
    }
}
