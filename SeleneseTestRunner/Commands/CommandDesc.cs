using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands
{
    public class CommandDesc
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
}
