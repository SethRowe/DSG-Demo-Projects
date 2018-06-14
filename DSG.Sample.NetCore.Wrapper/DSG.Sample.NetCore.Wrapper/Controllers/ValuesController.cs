using System;
using System.Linq;
using DSG.Sample.NetCore.BaseApi.Middleware;
using DSG.Sample.NetCore.BaseApi.Middleware.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using NFS = DSG.Sample.NetFourSix.Api.Controllers;

namespace DSG.Sample.NetCore.Wrapper.Controllers
{
    public class ValuesController : Controller
    {
        private readonly NFS.IValuesController _nfsValuesController;
        private readonly RequestProperties _requestProperties;

        public ValuesController(NFS.IValuesController nfsValuesController, RequestProperties requestProperties)
        {
            _nfsValuesController = nfsValuesController;
            _requestProperties = requestProperties;
        }

        [HttpGet]
        [Route("api/values")]
        [Produces("application/json")]
        [ResponseMessages(ExceptionMessage = "GetValues.Exception", FailureMessage = "GetValues.Failure")]
        public ActionResult Get()
        {
            // Demo of 'normal' controller code.
            // Passes through to old .NET 4.6 controller, then adds on new values
            var legacyValues = _nfsValuesController.Get();
            var newValues = new [] { "net core one", "net core two" };
            
            return Ok(legacyValues.Union(newValues));
        }

        [HttpGet]
        [Route("api/values/exception-demo")]
        [Produces("application/json")]
        [ResponseMessages(ExceptionMessage = "GetValues.Exception", FailureMessage = "GetValues.Failure")]
        public ActionResult GetWithException()
        {
            // Demo for throwing exception and handling it in custom middleware
            throw new Exception("Sample demo exception");
        }

        [HttpGet]
        [Route("api/values/internal-server-error-demo")]
        [Produces("application/json")]
        [ResponseMessages(ExceptionMessage = "GetValues.Exception", FailureMessage = "GetValues.Failure")]
        public ActionResult GetWithInternalServerError()
        {
            // Demo for returning a 500, and having middleware fill out dev & user messages
            return StatusCode(500);
        }

        // POST api/values
        [HttpPost]
        [Route("api/values/search")]
        [Produces("application/json")]
        [ResponseMessages(ExceptionMessage = "SearchForValues.Exception", FailureMessage = "SearchForValues.Failure")]
        public ActionResult SearchForValues([FromBody]GetValuesFilter filter)
        {
            // Yes... I know a POST for a search isn't RESTful, but I wanted an easy method to use to get some JSON into the body

            return Ok("placeholder");
        }
    }

    public class GetValuesFilter
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string SuperSecretSearch { get; set; }
    }
}