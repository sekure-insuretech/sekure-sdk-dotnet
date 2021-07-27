using Sekure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sekure.Runtime
{
    public interface IInsuranceOS
    {
        Task<Policy> Confirm(ExecutableProduct executableProduct, Guid sessionId);
        Task<string> Emit(ExecutableProduct executableProduct, Guid sessionId);
        Task<PaymentGatewayProduct> GetPaymentGatewayConfiguration(string paymentGatewayName, string productName);
        Task<PaymentStatus> GetPaymentStatus(PaymentDetail paymentDetail);
        Task<Product> GetProductById(int id);
        Task<List<ProductReference>> GetProducts();
        Task<string> GetProductStage(Guid sessionId);
        Task<string> Pay(PaymentDetail paymentDetail);
        Task<QuotedProduct> Quote(ExecutableProduct executableProduct, string customerEmail);
    }
}