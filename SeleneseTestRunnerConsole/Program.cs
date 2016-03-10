using SeleneseTestRunner.Razor;
using SeleneseTestRunner.Stats;
using SeleneseTestRunner.Suites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunnerConsole
{
    class Program
    {
        static string[] acceptedBrowsers = new string[] { "chrome", "firefox", "internetexplorer" };

        static int Main(string[] args)
        {
            //TestStats.ShowTestStats(@"..\..\..\..\BPApp\e2e-tests");

            try
            {
                var parsedArgs = args.Select(t => t.Split('=')).ToDictionary(t => t[0], t => t[1]);

                if (args.Length == 0 || !parsedArgs.ContainsKey("suitePath") || !parsedArgs.ContainsKey("url") || !parsedArgs.ContainsKey("resultFilePath"))
                {
                    Console.WriteLine("Missing parameters: 'suitePath' or 'url'");
                    return 1;
                }


                var browserName = "chrome";
                if (parsedArgs.ContainsKey("browser"))
                {
                    browserName = parsedArgs["browser"].ToLower();
                    if (!acceptedBrowsers.Contains(browserName))
                    {
                        Console.WriteLine("Browser must be one of: ", string.Join(", ", acceptedBrowsers));
                        return 1;
                    }
                }

                var suitePath = parsedArgs["suitePath"];
                var baseUrl = parsedArgs["url"];
                var resultsFile = parsedArgs["resultFilePath"];

                var result = SuiteExecutor.Execute(suitePath, baseUrl, browserName);
                var view = new RazorParser().Parse("SuiteResult", result);
                File.WriteAllText(resultsFile, view);

                return GetReturnCodeFromResult(result);
            }
            catch (Exception ex)
            {
                do
                {
                    Console.WriteLine(ex.Message);
                    ex = ex.InnerException;
                } while (ex != null);

                return 1;
            }
        }

        private static int GetReturnCodeFromResult(SuiteResult result)
        {
            if (result.IsAllSuccess)
                return 0;

            return 1;
        }
    }
}
