using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class ParameterService : ServiceBase<Parameter, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Parameter";
        public ParameterService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Parameter
        //*****************************************************************************
        public async Task<IEnumerable<Parameter>> GetAllParameters()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllParameters");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Parameter>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Parameter>> GetPagedParameters(PaginationResult<Parameter> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Parameter>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedParameters", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Parameter>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Parameter>()
            {
                Items = Enumerable.Empty<Parameter>()

            };

        }
        public async Task<Parameter> GetParameterById(int ParameterId)
        {
            //            await SetAuthenticationHeader();
            //              Parameter? _Parameter;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(ParameterId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetParameterById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Parameter>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Parameter> AddParameter(Parameter entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Parameter>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddParameter", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Parameter>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Parameter> UpdateParameter(Parameter entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Parameter>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateParameter", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Parameter>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteParameter(Parameter entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Parameter>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteParameter", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
