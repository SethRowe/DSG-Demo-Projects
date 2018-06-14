using System.Collections.Generic;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class RequestProperties
    {
        public string AuditName { get; set; }
        public ApiPartner Partner { get; set; }
        public ApiToken AuthenticationToken { get; set; }
        public int RequestedPageIndex { get; set; }
        public int RequestedPageCount { get; set; }
        public int TotalRecordCount { get; set; }
        public int TotalPageCount { get; set; }
        public int ResponsePageIndex { get; set; }
        public int ResponsePageCount { get; set; }
        public List<Link> CustomLinks { get; set; }
    }
}