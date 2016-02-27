using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Common.NUnitExtensions
{
    public class Bugs : PropertyAttribute
    {
        public Bugs(string text) : base("Bugs", editText(text)) { }

        private static string editText(string text)
        {
            return text;
        }
    }
}
