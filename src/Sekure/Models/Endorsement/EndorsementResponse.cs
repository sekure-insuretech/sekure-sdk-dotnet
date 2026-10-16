using System;

namespace Sekure.Models.Endorsement
{
    public class EndorsementResponse
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public byte ProductType { get; set; }
        public byte EndorsementType { get; set; }
        public string CreditNumber { get; set; }
        public DateTime DisbursementDate { get; set; }
        public DateTime CreditEndDate { get; set; }
        public DateTime CollectiveWithdrawalDate { get; set; }
        public decimal CollectivePremium { get; set; }
        public short InitialYear { get; set; }
        public decimal? OutstandingBalance { get; set; }
        public bool IsRenewal { get; set; }
        public string ClientName { get; set; }
        public string PolicyNumber { get; set; }
        public string InsurerName { get; set; }
        public decimal InsuredValue { get; set; }
        public decimal? PropertyValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte PolicyStatus { get; set; }
        public byte Status { get; set; }
        public string Observation { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
