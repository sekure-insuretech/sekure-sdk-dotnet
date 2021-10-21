using Sekure.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Sekure.Runtime
{
    public class InsuranceOS : IInsuranceOS
    {
        private string apiUrl = string.Empty;
        private string apiKey = string.Empty;
        private HttpClient _client;

        public InsuranceOS(string apiUrl, string apiKey, HttpClient client)
        {
            this.apiUrl = apiUrl;
            this.apiKey = apiKey;
            _client = client;
        }

        private HttpClient GetClient()
        {
            _client.DefaultRequestHeaders.Add("skr-key", apiKey);
            return _client;
        }

        #region Product

        public async Task<List<ProductReference>> GetProducts()
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Products");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string productsJson = await response.Content.ReadAsStringAsync();

            List<ProductReference> products = JsonConvert.DeserializeObject<List<ProductReference>>(productsJson);

            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string productResponseJson = await response.Content.ReadAsStringAsync();
            Product product = JsonConvert.DeserializeObject<Product>(productResponseJson);

            return product;
        }

        public async Task<QuotedProduct> Quote(ExecutableProduct executableProduct)
        {
            string jsonProduct = JsonConvert.SerializeObject(executableProduct);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/Quote", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string quotedProductJson = await response.Content.ReadAsStringAsync();
            QuotedProduct quotedProduct = JsonConvert.DeserializeObject<QuotedProduct>(quotedProductJson);

            return quotedProduct;
        }

        public async Task<Policy> Confirm(ExecutableProduct executableProduct, Guid sessionId)
        {
            string jsonProduct = JsonConvert.SerializeObject(executableProduct);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/Confirm/{sessionId}", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string confirmedProductJson = await response.Content.ReadAsStringAsync();
            Policy policy = JsonConvert.DeserializeObject<Policy>(confirmedProductJson);

            return policy;
        }

        public async Task<string> Emit(ExecutableProduct executableProduct, Guid sessionId)
        {
            string jsonProduct = JsonConvert.SerializeObject(executableProduct);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/Emit/{sessionId}", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string stage = await response.Content.ReadAsStringAsync();

            return stage;
        }

        public async Task<ProductStage> GetProductStage(Guid sessionId)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Products/Stage/{sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string productStageJson = await response.Content.ReadAsStringAsync();
            ProductStage productStage = JsonConvert.DeserializeObject<ProductStage>(productStageJson);

            return productStage;
        }

        #endregion

        #region Payment

        public async Task<string> Pay(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Payment", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync();

            return responsePayment;
        }

        public async Task<PaymentStatus> GetPaymentStatus(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Payment/Status", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string statusJson = await response.Content.ReadAsStringAsync();
            PaymentStatus paymentStatus = JsonConvert.DeserializeObject<PaymentStatus>(statusJson);

            return paymentStatus;
        }

        public async Task<PaymentGatewayProduct> GetPaymentGatewayConfiguration(string paymentGatewayName, string productName)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Payment/Configuration/paymentGatewayName={paymentGatewayName}&productName={productName}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string pyamentGatewayProductJson = await response.Content.ReadAsStringAsync();
            PaymentGatewayProduct paymentGatewayProduct = JsonConvert.DeserializeObject<PaymentGatewayProduct>(pyamentGatewayProductJson);

            return paymentGatewayProduct;
        }

        #endregion

        #region AskSekure
        public async Task<string> AskSekure(object parameters, int productId, string productName)
        {
            string jsonParameters = JsonConvert.SerializeObject(parameters);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/AskSekure/productId={productId}&productName={productName}", new StringContent(jsonParameters, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync();

            return responsePayment;
        }
        #endregion
    }
}