using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class PersonCorporateService : ServiceBase<PersonCorporate, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="PersonCorporate";
        public PersonCorporateService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  PersonCorporate
        //*****************************************************************************
        public async Task<IEnumerable<PersonCorporate>> GetAllPersonCorporates()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllPersonCorporates");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<PersonCorporate>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<PersonCorporate>> GetPagedPersonCorporates(PaginationResult<PersonCorporate> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<PersonCorporate>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedPersonCorporates", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<PersonCorporate>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<PersonCorporate>()
            {
                Items = Enumerable.Empty<PersonCorporate>()

            };

        }
        public async Task<PersonCorporate> GetPersonCorporateById(int PersonCorporateId)
        {
            //            await SetAuthenticationHeader();
            //              PersonCorporate? _PersonCorporate;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(PersonCorporateId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPersonCorporateById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PersonCorporate>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<PersonCorporate> AddPersonCorporate(PersonCorporate entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PersonCorporate>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddPersonCorporate", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PersonCorporate>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<PersonCorporate> UpdatePersonCorporate(PersonCorporate entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PersonCorporate>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdatePersonCorporate", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PersonCorporate>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeletePersonCorporate(PersonCorporate entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PersonCorporate>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeletePersonCorporate", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
