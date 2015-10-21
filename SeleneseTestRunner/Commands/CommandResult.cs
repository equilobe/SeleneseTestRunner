using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner.Commands
{
    public class CommandResult
    {
        public CommandDesc Command { get; set; }
        public bool IsSkipped { get; set; }
        public bool HasError { get; set; }
        public bool HasWarning { get; set; }
        public string Comments { get; set; }
    }
}
