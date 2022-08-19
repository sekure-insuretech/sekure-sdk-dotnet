using Newtonsoft.Json;
using Sekure.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

            List<ProductReference> products = JsonConvert.DeserializeObject<List<ProductReference>>(productsJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return products;
        }

        [Obsolete("This property is obsolete. Use ProductByIdNoPolicyHolder instead.", false)]
        public async Task<Product> GetProductById(int id)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string productResponseJson = await response.Content.ReadAsStringAsync();

            Product product = JsonConvert.DeserializeObject<Product>(productResponseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return product;
        }

        public async Task<Product> ProductByIdNoPolicyHolder(int id)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/v1/Products/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }
            string productResponseJson = await response.Content.ReadAsStringAsync();

            Product product = JsonConvert.DeserializeObject<Product>(productResponseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

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
            QuotedProduct quotedProduct = JsonConvert.DeserializeObject<QuotedProduct>(quotedProductJson, new JsonSerializerSettings 
            { 
                NullValueHandling = NullValueHandling.Ignore, 
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

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
            Policy policy = JsonConvert.DeserializeObject<Policy>(confirmedProductJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return policy;
        }

        [Obsolete("This property is obsolete. Use EmitWithPolicy instead.", false)]
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

        public async Task<Policy> EmitWithPolicy(ExecutableProduct executableProduct, Guid sessionId)
        {
            string jsonProduct = JsonConvert.SerializeObject(executableProduct);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/v1/Products/Emit/{sessionId}", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Policy policy = JsonConvert.DeserializeObject<Policy>(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return policy;
        }

        public async Task<string> Cancel(Guid sessionId)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{apiUrl}/Products/Cancel/{sessionId}");
            HttpResponseMessage response = await GetClient().SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<ProductStage> GetProductStage(Guid sessionId)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Products/Stage/{sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string productStageJson = await response.Content.ReadAsStringAsync();
            ProductStage productStage = JsonConvert.DeserializeObject<ProductStage>(productStageJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return productStage;
        }

        #endregion

        #region ProductLot
        public async Task<BatchDiscovery> GetProductLotByName(string name)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Products/Batch/{name}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string productResponseJson = await response.Content.ReadAsStringAsync();
            BatchDiscovery lot = JsonConvert.DeserializeObject<BatchDiscovery>(productResponseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return lot;
        }

        public async Task<QuotedProductLot> Quote(ExecutatbleProductLot executatbleProductLot)
        {
            string jsonProduct = JsonConvert.SerializeObject(executatbleProductLot);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/Batch/Quote", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string quotedProductJson = await response.Content.ReadAsStringAsync();
            QuotedProductLot quotedProduct = JsonConvert.DeserializeObject<QuotedProductLot>(quotedProductJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return quotedProduct;
        }

        public async Task<Policy> Confirm(ExecutatbleProductLot executableProduct, Guid sessionId)
        {
            string jsonProduct = JsonConvert.SerializeObject(executableProduct);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/Batch/Confirm/{sessionId}", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string confirmedProductJson = await response.Content.ReadAsStringAsync();
            Policy policy = JsonConvert.DeserializeObject<Policy>(confirmedProductJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return policy;
        }

        public async Task<Policy> EmitWithPolicy(ExecutatbleProductLot executableProduct, Guid sessionId)
        {
            string jsonProduct = JsonConvert.SerializeObject(executableProduct);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/Batch/Emit/{sessionId}", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string result = await response.Content.ReadAsStringAsync();
            Policy policy = JsonConvert.DeserializeObject<Policy>(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return policy;
        }
        #endregion

        #region Estimate
        public async Task<Estimate> GetEstimateBySessionId(Guid sessionId)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Estimates/Session/{sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string estimateBySessionIdJson = await response.Content.ReadAsStringAsync();
            Estimate estimate = JsonConvert.DeserializeObject<Estimate>(estimateBySessionIdJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return estimate;
        }

        public async Task<QuotedProduct> GetQuoteBySessionId(Guid sessionId)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/Quote/Session/{sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string estimateBySessionIdJson = await response.Content.ReadAsStringAsync();
            Estimate estimate = JsonConvert.DeserializeObject<Estimate>(estimateBySessionIdJson);

            QuotedProduct quotedProduct = JsonConvert.DeserializeObject<QuotedProduct>(estimate.Response, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return quotedProduct;
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
            PaymentStatus paymentStatus = JsonConvert.DeserializeObject<PaymentStatus>(statusJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

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
            PaymentGatewayProduct paymentGatewayProduct = JsonConvert.DeserializeObject<PaymentGatewayProduct>(pyamentGatewayProductJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

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