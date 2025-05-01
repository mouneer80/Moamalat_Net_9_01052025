using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class ServiceParameterService : ServiceBase<ServiceParameter, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="ServiceParameter";
        public ServiceParameterService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  ServiceParameter
        //*****************************************************************************
        public async Task<IEnumerable<ServiceParameter>> GetAllServiceParameters()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllServiceParameters");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<ServiceParameter>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<ServiceParameter>> GetPagedServiceParameters(PaginationResult<ServiceParameter> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<ServiceParameter>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedServiceParameters", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<ServiceParameter>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<ServiceParameter>()
            {
                Items = Enumerable.Empty<ServiceParameter>()

            };

        }
        public async Task<ServiceParameter> GetServiceParameterById(int ServiceParameterId)
        {
            //            await SetAuthenticationHeader();
            //              ServiceParameter? _ServiceParameter;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(ServiceParameterId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetServiceParameterById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceParameter>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<ServiceParameter> AddServiceParameter(ServiceParameter entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceParameter>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddServiceParameter", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceParameter>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<ServiceParameter> UpdateServiceParameter(ServiceParameter entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceParameter>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateServiceParameter", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceParameter>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteServiceParameter(ServiceParameter entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceParameter>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteServiceParameter", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
