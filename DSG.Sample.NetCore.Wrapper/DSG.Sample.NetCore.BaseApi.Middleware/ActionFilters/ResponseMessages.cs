using Microsoft.AspNetCore.Mvc.Filters;

namespace DSG.Sample.NetCore.BaseApi.Middleware.ActionFilters
{
    public class ResponseMessagesAttribute : ActionFilterAttribute
    {
        public string ExceptionMessage { get; set; }
        public string FailureMessage { get; set; }

        public const string ExceptionMessageKey = "ResponseMessages.ExceptionMessage";
        public const string FailureMessageKey = "ResponseMessages.FailureMessage";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items.Add(ExceptionMessageKey, ExceptionMessage);
            context.HttpContext.Items.Add(FailureMessageKey, FailureMessage);
        }
    }
}