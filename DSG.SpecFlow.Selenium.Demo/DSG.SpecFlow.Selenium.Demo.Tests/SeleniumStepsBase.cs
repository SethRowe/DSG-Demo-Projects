using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DSG.SpecFlow.Selenium.Demo.Tests
{
    public abstract class SeleniumStepsBase : IDisposable
    {
        protected readonly IWebDriver Driver;

        protected SeleniumStepsBase()
        {
            Driver = new ChromeDriver();
        }

        protected string BaseUrl => "http://localhost:51135/";

        protected bool VerifyElementExists(By by, double timeoutSeconds = 5.00)
        {
            // This timeout is useful for handling situations where the element isn't there
            // immediately (such as elements that are shown based on an ajax request/response).
            // This way we'll wait UP TO the timeout for the element to appear

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            try
            {
                return wait.Until(d => Driver.FindElements(by).Count != 0);
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected bool VerifyElementDoesNotExist(By by, double timeoutSeconds = 5.00)
        {
            // Similar to VerifyElementExists(...), we'll wait UP TO the timeout limit for the
            // element to disappear

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            try
            {
                return wait.Until(d =>
                {
                    var elements = Driver.FindElements(@by);

                    if (elements.Count == 0)
                        return true;

                    // Maybe we have the element, but it's hidden?
                    return elements.All(x => x.Displayed == false);
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected void AssertElementExists(By by, double timeoutSeconds = 5.00)
        {
            var elementExists = VerifyElementExists(by, timeoutSeconds);
            Assert.IsTrue(elementExists, "Element not found on page");
        }

        protected void AssertElementDoesNotExist(By by, double timeoutSeconds = 5.00)
        {
            var elementDoesNotExist = VerifyElementDoesNotExist(by, timeoutSeconds);
            Assert.IsTrue(elementDoesNotExist, "Element found on page, but should not have been");
        }

        protected void WaitForPageToLoad(double timeoutSeconds = 10.00)
        {
            // Please see:
            //   https://stackoverflow.com/questions/5868439/wait-for-page-load-in-selenium

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));

            wait.Until(d => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        #region IDisposable Implementation

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Driver.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}