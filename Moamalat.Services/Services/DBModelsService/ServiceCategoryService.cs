using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class ServiceCategoryService : ServiceBase<ServiceCategory, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="ServiceCategory";
        public ServiceCategoryService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  ServiceCategory
        //*****************************************************************************
        public async Task<IEnumerable<ServiceCategory>> GetAllServiceCategories()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllServiceCategories");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<ServiceCategory>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<ServiceCategory>> GetPagedServiceCategories(PaginationResult<ServiceCategory> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<ServiceCategory>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedServiceCategories", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<ServiceCategory>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<ServiceCategory>()
            {
                Items = Enumerable.Empty<ServiceCategory>()

            };

        }
        public async Task<ServiceCategory> GetServiceCategoryById(int ServiceCategoryId)
        {
            //            await SetAuthenticationHeader();
            //              ServiceCategory? _ServiceCategory;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(ServiceCategoryId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetServiceCategoryById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceCategory>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<ServiceCategory> AddServiceCategory(ServiceCategory entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceCategory>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddServiceCategory", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceCategory>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<ServiceCategory> UpdateServiceCategory(ServiceCategory entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceCategory>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateServiceCategory", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceCategory>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteServiceCategory(ServiceCategory entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceCategory>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteServiceCategory", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

       
        //  End of Service
    }
}
