using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;
using thisClass = jcWebGuiTools.jcBrowser;


namespace jcWebGuiTools
{
    /// <summary>
    /// An instance of specific browser.
    /// Provides wrapper for Selenium functions.
    /// </summary>
    public class jcBrowser
    {
        public thisClass to, and, ask, tell, the;
        IWebDriver _driver;
        jcAddressAtlas _addressAtlas;
        string _site;
        jcPageFactory _pageFactory;
        jcPage _currPage = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcBrowser"/> class.
        /// </summary>
        /// <param name="driverType">Type of the driver. Firefox or Chrome. </param>
        /// <param name="site">The site handle or name.</param>
        /// <param name="urlPrefix">The URL prefix. The base url to be prefixed onto any address.</param>
        public jcBrowser(string driverType, string site, string urlPrefix)
        {
            to = and = ask = tell = the = this;
            _site = site;
            _addressAtlas = new jcAddressAtlas(urlPrefix, site);
            setrDriver(driverType);
            _pageFactory = new jcPageFactory(_driver, _addressAtlas, _site);
        }
        /// <summary>
        /// Closes the browser.
        /// Performs a Selenium webdriver.Quit(). 
        /// </summary>
        public void Close()
        {
            _driver.Quit();
        }
        /// <summary>
        /// Gets the current page the browser is displaying.
        /// </summary>
        /// <returns>jcPage object representing curretly displayed page.</returns>
        public jcPage GetPage()
        {
            _currPage = _pageFactory.GetPage(_currPage);
            return _currPage;
        }
        /// <summary>
        /// Alias for GetPage().
        /// </summary>
        /// <returns></returns>
        public jcPage act_on_the_displayed_page()
        {
            return GetPage();
        }
        /// <summary>
        /// Address_the_current_pages this instance.
        /// </summary>
        /// <returns></returns>
        public jcPage page()
        {
            return GetPage();
        }
        /// <summary>
        /// Goes to the web page reprsented by the passed page handle.
        /// </summary>
        /// <param name="handle">The page handle.</param>
        /// <returns>jcBrowser: this browser object.</returns>
        public jcPage GotoPage(string handle)
        {
            var url = _addressAtlas.GetUrl(handle);
            _driver.Navigate().GoToUrl(url);
            return this.GetPage();
        }
        /// <summary>
        /// Alias for GotoPage
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns></returns>
        public jcPage Goto_page(string handle)
        {
            return GotoPage(handle);
        }
        /// <summary>
        /// Alias for GotoPage
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns></returns>
        public jcPage goto_page(string handle)
        {
            return GotoPage(handle);
        }


        /// <summary>
        /// Waits for the page to change.
        /// </summary>
        /// <returns></returns>
        public bool WaitForPageChange()
        {
            var currHandle = "";
            if (_currPage != null)
            {
                currHandle = _currPage.Handle;
            }
            var timeout = 30;
            this.GetPage();
            while ((_currPage.Handle == currHandle) && (timeout-- > 0))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                this.GetPage();
            }
            if (timeout > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Wait_for_the_page_to_changes this instance.
        /// Alias for WaitForPageChange; returns the new page
        /// </summary>
        /// <returns></returns>
        public jcPage Wait_for_the_page_to_change()
        {
            this.WaitForPageChange();
            return GetPage();
        }

        /// <summary>
        /// Goes to a specified url.
        /// </summary>
        /// <param name="url">The full URL to open in the browser.</param>
        /// <returns>jcBrowser: this browser object.</returns>
        public jcBrowser GotoUrl(String url)
        {
            _driver.Navigate().GoToUrl(url);
            return this;
        }
        /// <summary>
        /// Maximizes this browser instance.
        /// </summary>
        /// <returns>jcBrowser: this object instance.</returns>
        public jcBrowser Maximize()
        {
            _driver.Manage().Window.Maximize();
            return this;
        }
        /// <summary>
        /// Alias for Maximize()
        /// </summary>
        /// <returns></returns>
        public jcBrowser Maximize_the_window()
        {
            return this.Maximize();
        }
        /// <summary>
        /// Set the Selenium web driver type.
        /// </summary>
        /// <param name="driverType">Type of the driver, such as firefox.</param>
        /// <exception cref="System.Exception">Throws an exception if passed an unknown driver name.</exception>
        private void setrDriver(string driverType)
        {
            switch (driverType.ToLower())
            {
                case "chrome":
                    _driver = new ChromeDriver();
                    break;

                case "firefox": _driver = new FirefoxDriver();
                    break;
                default: throw new Exception(String.Format("Invalid browser type of {0} specified.", driverType));
            }
        }
    }
}
