using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class VwRequestDataService : ServiceBase<VwRequestData, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="VwRequestData";
        public VwRequestDataService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  VwRequestData
        //*****************************************************************************
        public async Task<IEnumerable<VwRequestData>> GetAllVwRequestDatas()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllVwRequestDatas");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<VwRequestData>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<VwRequestData>> GetPagedVwRequestDatas(PaginationResult<VwRequestData> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<VwRequestData>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedVwRequestDatas", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<VwRequestData>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<VwRequestData>()
            {
                Items = Enumerable.Empty<VwRequestData>()

            };

        }
        public async Task<VwRequestData> GetVwRequestDataById(int RequestId)
        {
            //            await SetAuthenticationHeader();
            //              VwRequestData? _VwRequestData;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(RequestId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetVwRequestDataById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<VwRequestData>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<VwRequestData> AddVwRequestData(VwRequestData entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<VwRequestData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddVwRequestData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<VwRequestData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<VwRequestData> UpdateVwRequestData(VwRequestData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<VwRequestData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateVwRequestData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<VwRequestData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteVwRequestData(VwRequestData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<VwRequestData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteVwRequestData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
