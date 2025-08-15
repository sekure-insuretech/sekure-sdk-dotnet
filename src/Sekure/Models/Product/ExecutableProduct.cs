using System;
using System.Collections.Generic;

namespace Sekure.Models
{
    public class ExecutableProduct
    {
        public string MarketingTracking { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public PolicyHolder PolicyHolder { get; set; }
        public List<InputParameter> Parameters { get; set; }

        public ExecutableProduct(string marketingTracking, ProductDetail productDetail, PolicyHolder policyHolder, List<InputParameter> parameters)
        {
            MarketingTracking = marketingTracking;
            ProductDetail = productDetail;
            PolicyHolder = policyHolder;
            Parameters = parameters;
        }

        public ExecutableProduct() { }

        public ExecutableProduct(ProductDetail productDetail, List<InputParameter> parameters, string marketingTracking)
        {
            ProductDetail = productDetail;
            Parameters = parameters;
            MarketingTracking = marketingTracking;
        }
    }

    public class InputParameter : ICloneable
    {
        public string Name { get; set; }
        public int InputParameterId { get; set; }
        public string InputParameterType { get; set; }
        public string InputParameterValue { get; set; }
        public string InputParameterDescription { get; set; }
        public string InputParameterRequired { get; set; }
        public bool ShowApi { get; set; }
        public string RegularExpressionPattern { get; set; }
        public string RegularExpressionErrorMessage { get; set; }
        public List<ParameterSchema> InputParameterSchemaList { get; set; }
        public List<InputParameter> InputParameterArrayObjectList { get; set; }

        public InputParameter()
        {
        }
        public object Clone()
        {
            // Crear una nueva instancia de InputParameter.
            InputParameter clonedParameter = new InputParameter
            {
                // Copiar los valores de las propiedades desde la instancia actual a la nueva instancia.
                Name = this.Name,
                InputParameterId = this.InputParameterId,
                InputParameterType = this.InputParameterType,
                InputParameterValue = this.InputParameterValue,
                InputParameterDescription = this.InputParameterDescription,
                InputParameterRequired = this.InputParameterRequired,
                ShowApi = this.ShowApi,
                RegularExpressionPattern = this.RegularExpressionPattern,
                RegularExpressionErrorMessage = this.RegularExpressionErrorMessage,
                InputParameterSchemaList = this.InputParameterSchemaList,
                InputParameterArrayObjectList = this.InputParameterArrayObjectList
            };

            // Clonar las listas si son necesarias (dependiendo de si son objetos mutables).
            if (this.InputParameterSchemaList != null)
            {
                clonedParameter.InputParameterSchemaList = new List<ParameterSchema>(this.InputParameterSchemaList.Count);
                foreach (var schema in this.InputParameterSchemaList)
                {
                    clonedParameter.InputParameterSchemaList.Add((ParameterSchema)schema.Clone());
                }
            }

            if (this.InputParameterArrayObjectList != null)
            {
                clonedParameter.InputParameterArrayObjectList = new List<InputParameter>(this.InputParameterArrayObjectList.Count);
                foreach (var parameter in this.InputParameterArrayObjectList)
                {
                    clonedParameter.InputParameterArrayObjectList.Add((InputParameter)parameter.Clone());
                }
            }

            return clonedParameter;
        }
    }

    public class ParameterSchema : ICloneable
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
        public string RegularExpressionPattern { get; set; }
        public string RegularExpressionErrorMessage { get; set; }
        public bool? IsRegexAllowed { get; set; }

        public object Clone()
        {
            return new ParameterSchema
            {
                PropertyId = this.PropertyId,
                PropertyName = this.PropertyName,
                PropertyValue = this.PropertyValue,
                PropertyDescription = this.PropertyDescription,
                PropertyTypeDescription = this.PropertyTypeDescription,
                PropertyTypeId = this.PropertyTypeId,
                PropertyTypeListValue = this.PropertyTypeListValue,
                PropertyRequired = this.PropertyRequired,
                IsAssistanceType = this.IsAssistanceType,
                IsRegexAllowed = this.IsRegexAllowed
            };
        }
    }
}