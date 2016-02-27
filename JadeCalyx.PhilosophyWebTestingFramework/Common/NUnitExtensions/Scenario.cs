using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Common.NUnitExtensions
{
    public class Scenario : PropertyAttribute
    {
        public Scenario(string text) : base("Scenario", editText(text)) { }

        private static string editText(string text)
        {
            var r = new Regex("\r\n[\\s]*");
            return r.Replace(text, "; ");
        }
    }
}
