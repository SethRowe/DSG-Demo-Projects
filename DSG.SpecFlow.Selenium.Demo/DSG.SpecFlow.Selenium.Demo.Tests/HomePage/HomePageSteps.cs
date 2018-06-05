using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace DSG.SpecFlow.Selenium.Demo.Tests
{
    [Binding]
    public class HomePageSteps : SeleniumStepsBase
    {
        [Given(@"I go to the home page")]
        public void GivenIGoToTheHomePage()
        {
            Driver.Navigate().GoToUrl(BaseUrl);
            WaitForPageToLoad();
        }

        [When(@"I click on the About link")]
        public void WhenIClickOnTheAboutLink()
        {
            var aboutLink = Driver.FindElement(By.Id("navbar-about"));
            aboutLink.Click();
            WaitForPageToLoad();
        }

        [When(@"I click on the Contact link")]
        public void WhenIClickOnTheContactLink()
        {
            var aboutLink = Driver.FindElement(By.Id("navbar-contact"));
            aboutLink.Click();
            WaitForPageToLoad();
        }

        [Then(@"it should have a welcome carousel")]
        public void ThenItShouldHaveAWelcomeCarousel()
        {
            AssertElementExists(By.ClassName("carousel"));
        }

        [Then(@"the carousel should have a ""(.*)"" button")]
        public void ThenTheCarouselShouldHaveAMessage(string buttonText)
        {
            // We can't match on text directly, so we'll grab all the buttons...
            var buttons = Driver.FindElements(By.CssSelector(".carousel .carousel-inner .item.active .btn"));
            
            // ... and check each one to see if it's a match
            if (buttons.Any(btn => string.Compare(btn.Text, buttonText, StringComparison.CurrentCultureIgnoreCase) == 0))
                return;

            Assert.Fail($"No button with text \"{buttonText}\" was found within the active carousel item");
        }

        [Then(@"I should be taken to the About page")]
        public void ThenIShouldBeTakenToTheAboutPage()
        {
            Assert.IsTrue(Driver.Title.ToLower().StartsWith("about -"), "Page title should start with \"About\"");
            Assert.IsTrue(Driver.Url.ToLower().EndsWith("/home/about"), "URL should be \"/home/about\"");
        }

        [Then(@"I should be taken to the Contact page")]
        public void ThenIShouldBeTakenToTheContactPage()
        {
            Assert.IsTrue(Driver.Title.ToLower().StartsWith("contact -"), "Page title should start with \"Contact\"");
            Assert.IsTrue(Driver.Url.ToLower().EndsWith("/home/contact"), "URL should be \"/home/contact\"");
        }
    }
}