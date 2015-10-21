using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleneseTestRunner.Commands;

namespace SeleneseTestRunner.Tests
{
    class TestExecutor
    {
        public static TestResult ExecuteTest(IWebDriver driver, string file)
        {
            try
            {
                var result = new TestResult { Path = file };
                var commands = new TestLoader().LoadFromFile(file);
                
                foreach (var command in commands)
                    ProcessCommand(driver, result, command);
                return result;
            }
            catch (Exception ex)
            {
                return new TestResult { Path = file, HasError = true, IsFailed = true, Comments = ex.Message };
            }
        }

        private static void ProcessCommand(IWebDriver driver, TestResult result, CommandDesc command)
        {
            if (result.IsFailed)
                ProcessCommandAfterTestFailed(result, command);
            else
                ProcessCommandBeforeTestFailed(driver, result, command);
        }

        private static void ProcessCommandAfterTestFailed(TestResult result, CommandDesc command)
        {
            result.CommandResults.Add(new CommandResult { Command = command, IsSkipped = true });
        }

        private static void ProcessCommandBeforeTestFailed(IWebDriver driver, TestResult result, CommandDesc command)
        {
            var commandResult = CommandExecutor.Execute(driver, command);

            if (commandResult.HasError)
            {
                result.HasError = true;
                if (command.IsAssert)
                    result.IsFailed = true;
            }

            result.CommandResults.Add(commandResult);
        }
    }


}
