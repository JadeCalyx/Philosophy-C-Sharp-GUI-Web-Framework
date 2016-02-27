using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Common.NUnitExtensions
{
    public class Stories : PropertyAttribute
    {
        public Stories(string text) : base("Stories", editText(text)) { }

        private static string editText(string text)
        {
            return text;
        }
    }
}
