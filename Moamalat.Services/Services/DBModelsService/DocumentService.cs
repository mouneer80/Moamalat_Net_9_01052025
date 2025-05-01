using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class DocumentService : ServiceBase<Document, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Document";
        public DocumentService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Document
        //*****************************************************************************
        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllDocuments");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Document>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Document>> GetPagedDocuments(PaginationResult<Document> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Document>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedDocuments", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Document>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Document>()
            {
                Items = Enumerable.Empty<Document>()

            };

        }
        public async Task<Document> GetDocumentById(int DocumentId)
        {
            //            await SetAuthenticationHeader();
            //              Document? _Document;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(DocumentId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetDocumentById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Document>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Document> AddDocument(Document entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Document>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Document>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Document> UpdateDocument(Document entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Document>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Document>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteDocument(Document entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Document>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteDocument", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
