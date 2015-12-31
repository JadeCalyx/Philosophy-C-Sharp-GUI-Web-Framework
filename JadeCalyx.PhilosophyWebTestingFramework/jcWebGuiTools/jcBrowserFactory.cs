using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thisClass = jcWebGuiTools.jcBrowserFactory;

namespace jcWebGuiTools
{
    /// <summary>
    /// A factory to create browser instances.
    /// </summary>
    public class jcBrowserFactory
    {
        public thisClass to, and;
        string _site;
        string _prefix;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcBrowserFactory"/> class.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="prefix">The prefix.</param>
        public jcBrowserFactory(string site, string prefix)
        {
            to = and = this;
            _site = site;
            _prefix = prefix;
        }
        /// <summary>
        /// Creates a new instance of a browser and returns it.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public jcBrowser GetBrowser(string type)
        {
            return new jcBrowser(type, _site, _prefix);
        }
        /// <summary>
        /// Alias for GetBrowser.
        /// </summary>
        /// <param name="browser_type">The browser_type.</param>
        /// <returns></returns>
       public jcBrowser Get_a_new_browser_instance(string of_type)
        {
            return GetBrowser(of_type);
        }
        /// <summary>
        /// Alias for GetBrowser.
        /// </summary>
        /// <param name="browser_type">The browser_type.</param>
        /// <returns></returns>
        public jcBrowser open_a_browser(string of_type)
        {
            return GetBrowser(of_type);
        }


        /// <summary>
        /// Alias for GetBrowser.
        /// </summary>
        /// <param name="browser_type">The browser_type.</param>
        /// <returns></returns>
        public jcBrowser GetANewBrowserInstance(string browser_type)
        {
            return GetBrowser(browser_type);
        }


    }
}
