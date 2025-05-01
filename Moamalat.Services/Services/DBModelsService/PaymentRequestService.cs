using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class PaymentRequestService : ServiceBase<PaymentRequest, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="PaymentRequest";
        public PaymentRequestService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  PaymentRequest
        //*****************************************************************************
        public async Task<IEnumerable<PaymentRequest>> GetAllPaymentRequests()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllPaymentRequests");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<PaymentRequest>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<PaymentRequest>> GetPagedPaymentRequests(PaginationResult<PaymentRequest> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<PaymentRequest>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedPaymentRequests", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<PaymentRequest>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<PaymentRequest>()
            {
                Items = Enumerable.Empty<PaymentRequest>()

            };

        }
        public async Task<PaymentRequest> GetPaymentRequestById(int PaymentRequestId)
        {
            //            await SetAuthenticationHeader();
            //              PaymentRequest? _PaymentRequest;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(PaymentRequestId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPaymentRequestById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaymentRequest>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<PaymentRequest> AddPaymentRequest(PaymentRequest entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaymentRequest>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddPaymentRequest", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaymentRequest>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<PaymentRequest> UpdatePaymentRequest(PaymentRequest entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaymentRequest>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdatePaymentRequest", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaymentRequest>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeletePaymentRequest(PaymentRequest entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaymentRequest>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeletePaymentRequest", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<PaymentRequest> InitiatePaymentRequest(PaymentRequest entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaymentRequest>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/InitiatePaymentRequest", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaymentRequest>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<PGResponse> CheckPaymentRequest(int RequestId)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<int>(RequestId)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/CheckPaymentRequest", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PGResponse>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        //  End of Service
    }
}
