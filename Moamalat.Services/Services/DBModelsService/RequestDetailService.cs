using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class RequestDetailService : ServiceBase<RequestDetail, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="RequestDetail";
        public RequestDetailService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  RequestDetail
        //*****************************************************************************
        public async Task<IEnumerable<RequestDetail>> GetAllRequestDetails()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllRequestDetails");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<RequestDetail>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<RequestDetail>> GetPagedRequestDetails(PaginationResult<RequestDetail> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<RequestDetail>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedRequestDetails", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<RequestDetail>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<RequestDetail>()
            {
                Items = Enumerable.Empty<RequestDetail>()

            };

        }
        public async Task<RequestDetail> GetRequestDetailById(int RequestDetailId)
        {
            //            await SetAuthenticationHeader();
            //              RequestDetail? _RequestDetail;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(RequestDetailId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetRequestDetailById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestDetail>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<RequestDetail> AddRequestDetail(RequestDetail entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestDetail>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddRequestDetail", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestDetail>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<RequestDetail> UpdateRequestDetail(RequestDetail entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestDetail>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateRequestDetail", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestDetail>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteRequestDetail(RequestDetail entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestDetail>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteRequestDetail", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
