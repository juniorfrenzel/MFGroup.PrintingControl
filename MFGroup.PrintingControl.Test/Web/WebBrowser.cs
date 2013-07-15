using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.IE;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace MFGroup.PrintingControl.Test.Web
{
    [Binding]
    public class WebBrowser
    {
        private const string WEBBROWSER = "ScenarioWebBrowser";

        public IWebDriver Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey(WEBBROWSER))
                {
                    ScenarioContext.Current[WEBBROWSER] = new InternetExplorerDriver();
                }

                return (IWebDriver)ScenarioContext.Current[WEBBROWSER];
            }
        }

        [AfterScenario]
        public void Close()
        {
            Current.Close();
        }
    }
}
