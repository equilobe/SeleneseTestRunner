using SeleneseTestRunner.Suites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands.Implementation
{
    class StoreEvalCommand : JavaScriptCommand
    {
        protected override void Execute(object scriptResult, CommandDesc command)
        {
            SuiteExecutor.StoreValue(command.Parameter, scriptResult.ToString());
        }
    }
}
