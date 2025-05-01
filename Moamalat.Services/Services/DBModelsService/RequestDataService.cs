using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class RequestDataService : ServiceBase<RequestData, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="RequestData";
        public RequestDataService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  RequestData
        //*****************************************************************************
        public async Task<IEnumerable<RequestData>> GetAllRequestDatas()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllRequestDatas");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<RequestData>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<RequestData>> GetPagedRequestDatas(PaginationResult<RequestData> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<RequestData>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedRequestDatas", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<RequestData>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<RequestData>()
            {
                Items = Enumerable.Empty<RequestData>()

            };

        }
        public async Task<RequestData> GetRequestDataById(int RequestId)
        {
            //            await SetAuthenticationHeader();
            //              RequestData? _RequestData;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(RequestId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetRequestDataById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestData>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<RequestData> AddRequestData(RequestData entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddRequestData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<RequestData> UpdateRequestData(RequestData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateRequestData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteRequestData(RequestData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteRequestData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
