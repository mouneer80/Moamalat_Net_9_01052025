using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class RequestStatuService : ServiceBase<RequestStatu, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="RequestStatu";
        public RequestStatuService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  RequestStatu
        //*****************************************************************************
        public async Task<IEnumerable<RequestStatu>> GetAllRequestStatus()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllRequestStatus");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<RequestStatu>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<RequestStatu>> GetPagedRequestStatus(PaginationResult<RequestStatu> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<RequestStatu>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedRequestStatus", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<RequestStatu>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<RequestStatu>()
            {
                Items = Enumerable.Empty<RequestStatu>()

            };

        }
        public async Task<RequestStatu> GetRequestStatuById(int StatusId)
        {
            //            await SetAuthenticationHeader();
            //              RequestStatu? _RequestStatu;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(StatusId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetRequestStatuById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestStatu>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<RequestStatu> AddRequestStatu(RequestStatu entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestStatu>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddRequestStatu", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestStatu>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<RequestStatu> UpdateRequestStatu(RequestStatu entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestStatu>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateRequestStatu", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestStatu>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteRequestStatu(RequestStatu entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestStatu>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteRequestStatu", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
