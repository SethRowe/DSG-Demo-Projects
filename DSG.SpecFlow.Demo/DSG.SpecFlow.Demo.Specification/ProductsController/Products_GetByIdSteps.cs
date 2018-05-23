using System;
using System.Net;
using DSG.SpecFlow.Demo.Specification.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace DSG.SpecFlow.Demo.Specification
{
    [Binding]
    public class Products_GetByIdSteps
    {
        private int _productId = -1;
        private dynamic _product = null;
        private HttpStatusCode _statusCode;

        [Given(@"A product id of (.*)")]
        public void GivenAProductIdOf(int productId)
        {
            _productId = productId;
        }

        [Given(@"an invalid product id")]
        public void GivenAnInvalidProductId()
        {
            _productId = -99999;
        }

        [When(@"I call GET api/product/id")]
        public void WhenICallGETApiProductsById()
        {
            var apiResponse = HttpClientHelper.GET($"api/product/{_productId}");

            _product = apiResponse.Object;
            _statusCode = apiResponse.StatusCode;
        }

        [Then(@"the result should have an id of (.*)")]
        public void ThenTheResultShouldHaveAnIdOf(int productId)
        {
            AssertProductWasFound();
            Assert.AreEqual(productId, (int)_product.id);
        }

        [Then(@"the result should have a name of '(.*)'")]
        public void ThenTheResultShouldHaveANameOf(string name)
        {
            AssertProductWasFound();
            Assert.AreEqual(name, (string)_product.name);
        }

        [Then(@"the result should have a description of '(.*)'")]
        public void ThenTheResultShouldHaveADescriptionOf(string desc)
        {
            AssertProductWasFound();
            Assert.AreEqual(desc, (string)_product.description);
        }

        [Then(@"the result should be 404 NotFound")]
        public void ThenTheResultShouldBe404NotFound()
        {
            Assert.IsNotNull(_statusCode, "Status code was not set");
            Assert.AreEqual("NotFound", _statusCode.ToString());
        }

        private void AssertProductWasFound()
        {
            // Make sure we got a product
            Assert.IsNotNull(_product, "Product not retrieved from API");
        }
    }
}
