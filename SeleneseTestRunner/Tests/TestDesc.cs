using SeleneseTestRunner.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Tests
{
    public class TestDesc
    {
        public string Name { get; set; }
        public IEnumerable<CommandDesc> Commands { get; set; }
    }
}
