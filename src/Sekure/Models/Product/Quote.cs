using System;
using System.Collections.Generic;

namespace Sekure.Models
{
    public class Quote
    {
        public Guid SessionId { get; set; }
        public string IdPlan { get; set; }
        public string PlanNumber { get; set; }
        public string PlanName { get; set; }
        public string QuotationValueMonthly { get; set; }
        public string QuotationValueAnnual { get; set; }
        public string ProductName { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string CoverageStartDate { get; set; }
        public string CoverageEndDate { get; set; }
        public string ExpeditionDate { get; set; }
        public string QuoteMessage { get; set; }
        public string AmountInsured { get; set; }
        public string QuotationTotal { get; set; }
        public string PolicyNumber { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailPolicy { get; set; }
        public string AttachName { get; set; }

        public List<CoverageResultApi> CoverageResultList { get; set; }
        public List<CoverageResultApi> AggregatesResultList { get; set; }
        public List<InfoResultApi> InsuredResultList { get; set; }
        public List<InfoResultApi> GracePeriodsList { get; set; }
        public List<InfoResultApi> DeductibleList { get; set; }
        public List<InputParameter> OtherInformationOS { get; set; }
        public List<AdditionalInsured> AdditionalInsured { get; set; }
        public EndOfValidityPremium EndOfValidityPremium { get; set; }
        public List<AdditionalInfo> QuoteInfo { get; set; }

        public Quote()
        {

        }

        public Quote(Guid sessionId, string idPlan, string planNumber, string planName, string quotationValueMonthly, string quotationValueAnnual, string productName, string insuranceCompanyName, string coverageStartDate, string coverageEndDate, string expeditionDate, string quoteMessage, string amountInsured, string quotationTotal, string policyNumber, string emailSubject, string emailBody, string emailPolicy, string attachName, List<CoverageResultApi> coverageResultList, List<CoverageResultApi> aggregatesResultList, List<InfoResultApi> insuredResultList, List<InfoResultApi> gracePeriodsList, List<InfoResultApi> deductibleList, List<InputParameter> otherInformationOS, List<AdditionalInsured> additionalInsured, EndOfValidityPremium endOfValidityPremium, List<AdditionalInfo> quoteInfo)
        {
            SessionId = sessionId;
            IdPlan = idPlan;
            PlanNumber = planNumber;
            PlanName = planName;
            QuotationValueMonthly = quotationValueMonthly;
            QuotationValueAnnual = quotationValueAnnual;
            ProductName = productName;
            InsuranceCompanyName = insuranceCompanyName;
            CoverageStartDate = coverageStartDate;
            CoverageEndDate = coverageEndDate;
            ExpeditionDate = expeditionDate;
            QuoteMessage = quoteMessage;
            AmountInsured = amountInsured;
            QuotationTotal = quotationTotal;
            PolicyNumber = policyNumber;
            EmailSubject = emailSubject;
            EmailBody = emailBody;
            EmailPolicy = emailPolicy;
            AttachName = attachName;
            CoverageResultList = coverageResultList;
            AggregatesResultList = aggregatesResultList;
            InsuredResultList = insuredResultList;
            GracePeriodsList = gracePeriodsList;
            DeductibleList = deductibleList;
            OtherInformationOS = otherInformationOS;
            AdditionalInsured = additionalInsured;
            EndOfValidityPremium = endOfValidityPremium;
            QuoteInfo = quoteInfo;
        }
    }

    public class CoverageResultApi
    {
        public string TypeCoverage { get; set; }
        public string NameResult { get; set; }
        public string ValueResult { get; set; }
        public string DescriptionResult { get; set; }
        public string DeductibleResult { get; set; }
        public string IsAssistanceResult { get; set; }
        public string AmountInsurance { get; set; }

        public CoverageResultApi() { }
    }
    public class InfoResultApi
    {
        public string NameResult { get; set; }
        public string ValueResult { get; set; }
        public string ComentsResult { get; set; }

        public InfoResultApi() { }
    }

    public class EndOfValidityPremium
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Comments { get; set; }

    }
}
