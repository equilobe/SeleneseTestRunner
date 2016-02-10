using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SeleneseTestRunner.Commands;
using SeleneseTestRunner.Stats;
using SeleneseTestRunner.Suites;
using SeleneseTestRunner.Razor;

namespace SeleneseTestRunner
{
    class Program
    {

        static void Main(string[] args)
        {
            //TestStats.ShowTestStats(@"..\..\..\..\BPApp\e2e-tests");
            try
            {
                var result = SuiteExecutor.Execute(@"..\..\..\..\BPApp\e2e-tests\Member\MemberSuite.html", "https://test.boardprospects.com");
                var view = new RazorParser().Parse("SuiteResult", result);
                File.WriteAllText(@"test-results.html", view);
            }
            catch (Exception ex)
            {
                do
                {
                    Console.WriteLine(ex.Message);
                    ex = ex.InnerException;
                } while (ex != null);
            }
        }
    }




}
