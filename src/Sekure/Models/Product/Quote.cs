using System.Collections.Generic;

namespace Sekure.Models
{
    public class Quote
    {
        public string PlanId { get; set; }
        public string PlanNumber { get; set; }
        public string PlanName { get; set; }
        public string InsuredValue { get; set; }
        public string PolicyNumber { get; set; }
        public List<CoverageResultAPI> Coverages { get; set; }
        public List<InfoResultAPI> Deductible { get; set; }
        public string StartDate { get; set; }
        public string TermTime { get; set; }
        public string ExpeditionDate { get; set; }
        public string PremiumAmount { get; set; }
        public string PremiumPaymentInterval { get; set; }
        public List<InfoResultAPI> Beneficiaries { get; set; }
        public List<InfoResultAPI> GracePeriodsList { get; set; }
        public List<InputParameter> AdditionalInfo { get; set; }
        public List<AdditionalInsured> AdditionalInsured { get; set; }

        public Quote() { }

        public Quote(string planId, string planNumber, string planName, string insuredValue, string policyNumber, List<CoverageResultAPI> coverages, List<InfoResultAPI> deductible, string startDate, string termTime, string expeditionDate, string premiumAmount, string premiumPaymentInterval, List<InfoResultAPI> beneficiaries, List<InfoResultAPI> gracePeriodsList, List<InputParameter> additionalInfo, List<AdditionalInsured> additionalInsured)
        {
            PlanId = planId;
            PlanNumber = planNumber;
            PlanName = planName;
            InsuredValue = insuredValue;
            PolicyNumber = policyNumber;
            Coverages = coverages;
            Deductible = deductible;
            StartDate = startDate;
            TermTime = termTime;
            ExpeditionDate = expeditionDate;
            PremiumAmount = premiumAmount;
            PremiumPaymentInterval = premiumPaymentInterval;
            Beneficiaries = beneficiaries;
            GracePeriodsList = gracePeriodsList;
            AdditionalInfo = additionalInfo;
            AdditionalInsured = additionalInsured;
        }
    }

    public class CoverageResultAPI
    {
        public string NameResult { get; set; }
        public string ValueResult { get; set; }
        public string DescriptionResult { get; set; }
        public string DeductibleResult { get; set; }
        public string IsAssistanceResult { get; set; }

        public CoverageResultAPI() { }
    }
    public class InfoResultAPI
    {
        public string NameResult { get; set; }
        public string ValueResult { get; set; }
        public string ComentsResult { get; set; }

        public InfoResultAPI() { }
    }
}
