using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class GradeService : ServiceBase<Grade, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Grade";
        public GradeService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Grade
        //*****************************************************************************
        public async Task<IEnumerable<Grade>> GetAllGrades()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllGrades");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Grade>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Grade>> GetPagedGrades(PaginationResult<Grade> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Grade>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedGrades", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Grade>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Grade>()
            {
                Items = Enumerable.Empty<Grade>()

            };

        }
        public async Task<Grade> GetGradeById(int GradeId)
        {
            //            await SetAuthenticationHeader();
            //              Grade? _Grade;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(GradeId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetGradeById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Grade>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Grade> AddGrade(Grade entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Grade>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddGrade", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Grade>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Grade> UpdateGrade(Grade entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Grade>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateGrade", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Grade>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteGrade(Grade entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Grade>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteGrade", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
