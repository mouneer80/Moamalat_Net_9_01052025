using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class TopUpHistoryService : ServiceBase<TopUpHistory, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="TopUpHistory";
        public TopUpHistoryService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  TopUpHistory
        //*****************************************************************************
        public async Task<IEnumerable<TopUpHistory>> GetAllTopUpHistories()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllTopUpHistories");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<TopUpHistory>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<TopUpHistory>> GetPagedTopUpHistories(PaginationResult<TopUpHistory> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<TopUpHistory>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedTopUpHistories", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<TopUpHistory>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<TopUpHistory>()
            {
                Items = Enumerable.Empty<TopUpHistory>()

            };

        }
        public async Task<TopUpHistory> GetTopUpHistoryById(int TopupId)
        {
            //            await SetAuthenticationHeader();
            //              TopUpHistory? _TopUpHistory;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(TopupId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetTopUpHistoryById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<TopUpHistory>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<TopUpHistory> AddTopUpHistory(TopUpHistory entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<TopUpHistory>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddTopUpHistory", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<TopUpHistory>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<TopUpHistory> UpdateTopUpHistory(TopUpHistory entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<TopUpHistory>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateTopUpHistory", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<TopUpHistory>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteTopUpHistory(TopUpHistory entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<TopUpHistory>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteTopUpHistory", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
