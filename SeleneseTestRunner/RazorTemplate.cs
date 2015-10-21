using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleneseTestRunner
{
    public class RazorTemplate<T>
    {
        public T Model { get; set; }

        public string Include(string viewname, object model = null)
        {
            return null;
        }
    }
}
