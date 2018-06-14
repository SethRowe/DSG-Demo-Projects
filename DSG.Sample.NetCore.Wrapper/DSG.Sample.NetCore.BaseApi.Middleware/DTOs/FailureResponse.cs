namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class FailureResponse
    {
        public long Id { get; set; }
        public string DeveloperMessage { get; set; }
        public string UserMessage { get; set; }
        public string[] FieldValidationErrors { get; set; }
    }
}