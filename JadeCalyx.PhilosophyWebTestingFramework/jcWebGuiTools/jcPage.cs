using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using thisClass = jcWebGuiTools.jcPage;

namespace jcWebGuiTools
{
    /// <summary>
    /// An object which represents a displayed web page in the browser.
    /// </summary>
    public class jcPage
    {
        public thisClass and, to;
        IWebDriver _driver;
        string _site;
        string _pageHandle;
        jcPageObjectAtlas _objectAtlas;
        /// <summary>
        /// Initializes a new instance of the <see cref="jcPage"/> class.
        /// </summary>
        /// <param name="driver">The selenium driver.</param>
        /// <param name="site">The site handle.</param>
        /// <param name="pageHandle">The page handle.</param>
        public jcPage(IWebDriver driver, string site, string pageHandle)
        {
            and = to = this;
            _driver = driver;
            _site = site;
            _pageHandle = pageHandle;
            _objectAtlas = new jcPageObjectAtlas(_site, _pageHandle);
        }
        /// <summary>
        /// Gets the page handle. Setting is turned off.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        public string Handle
        {
            get { return _pageHandle; }
            set { }
        }
        /// <summary>
        /// Get_the_page_handles this instance.
        /// Alias for Handle;
        /// </summary>
        /// <returns></returns>
        public string Get_the_page_handle()
        {
            return Handle;
        }

        /// <summary>
        /// Determines whether the passed handle is the same as the current handle.
        /// </summary>
        /// <param name="handleToCompare">The handle to compare.</param>
        /// <returns></returns>
        public bool IsCurrentHandle(string handleToCompare)
        {
            return handleToCompare.Equals(_pageHandle);
        }
        /// <summary>
        /// Sets the text of a page object.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <param name="textToSet">The text to set.</param>
        /// <returns></returns>
        public jcPage SetText(string objectHandle, string textToSet)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            el.ThrowIfNotFound(objectHandle).Clear().SetText(textToSet);
            return this;
        }
        /// <summary>
        /// Set_the_text_of_elements the specified object_handle.
        /// Alias for SetText()
        /// </summary>
        /// <param name="object_handle">The object_handle.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public jcPage Set_the_text_of_element(string object_handle, string text)
        {
            return SetText(object_handle, text);
        }


        /// <summary>
        /// Clicks the specified object.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <returns></returns>
        public jcPage Click(string objectHandle)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            el.ThrowIfNotFound(objectHandle).Click();
            return this;
        }
        /// <summary>
        /// Alias for Click()
        /// Clicks the specified object_handle.
        /// </summary>
        /// <param name="object_handle">The object_handle.</param>
        /// <returns></returns>
        public jcPage click(string object_handle)
        {
            return Click(object_handle);
        }


        /// <summary>
        /// Gets the web list.
        /// If element is type of web list (ul or ol) then returns as list of li items.
        /// Otherwise, throws error.
        /// </summary>
        /// <param name="objectHandle">The object handle.</param>
        /// <returns></returns>
        public List<jcElementWrapper> GetWebList(string objectHandle)
        {
            var lookupInfo = _objectAtlas.GetLooukupInfo(objectHandle);
            var el = getElement(lookupInfo, null);
            return el.ThrowIfNotFound(objectHandle).GetAsList();
        }
        /// <summary>
        /// Alias for GetWebList()
        /// Get_webpage_lists the specified object_handle.
        /// </summary>
        /// <param name="object_handle">The object_handle.</param>
        /// <returns></returns>
        public List<jcElementWrapper> get_webpage_list(string object_handle)
        {
            return GetWebList(object_handle);
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="lookupInfo">The lookup information.</param>
        /// <param name="currElement">The element that starts the search.</param>
        /// <returns></returns>
        public jcElementWrapper getElement(Stack<jcPageObjectLookupPair> lookupInfo, IWebElement currElement)
        {
            if (lookupInfo.Count < 1)
            {
                return new jcElementWrapper(currElement);
            }
            IReadOnlyCollection<IWebElement> elements;
            var currLookup = lookupInfo.Pop();
            if (currElement == null)
            {
                elements = _driver.FindElements(By.CssSelector(currLookup.Details));
            }
            else
            {
                elements = currElement.FindElements(By.CssSelector(currLookup.Details));
            } 
            if (elements.Count < 1)
            {
                string err = String.Format("Unable to find element: '{0} {1}'", 
                    currLookup.Method,
                    currLookup.Details);
                return new jcElementWrapper(null).SetErrorMsg(err);
            }
            else
            {
                return getElement(lookupInfo, elements.First());
            }
        }

    }
}
