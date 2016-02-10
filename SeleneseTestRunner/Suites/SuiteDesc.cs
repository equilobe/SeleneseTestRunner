using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Suites
{
    public class SuiteDesc
    {
        public string Name { get; set; }
        public IEnumerable<TestLocation> Tests { get; set; }
    }

    public class TestLocation
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
