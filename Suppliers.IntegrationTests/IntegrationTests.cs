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
    public class IntegrationTests : BaseCoypuTest
    {
        [Test]
        public void CreateNewSupplier()
        {
            using (var browser = new BrowserSession(SessionConfiguration))
            {
                browser.Visit("/");
                var numberOfSupplierRows = browser.FindAllXPath("//tr").Count();

                browser.Visit("/Supplier/Create");

                browser.FillIn("Name").With("name_" + CurrentMoment);
                browser.FillIn("Address").With("address_" + CurrentMoment);
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
            using (var browser = new BrowserSession(SessionConfiguration))
            {
                browser.Visit("/Supplier/Edit/1");
                browser.FillIn("Name").With("name_" + CurrentMoment);
                browser.ClickButton("Save");

                var newName = browser.FindXPath("//tr[2]/td[1]").InnerHTML.Trim();

                Assert.AreEqual("name_" + CurrentMoment, newName);
            }
        }
    }
}
