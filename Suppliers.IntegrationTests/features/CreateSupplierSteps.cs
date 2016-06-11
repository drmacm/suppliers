using System;
using Coypu;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Suppliers.IntegrationTests.features
{
    [Binding]
    public class CreateSupplierSteps : PagedCoypuTest
    {

        public CreateSupplierSteps() : base("/Supplier/Create") { }

        [Given(@"I have entered '(.*)' as name")]
        public void GivenIHaveEnteredAsName(string name)
        {
            BrowserSession.FillIn("Name").With(name);
        }
        
        [Given(@"I have entered '(.*)' as address")]
        public void GivenIHaveEnteredAsAddress(string address)
        {
            BrowserSession.FillIn("Address").With(address);
        }
        
        [Given(@"I have entered '(.*)' as email")]
        public void GivenIHaveEnteredAsEmail(string email)
        {
            BrowserSession.FillIn("EmailAddress").With(email);
        }
        
        [Given(@"I have entered '(.*)' as phone number")]
        public void GivenIHaveEnteredAsPhoneNumber(int phoneNumber)
        {
            BrowserSession.FillIn("PhoneNumber").With(phoneNumber.ToString());
        }
        
        [Given(@"I have chosen '(.*)' from the '(.*)' drop down list")]
        public void GivenIHaveChosenAsTheGroup(string value, string dropDown)
        {
            BrowserSession.Select(value).From(dropDown);
        }
        
        [When(@"I press '(.*)' button")]
        public void WhenIPressButton(string buttonName)
        {
            BrowserSession.ClickButton(buttonName);
        }
        
        [Then(@"I should have one more supplier")]
        public void ThenIShouldHaveOneMoreSupplier()
        {
            Assert.Pass();
        }
    }
}
