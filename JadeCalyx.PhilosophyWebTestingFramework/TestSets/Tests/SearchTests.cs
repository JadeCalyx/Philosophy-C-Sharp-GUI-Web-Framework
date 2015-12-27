using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcWebGuiTools;
using NUnit.Framework;
using TestSets.Utilities;
using thisClass = TestSets.Tests.SearchTests;


namespace TestSets.Tests
{
    [Property("Feature", @"
        As a user
        I need to perform searches on the home page
        so I can go quickly find a subject page")]
    class SearchTests
    {
        private AppFile _appFile;
        private jcBrowser _browser;
        private jcBrowser browser;
        private jcBrowserFactory _browserFactory;
        private jcBrowserFactory browser_factory;
        private thisClass I, me, the, to, ask;


        [OneTimeSetUp]
        public void ClassSetup()
        {
            I = me = the = to = ask = this;
            _appFile = new AppFile();
            _browserFactory = new jcBrowserFactory("Wikipedia", _appFile.WebPrefix);
            browser_factory = _browserFactory;
        }

        [OneTimeTearDown]
        public void ClassTeardown()
        {
        }

        [SetUp]
        public void TestSetup()
        {

        }

        [TearDown]
        public void TestTeardown()
        {
            _browser.Close();
        }

        [Test]
        [Category("Search")]
        [Property("Summary", @"
            Perform a valid search from the home page.")]
        [Property("Scenario", @"
            Given I am on the home page
            When I perform a search using the search box
            Then I am taken to the correct page")]
        [Property("Details", @"
            This test performs a valid search on the main page using the search text box.")]
        [Property("Stories", @"
            Wiki-101, Wiki-153")]
        [Property("Bugs", @"
            Wiki-937")]
        [TestCase("firefox")]
        [TestCase("chrome")]
        public void PerformValidMainPageSearch(string browser_type)
        {
            the.browser = I.ask.the.browser_factory.to.Get_a_new_browser_instance(browser_type).
                            and.Maximize_the_window();
            I.ask.the.browser.to.Goto_page("main-page").
                and.Set_the_text_of_element("search-box", text: "archery").
                and.Click("search-button");
            var the_page_handle = I.ask.the.browser.to.Wait_for_the_page_to_change().and.Get_the_page_handle();
            Assert.That(the_page_handle.Equals("archery-page"), "error message");
        }

    }
}
