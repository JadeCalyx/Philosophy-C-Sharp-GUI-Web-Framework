using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thisClass = Common.Toolbox;

namespace Common
{
    /// <summary>
    /// Puts common tools used by multiple tests in one object
    /// to make it easier to access.
    /// </summary>
    public class Toolbox
    {
        public thisClass to, and, the;

        public Toolbox()
        {
            to = and = the = this;
        }

        public List<string> get_a_new_error_container()
        {
            return new List<string>();
        }
    }
}
