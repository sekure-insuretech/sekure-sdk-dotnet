using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sekure.Models;
using Sekure.Models.Endorsement;
using Sekure.Models.RiskValidator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sekure.Runtime
{
    public class InsuranceOS : IInsuranceOS
    {
        private string apiUrl = string.Empty;
        private string apiKey = string.Empty;
        private string clientIpAddress = string.Empty;
        private HttpClient _client;

        public InsuranceOS(string apiUrl, string apiKey, HttpClient client)
        {
            this.apiUrl = apiUrl;
            this.apiKey = apiKey;
            _client = client;
        }

        public InsuranceOS(string apiUrl, string apiKey, string clientIpAddress, HttpClient client)
        {
            this.apiUrl = apiUrl;
            this.apiKey = apiKey;
            this.clientIpAddress = clientIpAddress;
            _client = client;
        }

        private HttpClient GetClient()
        {
            _client.DefaultRequestHeaders.Add("skr-key", apiKey);
            _client.DefaultRequestHeaders.Add("client-ip-address", clientIpAddress);
            _client.Timeout = TimeSpan.FromMinutes(5);
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

        public async Task<QuotedProduct> UpdateQuote(ExecutableProduct executableProduct, Guid sessionId)
        {
            string jsonProduct = JsonConvert.SerializeObject(executableProduct, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Products/UpdateQuote/{sessionId}", new StringContent(jsonProduct, Encoding.UTF8, "application/json"));

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

        public async Task<Policy> GetProductDetailsBySessionId(Guid sessionId)
        {
            var response = await GetClient().GetAsync($"{apiUrl}/Product/Details/{sessionId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string policyJson = await response.Content.ReadAsStringAsync();
            Policy policy = JsonConvert.DeserializeObject<Policy>(policyJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return policy;
        }

        public async Task<IEnumerable<PresubscribedByIds>> GetProductsByTenant()
        {
            var response = await GetClient().GetAsync($"{apiUrl}/Products/ByTenant");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responseJson = await response.Content.ReadAsStringAsync();
            IEnumerable<PresubscribedByIds> presubscribeds = JsonConvert.DeserializeObject<IEnumerable<PresubscribedByIds>>(responseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return presubscribeds;
        }

        public async Task<CalculationInfoWithPaginator> GetCalculationInfoById(
            IEnumerable<int?> calculationInfoTypeIds
            , string searchterm
            , int presubscribedId
            , int? pageNumber
            , int? pageSize
        )
        {
            StringBuilder queryParams = new StringBuilder();
            string CALCULATION_INFO_TYPE_IDS = "calculationInfoTypeIds";
            string PAGE_NUMBER = "pageNumber";
            string PAGE_SIZE = "pageSize";
            string SEARCH_TERM = "searchterm";

            if (calculationInfoTypeIds.Any())
                queryParams.Append($"{CALCULATION_INFO_TYPE_IDS}={string.Join(",", calculationInfoTypeIds)}");

            if (pageNumber != default(int))
                queryParams.Append($"&{PAGE_NUMBER}={pageNumber}");

            if (pageSize != default(int))
                queryParams.Append($"&{PAGE_SIZE}={pageSize}");

            if (!string.IsNullOrEmpty(searchterm))
                queryParams.Append($"&{SEARCH_TERM}={searchterm}");

            var response = await GetClient().GetAsync($"{apiUrl}/CalculationInfo/ListByPresubscribedId/{presubscribedId}?{queryParams}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responseJson = await response.Content.ReadAsStringAsync();
            CalculationInfoWithPaginator calculationsInfo = JsonConvert.DeserializeObject<CalculationInfoWithPaginator>(responseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return calculationsInfo;
        }

        public async Task<IdUpdatedResponse> UpdateCalculationInfo(CalculationInfoUpdate calculationInfoUpdate)
        {
            string jsonCalculationInfo = JsonConvert.SerializeObject(calculationInfoUpdate);

            HttpResponseMessage response = await GetClient().PutAsync($"{apiUrl}/CalculationInfo/Update", new StringContent(jsonCalculationInfo, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string result = await response.Content.ReadAsStringAsync();

            IdUpdatedResponse idUpdatedResponse = JsonConvert.DeserializeObject<IdUpdatedResponse>(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return idUpdatedResponse;
        }

        public async Task<IdCreateResponse> CreateCalculationInfo(CalculationInfo calculationInfo)
        {
            string jsonCalculationInfo = JsonConvert.SerializeObject(calculationInfo);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/CalculationInfo/Add", new StringContent(jsonCalculationInfo, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string result = await response.Content.ReadAsStringAsync();

            IdCreateResponse idCreateResponse = JsonConvert.DeserializeObject<IdCreateResponse>(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return idCreateResponse;
        }

        public async Task<IdDeletedResponse> DeleteCalculationInfo(int presubscribedId, int calculationInfoId)
        {
            HttpResponseMessage response = await GetClient().DeleteAsync($"{apiUrl}/CalculationInfo/delete/presubscribedId={presubscribedId}&calculationInfoId={calculationInfoId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string result = await response.Content.ReadAsStringAsync();

            IdDeletedResponse idDeletedResponse = JsonConvert.DeserializeObject<IdDeletedResponse>(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return idDeletedResponse;
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

        public async Task<string> SessionToken(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/SessionToken", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync();

            return responsePayment;
        }

        public async Task<string> ReverseCapture(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/ReverseCapture", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync();

            return responsePayment;
        }

        public async Task<PaymentStatus> GetTokenizationStatus(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);

            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/Payment/TokenizationStatus", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string statusJson = await response.Content.ReadAsStringAsync();
            PaymentStatus paymentStatus = JsonConvert.DeserializeObject<PaymentStatus>(statusJson);

            return paymentStatus;
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

        public async Task<string> ConfirmPayment(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);
            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/ConfirmPayment", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync(); return responsePayment;
        }

        public async Task<string> ConfirmPaymentV1(HttpRequest req)
        {
            string jsonReq = JsonConvert.SerializeObject(req);
            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/ConfirmPaymentV1", new StringContent(jsonReq, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync(); return responsePayment;
        }

        public async Task<string> UpdateSessionDetail(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);
            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/UpdateSessionDetail", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync();
            return responsePayment;
        }

        public async Task<string> SessionByNroElctronico(PaymentDetail paymentDetail)
        {
            string jsonPaymentDetail = JsonConvert.SerializeObject(paymentDetail);
            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/SessionByNroElctronico", new StringContent(jsonPaymentDetail, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync();
            return responsePayment;
        }

        public async Task<string> GetSessionByTransactionSkrId(string transactionSkrId)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/GetSessionByTransactionSkrId/{transactionSkrId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string responsePayment = await response.Content.ReadAsStringAsync();
            return responsePayment;
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

        #region RiskValidation

        public async Task<ExecutableRiskValidator> RikValidator(RequestExecutable requestExecutable, Guid sessionId)
        {
            string jsonRequestExecutable = JsonConvert.SerializeObject(requestExecutable);
            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/risk/validator/{sessionId}", new StringContent(jsonRequestExecutable, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }
            string responseJson = await response.Content.ReadAsStringAsync();
            ExecutableRiskValidator executableRiskValidator = JsonConvert.DeserializeObject<ExecutableRiskValidator>(responseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            return executableRiskValidator;
        }

        public async Task<ResponseConfiguration> GetValidatorConfiguration(Guid sessionId)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/risk/getconfiguration/{sessionId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }

            string configurationJson = await response.Content.ReadAsStringAsync();

            ResponseConfiguration Configuration = JsonConvert.DeserializeObject<ResponseConfiguration>(configurationJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            return Configuration;
        }

        public async Task<ValidationProcess> ValidateStatus(RequestExecutable requestExecutable, Guid sessionId)
        {
            string jsonRequestExecutable = JsonConvert.SerializeObject(requestExecutable);
            HttpResponseMessage response = await GetClient().PostAsync($"{apiUrl}/SKR/ValidateStatus/{sessionId}", new StringContent(jsonRequestExecutable, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }
            string responseJson = await response.Content.ReadAsStringAsync();
            ValidationProcess executableRiskValidator = JsonConvert.DeserializeObject<ValidationProcess>(responseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            return executableRiskValidator;
        }

        public async Task<Tuple<bool, List<ValidationProcess>>> GetValidationProcessByTransactionSkrId(RequestExecutable requestExecutable, Guid sessionId)
        {
            string jsonRequestExecutable = JsonConvert.SerializeObject(requestExecutable);
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/SKR/GetValidationProcessByTransactionSkrId/{sessionId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }
            string responseJson = await response.Content.ReadAsStringAsync();
            Tuple<bool, List<ValidationProcess>> executableRiskValidator = JsonConvert.DeserializeObject<Tuple<bool, List<ValidationProcess>>>(responseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            return executableRiskValidator;
        }

        public async Task<ValidationProcess> RiskStatusByProduct(string productName, Guid sessionId)
        {
            HttpResponseMessage response = await GetClient().GetAsync($"{apiUrl}/SKR/RiskStatusByProduct/{productName}/{sessionId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"statusCode: {response.StatusCode}, messageException: {response.Content.ReadAsStringAsync().Result}");
            }
            string responseJson = await response.Content.ReadAsStringAsync();
            ValidationProcess executableRiskValidator = JsonConvert.DeserializeObject<ValidationProcess>(responseJson, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            return executableRiskValidator;
        }
        #endregion

        private static readonly JsonSerializerSettings EndorsementJsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        #region Endosos - listado y detalle

        public Task<EndorsementSearchResponse> SearchEndorsements(EndorsementSearchRequest request, string customerEmail)
            => SendEndorsementJson<EndorsementSearchResponse>(HttpMethod.Post, "Endorsement/search", request, customerEmail, null);

        public Task<EndorsementDetailResponse> GetEndorsementDetail(Guid endorsementId, string customerEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Get, $"Endorsement/{endorsementId}/detail", null, customerEmail, null);

        public Task<EndorsementResponse> GetEndorsementById(Guid endorsementId, string customerEmail)
            => SendEndorsementJson<EndorsementResponse>(HttpMethod.Get, $"Endorsement/{endorsementId}", null, customerEmail, null);

        public Task<EndorsementPagedResponse> GetEndorsements(int skip, int take, string customerEmail)
            => SendEndorsementJson<EndorsementPagedResponse>(HttpMethod.Get, $"Endorsement?skip={skip}&take={take}", null, customerEmail, null);

        #endregion

        #region Endosos - registro y edición

        public Task<EndorsementWriteResponse> CreateEndorsement(EndorsementUpsertRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementWriteResponse>(HttpMethod.Post, "Endorsement", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementWriteResponse> UpdateEndorsement(Guid endorsementId, EndorsementUpsertRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementWriteResponse>(HttpMethod.Put, $"Endorsement/{endorsementId}", request, customerEmail, RequireUser(userEmail));

        public Task DeleteEndorsement(Guid endorsementId, string customerEmail, string userEmail)
            => SendEndorsementNoContent(HttpMethod.Delete, $"Endorsement/{endorsementId}", customerEmail, RequireUser(userEmail));

        #endregion

        #region Endosos - acciones de gestión

        public Task<EndorsementDetailResponse> RenewEndorsement(Guid endorsementId, RenewEndorsementRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/renew", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementDetailResponse> RejectEndorsementPerson(Guid endorsementId, RejectPersonRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/reject", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementDetailResponse> MarkClientManagement(Guid endorsementId, ClientManagementRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/client-management", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementDetailResponse> MarkEndorsementInProcess(Guid endorsementId, InProcessRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/in-process", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementDetailResponse> ReassignEndorsement(Guid endorsementId, ReassignRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/reassign", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementDetailResponse> AddEndorsementNote(Guid endorsementId, NoteRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/notes", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementDetailResponse> SendEndorsementReminder(Guid endorsementId, ReminderRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/reminders", request, customerEmail, RequireUser(userEmail));

        public Task<EndorsementDetailResponse> RegeneratePortalLink(Guid endorsementId, ReminderRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/portal-link/regenerate", request, customerEmail, RequireUser(userEmail));

        #endregion

        #region Endosos - documentos

        public async Task<UploadDocumentResponse> UploadEndorsementDocument(Guid endorsementId, Stream file, string fileName, string contentType, byte personRole, byte documentType, string customerEmail, string userEmail)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            using (var form = new MultipartFormDataContent())
            {
                var filePart = new StreamContent(file);
                filePart.Headers.ContentType = new MediaTypeHeaderValue(string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType);
                form.Add(filePart, "Files[0]", fileName);
                form.Add(new StringContent(personRole.ToString()), "personRole");
                form.Add(new StringContent(documentType.ToString()), "documentType");

                using (var message = BuildEndorsementMessage(HttpMethod.Post, $"Endorsement/{endorsementId}/documents", customerEmail, RequireUser(userEmail)))
                {
                    message.Content = form;
                    return await SendAndRead<UploadDocumentResponse>(message);
                }
            }
        }

        public Task<List<DocumentListItemResponse>> GetEndorsementDocuments(Guid endorsementId, string customerEmail)
            => SendEndorsementJson<List<DocumentListItemResponse>>(HttpMethod.Get, $"Endorsement/{endorsementId}/documents", null, customerEmail, null);

        public Task<SekureFile> DownloadEndorsementDocument(Guid publicId, string customerEmail)
            => SendEndorsementFile(HttpMethod.Get, $"Endorsement/documents/{publicId}", null, customerEmail, null);

        public Task DeleteEndorsementDocument(Guid publicId, string customerEmail, string userEmail)
            => SendEndorsementNoContent(HttpMethod.Delete, $"Endorsement/documents/{publicId}", customerEmail, RequireUser(userEmail));

        #endregion

        #region Endosos - comunicación de inclusión en la colectiva

        public Task<CollectiveNoticePreview> GetCollectiveNoticePreview(Guid endorsementId, byte role, string customerEmail)
            => SendEndorsementJson<CollectiveNoticePreview>(HttpMethod.Get, $"Endorsement/{endorsementId}/collective-notice?role={role}", null, customerEmail, null);

        public Task<EndorsementDetailResponse> SendCollectiveNotice(Guid endorsementId, CollectiveNoticeRequest request, string customerEmail, string userEmail)
            => SendEndorsementJson<EndorsementDetailResponse>(HttpMethod.Post, $"Endorsement/{endorsementId}/collective-notice", request, customerEmail, RequireUser(userEmail));

        #endregion

        #region Endosos - importación y exportación

        public Task<SekureFile> ExportEndorsements(EndorsementExportRequest request, string customerEmail, string userEmail)
            => SendEndorsementFile(HttpMethod.Post, "Endorsement/export", request, customerEmail, RequireUser(userEmail));

        public async Task<StartImportResponse> StartEndorsementImport(Stream file, string fileName, byte productType, byte endorsementType, string customerEmail, string userEmail)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            using (var form = new MultipartFormDataContent())
            {
                var filePart = new StreamContent(file);
                filePart.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                form.Add(filePart, "file", fileName);
                form.Add(new StringContent(productType.ToString()), "productType");
                form.Add(new StringContent(endorsementType.ToString()), "endorsementType");

                using (var message = BuildEndorsementMessage(HttpMethod.Post, "Endorsement/import", customerEmail, RequireUser(userEmail)))
                {
                    message.Content = form;
                    return await SendAndRead<StartImportResponse>(message);
                }
            }
        }

        public Task<EndorsementImportJobResponse> GetEndorsementImportJob(Guid jobId, string customerEmail)
            => SendEndorsementJson<EndorsementImportJobResponse>(HttpMethod.Get, $"Endorsement/import/{jobId}", null, customerEmail, null);

        public Task<SekureFile> GetEndorsementImportTemplate(byte endorsementType)
            => SendEndorsementFile(HttpMethod.Get, $"Endorsement/import/template/{endorsementType}", null, null, null);

        public Task<SekureFile> DownloadEndorsementImportReport(Guid jobId, string customerEmail)
            => SendEndorsementFile(HttpMethod.Get, $"Endorsement/import/{jobId}/report", null, customerEmail, null);

        #endregion

        #region Endosos - reportes

        public Task<EndorsementReportResponse> GetEndorsementReport(EndorsementReportRequest request, string customerEmail)
            => SendEndorsementJson<EndorsementReportResponse>(HttpMethod.Post, "Endorsement/reports", request, customerEmail, null);

        public Task<SekureFile> ExportEndorsementReport(EndorsementReportRequest request, string customerEmail, string userEmail)
            => SendEndorsementFile(HttpMethod.Post, "Endorsement/reports/export", request, customerEmail, RequireUser(userEmail));

        #endregion

        #region Endosos - resumen PDF

        public Task<SekureFile> GetEndorsementSummaryPdf(Guid endorsementId, string customerEmail, string userEmail = null)
            => SendEndorsementFile(HttpMethod.Get, $"Endorsement/{endorsementId}/summary-pdf", null, customerEmail, userEmail);

        #endregion

        #region Endosos - helpers

        private static string RequireUser(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                throw new ArgumentException("user-email es obligatorio para esta operación.", nameof(userEmail));
            return userEmail;
        }

        private HttpRequestMessage BuildEndorsementMessage(HttpMethod method, string path, string customerEmail, string userEmail)
        {
            var message = new HttpRequestMessage(method, $"{apiUrl.TrimEnd('/')}/{path}");

            // Si GetClient() ya agregó estos headers al cliente compartido, no se repiten.
            if (!_client.DefaultRequestHeaders.Contains("skr-key"))
                message.Headers.TryAddWithoutValidation("skr-key", apiKey);
            if (!_client.DefaultRequestHeaders.Contains("client-ip-address") && !string.IsNullOrEmpty(clientIpAddress))
                message.Headers.TryAddWithoutValidation("client-ip-address", clientIpAddress);

            if (!string.IsNullOrWhiteSpace(customerEmail))
                message.Headers.TryAddWithoutValidation("customer-email", customerEmail);
            if (!string.IsNullOrWhiteSpace(userEmail))
                message.Headers.TryAddWithoutValidation("user-email", userEmail);

            return message;
        }

        private async Task<T> SendEndorsementJson<T>(HttpMethod method, string path, object body, string customerEmail, string userEmail)
        {
            using (var message = BuildEndorsementMessage(method, path, customerEmail, userEmail))
            {
                if (body != null)
                    message.Content = new StringContent(JsonConvert.SerializeObject(body, EndorsementJsonSettings), Encoding.UTF8, "application/json");
                return await SendAndRead<T>(message);
            }
        }

        private async Task SendEndorsementNoContent(HttpMethod method, string path, string customerEmail, string userEmail)
        {
            using (var message = BuildEndorsementMessage(method, path, customerEmail, userEmail))
            using (var response = await _client.SendAsync(message))
            {
                await EnsureEndorsementSuccess(response);
            }
        }

        private async Task<SekureFile> SendEndorsementFile(HttpMethod method, string path, object body, string customerEmail, string userEmail)
        {
            using (var message = BuildEndorsementMessage(method, path, customerEmail, userEmail))
            {
                if (body != null)
                    message.Content = new StringContent(JsonConvert.SerializeObject(body, EndorsementJsonSettings), Encoding.UTF8, "application/json");

                using (var response = await _client.SendAsync(message))
                {
                    await EnsureEndorsementSuccess(response);
                    var headers = response.Content.Headers;
                    return new SekureFile
                    {
                        Content = await response.Content.ReadAsByteArrayAsync(),
                        ContentType = headers.ContentType?.MediaType ?? "application/octet-stream",
                        FileName = headers.ContentDisposition?.FileNameStar ?? headers.ContentDisposition?.FileName?.Trim('"')
                    };
                }
            }
        }

        private async Task<T> SendAndRead<T>(HttpRequestMessage message)
        {
            using (var response = await _client.SendAsync(message))
            {
                await EnsureEndorsementSuccess(response);
                var json = await response.Content.ReadAsStringAsync();
                return string.IsNullOrWhiteSpace(json) ? default : JsonConvert.DeserializeObject<T>(json, EndorsementJsonSettings);
            }
        }

        private static async Task EnsureEndorsementSuccess(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) return;
            var body = response.Content == null ? null : await response.Content.ReadAsStringAsync();
            throw new SekureApiException((int)response.StatusCode, body);
        }

        #endregion
    }
}