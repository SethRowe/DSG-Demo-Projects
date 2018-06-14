using System;

namespace DSG.Sample.NetCore.BaseApi.Middleware
{
    public class ApiPartner
    {
        public int PartnerId { get; set; }
        public string PartnerKey { get; set; }
        public int ApiId { get; set; }
        public string ExternalId { get; set; }
        public string OrganizationName { get; set; }
        public string PrimaryContactName { get; set; }
        public string PrimaryContactPhone { get; set; }
        public string PrimaryContactEmail { get; set; }
        public int DefaultTokenDurationMin { get; set; }
        public int ExtendedTokenDurationMin { get; set; }
        public DateTime CreatedDttm { get; set; }
        public string CreatedUser { get; set; }
        public DateTime UpdatedDttm { get; set; }
        public string UpdatedUser { get; set; }
        public string Status { get; set; }
    }
}