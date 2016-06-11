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
    /// <summary>Contains common functionality for all Coypu tests.</summary>
    public class BaseCoypuTest : IDisposable
    {
        protected string CurrentMoment { get; private set; }
        protected SessionConfiguration SessionConfiguration { get; private set; }
        protected BrowserSession BrowserSession { get; private set; }

        public BaseCoypuTest()
        {
            IisExpressHelper.StartIis();
            SessionConfiguration = new SessionConfiguration
            {
                AppHost = "localhost",
                Port = 54401,
                SSL = false,
                Driver = typeof(SeleniumWebDriver),
                Browser = Browser.Chrome
            };

            CurrentMoment = DateTime.UtcNow.Ticks.ToString();
            BrowserSession = new BrowserSession(SessionConfiguration);
        }

        public void Dispose()
        {
            ((IDisposable)BrowserSession).Dispose();
            IisExpressHelper.StopIis();
        }
    }

    public class PagedCoypuTest : BaseCoypuTest
    {
        public PagedCoypuTest(string page)
        {
            BrowserSession.Visit(page);
        }
    }
}
