using Sekure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sekure.Runtime
{
    /// <summary>
    /// Interface for Sekure clients used to make requests to InsuranceOS's API.
    /// </summary>
    public interface IInsuranceOS
    {
        /// <summary>
        /// Returns a list of all the products available in sekure. 
        /// Send a GetAsync request to InsuranceOS API./>.
        /// </summary>
        Task<List<ProductReference>> GetProducts();

        /// <summary>
        /// Returns a discovery of the product, with which information will be obtained about 
        /// how to configure the product at each stage of the process of quotation and obtaining the product.
        /// Send a GetAsync request to InsuranceOS API.
        /// </summary>
        /// <param name="id">This is the product id.</param>
        Task<Product> GetProductById(int id);

        /// <summary>
        /// Returns a discovery of batches of products, which will obtain information about
        /// how to configure the batch at each stage of the quote, confirmation and issuance process
        /// Send a GetAsync request to InsuranceOS API.
        /// </summary>
        /// <param name="name">This is the batch ID.</param>
        Task<BatchDiscovery> GetProductLotByName(string name);

        /// <summary>
        /// This method is responsible for making the product quote according to the entered parameters 
        /// and delivers a sessionId with which the quote made is recorded as well as returns the available quotes of the product
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="executableProduct">This is the object necessary to execute the actions (quote, confirm, emit) in the product purchase process. 
        /// It has essential information about the product, policyholder, and necessary parameters.</param>
        Task<QuotedProduct> Quote(ExecutableProduct executableProduct);

        /// <summary>
        /// This method is responsible for making the batch price of the products according to the parameters entered. 
        /// and delivers a list of session Id with which the selected quote is registered as well as the available quotes of the product
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="executatbleProductLot">This is the object needed to execute the batch actions (quote, confirm, issue)
        /// It has essential information about the product and the necessary parameters.</param>
        Task<QuotedProductLot> Quote(ExecutatbleProductLot executatbleProductLot);

        /// <summary>
        /// Confirms the purchase of the product and allows to deliver a policy to the policy holder who was registered in the quote.
        /// This method will return a list of all the products available in sekure. 
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="executableProduct">This is the object necessary to execute the actions (quote, confirm, emit) in the product purchase process. 
        /// It has essential information about the product, policyholder, and necessary parameters.</param>
        /// <param name="sessionId">This is the session id with which the quote was registered.</param>
        Task<Policy> Confirm(ExecutableProduct executableProduct, Guid sessionId);

        /// <summary>
        /// Confirms the quote of the selected product and allows to deliver a policy to the policyholder who registered in the quote.
        /// This method will return the product confirmation
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="executableProduct">This is the object necessary to execute the actions (quote, confirm, emit) in the product purchase process. 
        /// It has essential information about the product, policyholder, and necessary parameters.</param>
        /// <param name="sessionId">This is the session id with which the quote was registered.</param>
        Task<Policy> Confirm(ExecutatbleProductLot executableProduct, Guid sessionId);

        /// <summary>
        /// Generate the insurance policy in case the product is not paid with the sekure payment gateway, or on the contrary, 
        /// allow the policy to pass to a state of pending payment. In the quote response there is a boolean property which indicates if the product is paid through the sekure payment gateways or not.
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="executableProduct">This is the object necessary to execute the actions (quote, confirm, emit) in the product purchase process. 
        /// It has essential information about the product, policyholder, and necessary parameters.</param>
        /// <param name="sessionId">This is the session id with which the quote was registered.</param>
        Task<string> Emit(ExecutableProduct executableProduct, Guid sessionId);

        /// <summary>
        /// Generates the emission of the selected product of the Lot
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="executableProduct">This is the object necessary to execute the actions (quote, confirm, emit) in the product purchase process. 
        /// It has essential information about the product, policyholder, and necessary parameters.</param>
        /// <param name="sessionId">This is the session id with which the quote was registered.</param>
        Task<string> Emit(ExecutatbleProductLot executableProduct, Guid sessionId);

        /// <summary>
        /// performs the cancellation of the policy purchase flow.
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="sessionId">This is the session id with which the quote was registered.</param>
        Task<string> Cancel(Guid sessionId);

        /// <summary>
        /// Allows to know in what state of the process the policy is
        /// Send a GetAsync request to InsuranceOS API./>.
        /// </summary>
        /// <param name="sessionId">This is the session id with which the quote was registered.</param>
        Task<ProductStage> GetProductStage(Guid sessionId);

        /// <summary>
        /// Return the estimate associated with the session ID 
        /// Send a GetAsync request to the Insurance API
        /// </summary>
        /// <param name="sessionId">This is the session id with which the quote was registered.</param>
        Task<Estimate> GetEstimateBySessionId(Guid sessionId);

        /// <summary>
        /// Allows to obtain the configuration that we must use for the payment gateway that is going to be used
        /// </summary>
        /// /// <param name="paymentGatewayName">Name of the payment gateway</param>
        /// <param name="productName">product name of the payment gateway</param>
        Task<PaymentGatewayProduct> GetPaymentGatewayConfiguration(string paymentGatewayName, string productName);

        /// <summary>
        /// Start a payment session with the chosen gateway returns the url where the payment can be made.
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// /// <param name="paymentDetail">Important payment detail. 
        /// The object type property allows to be configured to any object that is necessary to start a session in any of the gateways that sekure has.</param>
        Task<string> Pay(PaymentDetail paymentDetail);

        /// <summary>
        /// Returns information about the status of the payment of the policy.
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        /// /// <param name="paymentDetail">Important payment detail. 
        /// The object type property allows to be configured to any object that is necessary to get payment status in any of the gateways that sekure has.</param>
        Task<PaymentStatus> GetPaymentStatus(PaymentDetail paymentDetail);

        /// <summary>
        /// Returns information about the any additional information of a product.
        /// Send a PostAsync request to InsuranceOS API./>.
        /// </summary>
        Task<string> AskSekure(object parameters, int productId, string productName);
    }
}