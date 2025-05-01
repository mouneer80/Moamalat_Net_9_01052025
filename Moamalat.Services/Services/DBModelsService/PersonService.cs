using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class PersonService : ServiceBase<Person, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Person";
        public PersonService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Person
        //*****************************************************************************
        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllPersons");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Person>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Person>> GetPagedPersons(PaginationResult<Person> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Person>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedPersons", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Person>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Person>()
            {
                Items = Enumerable.Empty<Person>()

            };

        }
        public async Task<Person> GetPersonById(int PersonId)
        {
            //            await SetAuthenticationHeader();
            //              Person? _Person;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(PersonId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPersonById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Person>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Person> AddPerson(Person entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Person>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddPerson", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Person>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Person> UpdatePerson(Person entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Person>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdatePerson", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Person>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeletePerson(Person entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Person>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeletePerson", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
