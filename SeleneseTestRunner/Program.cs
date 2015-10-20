using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SeleneseTestRunner.Commands;
using SeleneseTestRunner.Executors;
using SeleneseTestRunner.Loaders;

namespace SeleneseTestRunner
{
    class Program
    {

        static void Main(string[] args)
        {
            //TestStats.ShowTestStats();
            try
            {
                SuiteExecutor.Execute(null, null);
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
