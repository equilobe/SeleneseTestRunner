using OpenQA.Selenium;

namespace SeleneseTestRunner
{
    interface ICommand
    {
        CommandResult Execute(IWebDriver driver, CommandDesc command);
    }

    class CommandDesc
    {
        public string Name { get; set; }
        public string Selector { get; set; }
        public string Parameter { get; set; }

        public bool IsAssert
        {
            get
            {
                return Name.ToLower().StartsWith("assert");
            }
        }
    }

    class CommandResult
    {
        public CommandDesc Command { get; set; }
        public bool IsSkipped { get; set; }
        public bool HasError { get; set; }
        public bool HasWarning { get; set; }
        public string Comments { get; set; }
    }
}