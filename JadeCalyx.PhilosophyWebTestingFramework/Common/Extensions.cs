using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Extensions
    {
        //**** extensions for List<> ***
        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <param name="theList">The list.</param>
        /// <returns></returns>
        public static bool IsEmpty(this IList<string> theList)
        {
            return theList.Count.Equals(0);
        }
        /// <summary>
        /// Formats for printing.
        /// </summary>
        /// <param name="theList">The list.</param>
        /// <returns></returns>
        public static string FormatForPrinting(this IList<string> theList)
        {
            return String.Join("\n",theList.ToArray());
        }


    }
}
