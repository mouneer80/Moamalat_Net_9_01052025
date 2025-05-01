using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class DocumentTypeService : ServiceBase<DocumentType, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="DocumentType";
        public DocumentTypeService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  DocumentType
        //*****************************************************************************
        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypes()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllDocumentTypes");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<DocumentType>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<DocumentType>> GetPagedDocumentTypes(PaginationResult<DocumentType> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<DocumentType>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedDocumentTypes", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<DocumentType>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<DocumentType>()
            {
                Items = Enumerable.Empty<DocumentType>()

            };

        }
        public async Task<DocumentType> GetDocumentTypeById(int DocumentTypeId)
        {
            //            await SetAuthenticationHeader();
            //              DocumentType? _DocumentType;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(DocumentTypeId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetDocumentTypeById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<DocumentType>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<DocumentType> AddDocumentType(DocumentType entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<DocumentType>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddDocumentType", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<DocumentType>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<DocumentType> UpdateDocumentType(DocumentType entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<DocumentType>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateDocumentType", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<DocumentType>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteDocumentType(DocumentType entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<DocumentType>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteDocumentType", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
