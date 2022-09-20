using System.Collections.Generic;

namespace Sekure.Models
{
    public class Product
    {
        public ProductDetail ProductDetail { get; set; }
        public List<object> PolicyHolder { get; set; }
        public List<InputParameter> Quote { get; set; }
        public List<InputParameter> Confirm { get; set; }
        public List<InputParameter> ToEmit { get; set; }
        public List<AskSekure> AskSekure { get; set; }
        public Product() { }

        public Product(ProductDetail productDetail, List<object> policyHolder, List<InputParameter> quote, List<InputParameter> confirm, List<InputParameter> toEmit)
        {
            ProductDetail = productDetail;
            PolicyHolder = policyHolder;
            Quote = quote;
            Confirm = confirm;
            ToEmit = toEmit;
        }
    }

    public class AskSekure
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<InputParameter> Parameters { get; set; }

        public AskSekure() { }

        public AskSekure(int productId, string productName, List<InputParameter> parameters)
        {
            ProductId = productId;
            ProductName = productName;
            Parameters = parameters;
        }
    }
}