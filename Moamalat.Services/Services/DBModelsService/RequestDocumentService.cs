using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class RequestDocumentService : ServiceBase<RequestDocument, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="RequestDocument";
        public RequestDocumentService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  RequestDocument
        //*****************************************************************************
        public async Task<IEnumerable<RequestDocument>> GetAllRequestDocuments()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllRequestDocuments");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<RequestDocument>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<RequestDocument>> GetPagedRequestDocuments(PaginationResult<RequestDocument> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<RequestDocument>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedRequestDocuments", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<RequestDocument>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<RequestDocument>()
            {
                Items = Enumerable.Empty<RequestDocument>()

            };

        }
        public async Task<RequestDocument> GetRequestDocumentById(int RequestDocumentId)
        {
            //            await SetAuthenticationHeader();
            //              RequestDocument? _RequestDocument;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(RequestDocumentId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetRequestDocumentById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestDocument>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<RequestDocument> AddRequestDocument(RequestDocument entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestDocument>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddRequestDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestDocument>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<RequestDocument> UpdateRequestDocument(RequestDocument entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestDocument>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateRequestDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<RequestDocument>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteRequestDocument(RequestDocument entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<RequestDocument>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteRequestDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
