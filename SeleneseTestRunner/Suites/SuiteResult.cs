using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleneseTestRunner.Tests;

namespace SeleneseTestRunner.Suites
{
    public class SuiteResult
    {
        public SuiteResult()
        {
            TestResults = new List<TestResult>();
        }

        public string Name { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
