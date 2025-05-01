using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class CorporateService : ServiceBase<Corporate, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Corporate";
        public CorporateService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Corporate
        //*****************************************************************************
        public async Task<IEnumerable<Corporate>> GetAllCorporates()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllCorporates");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Corporate>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Corporate>> GetPagedCorporates(PaginationResult<Corporate> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Corporate>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedCorporates", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Corporate>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Corporate>()
            {
                Items = Enumerable.Empty<Corporate>()

            };

        }
        public async Task<Corporate> GetCorporateById(int CorporateId)
        {
            //            await SetAuthenticationHeader();
            //              Corporate? _Corporate;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(CorporateId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetCorporateById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Corporate>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Corporate> AddCorporate(Corporate entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Corporate>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddCorporate", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Corporate>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Corporate> UpdateCorporate(Corporate entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Corporate>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateCorporate", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Corporate>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteCorporate(Corporate entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Corporate>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteCorporate", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
