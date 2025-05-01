using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class RegionService : ServiceBase<Region, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Region";
        public RegionService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Region
        //*****************************************************************************
        public async Task<IEnumerable<Region>> GetAllRegions()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllRegions");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Region>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Region>> GetPagedRegions(PaginationResult<Region> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Region>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedRegions", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Region>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Region>()
            {
                Items = Enumerable.Empty<Region>()

            };

        }
        public async Task<Region> GetRegionById(int RegionId)
        {
            //            await SetAuthenticationHeader();
            //              Region? _Region;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(RegionId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetRegionById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Region>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Region> AddRegion(Region entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Region>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddRegion", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Region>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Region> UpdateRegion(Region entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Region>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateRegion", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Region>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteRegion(Region entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Region>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteRegion", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
