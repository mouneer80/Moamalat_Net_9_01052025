using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class PaymentDataService : ServiceBase<PaymentData, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="PaymentData";
        public PaymentDataService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  PaymentData
        //*****************************************************************************
        public async Task<IEnumerable<PaymentData>> GetAllPaymentDatas()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllPaymentDatas");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<PaymentData>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<PaymentData>> GetPagedPaymentDatas(PaginationResult<PaymentData> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<PaymentData>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedPaymentDatas", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<PaymentData>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<PaymentData>()
            {
                Items = Enumerable.Empty<PaymentData>()

            };

        }
        public async Task<PaymentData> GetPaymentDataById(int PaymentId)
        {
            //            await SetAuthenticationHeader();
            //              PaymentData? _PaymentData;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(PaymentId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPaymentDataById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaymentData>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<PaymentData> AddPaymentData(PaymentData entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaymentData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddPaymentData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaymentData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<PaymentData> UpdatePaymentData(PaymentData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaymentData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdatePaymentData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaymentData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeletePaymentData(PaymentData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaymentData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeletePaymentData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
