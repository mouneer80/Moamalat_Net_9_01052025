using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class WilayatService : ServiceBase<Wilayat, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Wilayat";
        public WilayatService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Wilayat
        //*****************************************************************************
        public async Task<IEnumerable<Wilayat>> GetAllWilayats()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllWilayats");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Wilayat>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Wilayat>> GetPagedWilayats(PaginationResult<Wilayat> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Wilayat>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedWilayats", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Wilayat>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Wilayat>()
            {
                Items = Enumerable.Empty<Wilayat>()

            };

        }
        public async Task<Wilayat> GetWilayatById(int WilayatId)
        {
            //            await SetAuthenticationHeader();
            //              Wilayat? _Wilayat;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(WilayatId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetWilayatById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Wilayat>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Wilayat> AddWilayat(Wilayat entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Wilayat>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddWilayat", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Wilayat>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Wilayat> UpdateWilayat(Wilayat entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Wilayat>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateWilayat", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Wilayat>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteWilayat(Wilayat entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Wilayat>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteWilayat", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
