using System.Collections.Generic;

namespace Sekure.Models
{
    public class ExecutableProduct
    {
        public string MarketingTracking { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public PolicyHolder PolicyHolder { get; set; }
        public List<InputParameter> Parameters { get; set; }

        public ExecutableProduct() { }

        public ExecutableProduct(ProductDetail productDetail, PolicyHolder policyHolder, List<InputParameter> parameters, string marketingTracking)
        {
            ProductDetail = productDetail;
            PolicyHolder = policyHolder;
            Parameters = parameters;
            MarketingTracking = marketingTracking;
        }
    }

    public class InputParameter
    {
        public string Name { get; set; }
        public int InputParameterId { get; set; }
        public string InputParameterType { get; set; }
        public string InputParameterValue { get; set; }
        public string InputParameterDescription { get; set; }
        public string InputParameterRequired { get; set; }
        public bool ShowApi { get; set; }
        public List<ParameterSchema> InputParameterSchemaList { get; set; }

        public InputParameter() { }
    }

    public class ParameterSchema
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string PropertyDescription { get; set; }
        public string PropertyTypeDescription { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyTypeListValue { get; set; }
        public string PropertyRequired { get; set; }
        public string IsAssistanceType { get; set; }

        public ParameterSchema() { }
    }
}