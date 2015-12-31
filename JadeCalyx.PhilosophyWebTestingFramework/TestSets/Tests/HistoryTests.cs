using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jcWebGuiTools;
using NUnit.Framework;
using TestSets.Utilities;
using Common;
using thisClass = TestSets.Tests.HistoryTests;

namespace TestSets.Tests
{
    class HistoryTests
    {
        private thisClass I, my, ask, the, to;
        private AppFile _appFile;
        private jcBrowser _browser;
        private jcBrowser browser;
        private jcBrowserFactory _browserFactory;
        private jcBrowserFactory browser_factory;
        private Toolbox toolbox = new Toolbox();

        [OneTimeSetUp]
        public void ClassSetup()
        {
            I = my = ask = the = to = this;
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
            browser.Close();
        }

        /// <summary>
        /// Validates that the main page seach performs as expected.
        /// Stories: Wiki-101, Wiki-153
        /// Bugs: Wiki-937
        /// </summary>
        [Test]
        [Category("GUI")]
        [Category("History")]
        [Property("Summary", @"
            Validate the history link takes you to the correct page.")]
        [Property("Scenario", @"
            Given I am on the home page
            When I click the view history link
            Then I am taken to the view history page")]
        [Property("Details", @"
            This test determines if the history link takes you to the correct page.")]
        [Property("Stories", @"")]
        [Property("Bugs", @"")]
        [TestCase("firefox")]
        [TestCase("chrome")]
        public void OpenPageHistory(string browserType)
        {
            the.browser = I.ask.the.browser_factory.to.open_a_browser(of_type: browserType).
                            and.Maximize_the_window();
            I.ask.the.browser.to.goto_page("main-page").and.click("view-history-anchor");
            var the_current_page_handle = I.ask.the.browser.to.Wait_for_the_page_to_change().
                                            and.Get_the_page_handle();
            Assert.That(the_current_page_handle.Equals("view-history-page"),
                String.Format("Landed on wrong page: {0}", the_current_page_handle));
        }

        [Test]
        [Category("GUI")]
        [Category("History")]
        [Property("Summary", @"
            Validate the link to limit the number of entries works as expected.")]
        [Property("Scenario", @"
            Given I am on the home page
            When I click the view history link
            And I click on the link to display a specific number of history items
            Then the correct number of entries is displayed")]
        [Property("Details", @"
            This test validates the link to limit the number of entries works as expected.")]
        [Property("Stories", @"")]
        [Property("Bugs", @"")]
        [TestCase("chrome", "main-page", "limit-to-20-anchor", 20)]
        [TestCase("chrome", "archery-page", "limit-to-50-anchor", 50)]
        [TestCase("chrome", "main-page", "limit-to-100-anchor", 100)]
        [TestCase("chrome", "archery-page", "limit-to-250-anchor", 250)]
        [TestCase("chrome", "main-page", "limit-to-500-anchor", 500)]
        [TestCase("firefox", "archery-page", "limit-to-20-anchor", 20)]
        [TestCase("firefox", "main-page", "limit-to-50-anchor", 50)]
        [TestCase("firefox", "archery-page", "limit-to-100-anchor", 100)]
        [TestCase("firefox", "main-page", "limit-to-250-anchor", 250)]
        [TestCase("firefox", "archery-page", "limit-to-500-anchor", 500)]
        public void FilterHistoryPageListEntries(string browser_type,
            string subjectPage, string anchorToClick, int expectedCount)
        {
            the.browser = I.ask.the.browser_factory.to.open_a_browser(of_type: browser_type).
                            and.Maximize_the_window();
            I.ask.the.browser.to.goto_page(subjectPage).and.Click("view-history-anchor");
            I.ask.the.browser.page().to.Click(anchorToClick);
            var count = I.ask.the.browser.page().to.get_webpage_list("page-history-list").Count;
            Assert.That(count.Equals(expectedCount), 
                String.Format("Wrong number of items. Expected {0}, Found {1}",
                expectedCount, count));
        }

        [Test]
        [Category("GUI")]
        [Category("History")]
        [Property("Summary", @"
            Validate the history entries sort correctly--oldest first.")]
        [Property("Scenario", @"
            Given I am a wikipedia subject page
            When I click the view history link
            And I click on the oldest first link
            Then the history links will sort oldest first")]
        [Property("Details", @"
            This test validates if the history links sort as expected.")]
        [Property("Stories", @"")]
        [Property("Bugs", @"")]
        public void HistoryOrderOldest()
        {
            the.browser = I.ask.the.browser_factory.to.open_a_browser(of_type: "chrome").
                            and.Maximize_the_window();
            I.ask.the.browser.to.goto_page("main-page").and.click("view-history-anchor");
            var history_list = I.ask.the.browser.page().to.click("oldest-anchor").
                                and.get_webpage_list("page-history-list");
            var errors = I.ask.the.toolbox.to.get_a_new_error_container();
            I.validate_the_date_order_oldest_to_newest(list: history_list, error_container: errors);
            Assert.That(errors.IsEmpty(), errors.FormatForPrinting());
        }
        /// <summary>
        /// Validate_the_date_order_oldest_to_newests the specified list.
        /// Code for HistoryOrderOldest()
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="error_container">The error_container.</param>
        private void validate_the_date_order_oldest_to_newest(List<jcElementWrapper> list, List<string> error_container)
        {
            var dateList = new List<DateTime>();
            foreach (var el in list)
            {
                var d = el.FindSubElement("a[class=\"mw-changeslist-date\"]").GetElementText();
                dateList.Add(DateHelper.ConvertWikipediaHistoryDateString(d));
            }
            for (var i = 1; i < dateList.Count; i++)
            {
                var compare = DateTime.Compare(dateList[i - 1], dateList[i]);
                if (compare > 0)
                    error_container.Add(String.Format("Dates out of order. upper date: {0}, lower date {1}",
                        dateList[i - 1], dateList[i]));
            }

        }

        [Test]
        [Category("GUI")]
        [Category("History")]
        [Property("Summary", @"
            Validate the history entries sort correctly--newest first.")]
        [Property("Scenario", @"
            Given I am a wikipedia subject page
            When I click the view history link
            Then the history links will sort newest first")]
        [Property("Details", @"
            This test validates if the history links sort as expected.")]
        [Property("Stories", @"")]
        [Property("Bugs", @"")]
        public void HistoryOrderNewest()
        {
            the.browser = I.ask.the.browser_factory.to.open_a_browser(of_type: "chrome").
                            and.Maximize_the_window();
            I.ask.the.browser.to.goto_page("main-page").and.click("view-history-anchor");
            var history_list = I.ask.the.browser.page().to.get_webpage_list("page-history-list");
            var errors = I.ask.the.toolbox.to.get_a_new_error_container();
            I.validate_the_date_order_newest_to_oldest(list: history_list, error_container: errors);
            Assert.That(condition: errors.IsEmpty(), message: errors.FormatForPrinting());
        }
        /// <summary>
        /// Validate_the_date_order_newest_to_oldests the specified list.
        /// Code for HistoryOrderNewest().
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="error_container">The error_container.</param>
        private void validate_the_date_order_newest_to_oldest(List<jcElementWrapper> list, List<string> error_container)
        {
            var dateList = new List<DateTime>();
            foreach (var el in list)
            {
                var d = el.FindSubElement("a[class=\"mw-changeslist-date\"]").GetElementText();
                dateList.Add(DateHelper.ConvertWikipediaHistoryDateString(d));
            }
            for (var i = 1; i < dateList.Count; i++)
            {
                var compare = DateTime.Compare(dateList[i - 1], dateList[i]);
                if (compare < 0)
                    error_container.Add(String.Format("Dates out of order. upper date: {0}, lower date {1}",
                        dateList[i - 1], dateList[i]));
            }

        }


    }
}
