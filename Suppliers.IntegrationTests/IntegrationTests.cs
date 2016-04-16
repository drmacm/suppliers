using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coypu;
using Coypu.Drivers;
using Coypu.Drivers.Selenium;
using NUnit.Framework;

namespace Suppliers.IntegrationTests
{
    [TestFixture]
    public class IntegrationTests
    {
        private string currentMoment;
        [OneTimeSetUp]
        public void BeforeTests()
        {
            currentMoment = DateTime.UtcNow.Ticks.ToString();
            IisExpressHelper.StartIis();
        }

        [OneTimeTearDown]
        public void AfterTests()
        {
            IisExpressHelper.StopIis();
        }

        private SessionConfiguration sessionConfiguration = new SessionConfiguration
        {
            AppHost = "localhost",
            Port = 54401,
            SSL =  false,
            Driver = typeof(SeleniumWebDriver),
            Browser = Browser.Chrome
        };

        [Test]
        public void CreateNewSupplier()
        {
            using (var browser = new BrowserSession(sessionConfiguration))
            {
                browser.Visit("/");
                var numberOfSupplierRows = browser.FindAllXPath("//tr").Count();

                browser.Visit("/Supplier/Create");

                browser.FillIn("Name").With("name_" + currentMoment);
                browser.FillIn("Address").With("address_" + currentMoment);
                browser.FillIn("EmailAddress").With("automated.test.address@domain.com");
                browser.FillIn("PhoneNumber").With("123123123");
                browser.Select("Security").From("GroupId");

                browser.ClickButton("Create");

                var numberOfSupplierRowsAfterCreation = browser.FindAllXPath("//tr").Count();

                Assert.AreEqual("http://localhost:54401/", browser.Location.ToString());                     //redirected to index page
                Assert.AreEqual(numberOfSupplierRowsAfterCreation, numberOfSupplierRows + 1);               //we have one more row
            }
        }

        [Test]
        public void EditExistingSupplier()
        {
            using (var browser = new BrowserSession(sessionConfiguration))
            {
                browser.Visit("/Supplier/Edit/1");
                browser.FillIn("Name").With("name_" + currentMoment);
                browser.ClickButton("Save");

                var newName = browser.FindXPath("//tr[2]/td[1]").InnerHTML.Trim();

                Assert.AreEqual("name_" + currentMoment, newName);
            }
        }
    }
}
