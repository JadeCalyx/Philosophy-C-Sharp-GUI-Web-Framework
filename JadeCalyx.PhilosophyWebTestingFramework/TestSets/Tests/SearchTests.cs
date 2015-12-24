using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcWebGuiTools;
using NUnit.Framework;
using TestSets.Utilities;


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
        private jcBrowserFactory _browserFactory;
        [OneTimeSetUp]
        public void ClassSetup()
        {
            _appFile = new AppFile();
            _browserFactory = new jcBrowserFactory("Wikipedia", _appFile.WebPrefix);
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
        public void PerformValidMainPageSearch(string browserType)
        {
            _browser = _browserFactory.GetBrowser(browserType);
            _browser.Maximize();
            _browser.GotoPage("main-page");
            var currPage = _browser.GetPage();
            currPage.SetText("search-box", "archery");
            currPage.Click("search-button");
            var pageChanged = _browser.WaitForPageChange();
            Assume.That(pageChanged, "Page never changed from main search page");
            var newPage = _browser.GetPage();
            Assert.That(newPage.Handle.Equals("archery-page"), "error message");
        }

    }
}
