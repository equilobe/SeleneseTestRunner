using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleneseTestRunner.Commands;

namespace SeleneseTestRunner.Tests
{
    public class TestResult
    {
        public TestResult()
        {
            CommandResults = new List<CommandResult>();
        }

        public string Path { get; set; }
        public List<CommandResult> CommandResults { get; set; }
        public bool IsFailed { get; set; }
        public bool HasError { get; set; }
        public string Comments { get; set; }
    }
}
