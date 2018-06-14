using System;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class ApiToken
    {
        public long TokenId { get; set; }
        public string TokenKey { get; set; }
        public AuthenticationProvider AuthenticationProvider { get; set; }
        public DateTime TokenExpirationDttm { get; set; }
        public int PartnerId { get; set; }
        public string CorrelationId1 { get; set; }
        public string CorrelationId2 { get; set; }
        public string CorrelationId3 { get; set; }
        public DateTime CreatedDttm { get; set; }
        public string CreatedUser { get; set; }
        public DateTime UpdatedDttm { get; set; }
        public string UpdatedUser { get; set; }
        public string Status { get; set; }
    }
}