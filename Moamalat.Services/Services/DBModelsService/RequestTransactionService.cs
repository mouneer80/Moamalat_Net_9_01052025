using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class RequestTransactionService : ServiceBase<RequestTransaction, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="RequestTransaction";
        public RequestTransactionService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  RequestTransaction
        //*****************************************************************************
        public async Task<IEnumerable<RequestTransaction>> GetAllRequestTransactions()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllRequestTransactions");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<RequestTransaction>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<RequestTransaction>> GetPagedRequestTransactions(PaginationResult<RequestTransaction> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<RequestTransaction>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedRequestTransactions", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<RequestTransaction>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<RequestTransaction>()
            {
                Items = Enumerable.Empty<RequestTransaction>()

            };

        }
        public async Task<RequestTransaction> GetRequestTransactionById(int TransactionId)
        {
            //            await SetAuthenticationHeader();
            //              RequestTransaction? _RequestTransaction;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(TransactionId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetRequestTransactionById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestTransaction>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<RequestTransaction> AddRequestTransaction(RequestTransaction entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestTransaction>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddRequestTransaction", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestTransaction>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<RequestTransaction> UpdateRequestTransaction(RequestTransaction entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestTransaction>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateRequestTransaction", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestTransaction>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteRequestTransaction(RequestTransaction entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestTransaction>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteRequestTransaction", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
