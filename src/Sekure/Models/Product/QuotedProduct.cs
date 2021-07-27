using System;
using System.Collections.Generic;

namespace Sekure.Models
{
    public class QuotedProduct
    {
        public Guid SessionId { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public PolicyHolder PolicyHolder { get; set; }
        public List<Quote> Quotes { get; set; }
        public bool PaymentGatewaySkr { get; set; }

        public QuotedProduct(ProductDetail productDetail, PolicyHolder policyHolder, List<Quote> quotes, bool paymentGatewaySkr)
        {
            ProductDetail = productDetail;
            PolicyHolder = policyHolder;
            Quotes = quotes;
            PaymentGatewaySkr = paymentGatewaySkr;
        }
    }
}
