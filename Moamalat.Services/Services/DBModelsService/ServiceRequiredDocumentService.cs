using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class ServiceRequiredDocumentService : ServiceBase<ServiceRequiredDocument, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="ServiceRequiredDocument";
        public ServiceRequiredDocumentService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  ServiceRequiredDocument
        //*****************************************************************************
        public async Task<IEnumerable<ServiceRequiredDocument>> GetAllServiceRequiredDocuments()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllServiceRequiredDocuments");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<ServiceRequiredDocument>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<ServiceRequiredDocument>> GetPagedServiceRequiredDocuments(PaginationResult<ServiceRequiredDocument> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<ServiceRequiredDocument>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedServiceRequiredDocuments", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<ServiceRequiredDocument>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<ServiceRequiredDocument>()
            {
                Items = Enumerable.Empty<ServiceRequiredDocument>()

            };

        }
        public async Task<ServiceRequiredDocument> GetServiceRequiredDocumentById(int RequiredDocumentId)
        {
            //            await SetAuthenticationHeader();
            //              ServiceRequiredDocument? _ServiceRequiredDocument;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(RequiredDocumentId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetServiceRequiredDocumentById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceRequiredDocument>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<ServiceRequiredDocument> AddServiceRequiredDocument(ServiceRequiredDocument entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceRequiredDocument>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddServiceRequiredDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceRequiredDocument>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<ServiceRequiredDocument> UpdateServiceRequiredDocument(ServiceRequiredDocument entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceRequiredDocument>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateServiceRequiredDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceRequiredDocument>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteServiceRequiredDocument(ServiceRequiredDocument entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceRequiredDocument>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteServiceRequiredDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
