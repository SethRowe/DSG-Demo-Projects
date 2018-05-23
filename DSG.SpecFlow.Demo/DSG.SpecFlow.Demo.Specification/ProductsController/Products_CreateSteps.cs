using System.Linq;
using System.Net;
using DSG.SpecFlow.Demo.Specification.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace DSG.SpecFlow.Demo.Specification.ProductsController
{
    [Binding]
    public class Products_CreateSteps
    {
        private dynamic _product = null;
        private object _serviceResponse = null;
        private HttpStatusCode _statusCode;

        [Given(@"a new and valid product")]
        public void GivenANewAndValidProduct()
        {
            _product = new
            {
                name = "new product name",
                description = "new product description"
            };
        }

        [Given(@"a product missing a name")]
        public void GivenAProductMissingAName()
        {
            _product = new
            {
                name = "",
                description = "new product description"
            };
        }

        [When(@"I call POST api/product")]
        public void WhenICallPOSTApiProduct()
        {
            var apiResponse = HttpClientHelper.POST("api/product", _product);

            _serviceResponse = apiResponse.Object;
            _statusCode = apiResponse.StatusCode;
        }

        [Then(@"the result should be 201 Created")]
        public void ThenTheResultShouldBe201Created()
        {
            Assert.IsNotNull(_statusCode, "Status code was not set");
            Assert.AreEqual("Created", _statusCode.ToString());
        }

        [Then(@"the result should contain the new id")]
        public void ThenTheResultShouldContainTheNewId()
        {
            var validId = int.TryParse(_serviceResponse.ToString(), out var productId);

            Assert.IsTrue(validId, "Unable to parse service response into product id");
            Assert.IsTrue(productId > 0, "Product id was not greater than zero, and likely invalid");
        }

        [Then(@"the result should be 400 BadRequest")]
        public void ThenTheResultShouldBeBadRequest()
        {
            Assert.IsNotNull(_statusCode, "Status code was not set");
            Assert.AreEqual("BadRequest", _statusCode.ToString());
        }

        [Then(@"the result should contain a validation message")]
        public void ThenTheResultShouldContainAValidationMessage()
        {
            dynamic response = _serviceResponse;
            var validationMessages = ((JArray) response.validationMessages).ToObject<string[]>();

            Assert.IsTrue(validationMessages.Any(x => x == "product name is required"));
        }

    }
}
