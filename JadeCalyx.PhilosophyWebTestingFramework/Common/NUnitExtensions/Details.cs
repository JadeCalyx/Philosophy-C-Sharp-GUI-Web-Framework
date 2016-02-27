using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Common.NUnitExtensions
{
    public class Details : PropertyAttribute
    {
        public Details(string text) : base("Details", editText(text)) { }

        private static string editText(string text)
        {
            return text;
        }
    }
}
