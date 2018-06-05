using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace DSG.SpecFlow.Selenium.Demo.Tests.Dropdowns
{
    [Binding]
    public class ProductsSteps : SeleniumStepsBase
    {
        [Given(@"I am on the products page")]
        public void GivenIAmOnTheProductsPage()
        {
            Driver.Navigate().GoToUrl(BaseUrl + "products");
            WaitForPageToLoad();
        }
        
        [When(@"I select the ""(.*)"" category")]
        public void WhenISelectTheCategory(string optionText)
        {
            var mainSelect = new SelectElement(Driver.FindElement(By.CssSelector("#product-category select")));
            mainSelect.SelectByText(optionText);
        }

        [When(@"I select the ""(.*)"" sub-category")]
        public void WhenISelectTheSub_Category(string optionText)
        {
            var mainSelect = new SelectElement(Driver.FindElement(By.CssSelector("#product-sub-category select")));
            mainSelect.SelectByText(optionText);
        }
        
        [Then(@"the sub-category dropdown should be shown")]
        public void ThenTheSubCategoryDropDownShouldBeShown()
        {
            AssertElementExists(By.CssSelector("#product-sub-category select"));
        }

        [Then(@"the sub-category dropdown should not be shown")]
        public void ThenTheSubCategoryDropDownShouldNotBeShown()
        {
            AssertElementDoesNotExist(By.CssSelector("#product-sub-category select"));
        }

        [Then(@"the product dropdown should be shown")]
        public void ThenTheProductDropdownShouldBeShown()
        {
            AssertElementExists(By.CssSelector("#product select"));
        }

        [Then(@"the sub-product dropdown should be shown")]
        public void ThenTheSub_ProductDropdownShouldBeShown()
        {
            AssertElementExists(By.CssSelector("#sub-product select"));
        }

        [Then(@"the product dropdown should not be shown")]
        public void ThenTheProductDropdownShouldNotBeShown()
        {
            AssertElementDoesNotExist(By.CssSelector("#product select"));
        }

        [Then(@"the sub-product dropdown should not be shown")]
        public void ThenTheSub_ProductDropdownShouldNotBeShown()
        {
            AssertElementDoesNotExist(By.CssSelector("#sub-product select"));
        }
    }
}