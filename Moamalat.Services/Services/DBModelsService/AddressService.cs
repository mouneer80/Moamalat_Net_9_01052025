using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class AddressService : ServiceBase<Address, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="Address";
        public AddressService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  Address
        //*****************************************************************************
        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllAddresses");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<Address>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<Address>> GetPagedAddresses(PaginationResult<Address> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<Address>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedAddresses", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<Address>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<Address>()
            {
                Items = Enumerable.Empty<Address>()

            };

        }
        public async Task<Address> GetAddressById(string AddressId)
        {
            //            await SetAuthenticationHeader();
            //              Address? _Address;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(AddressId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetAddressById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Address>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<Address> AddAddress(Address entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Address>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddAddress", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Address>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<Address> UpdateAddress(Address entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Address>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateAddress", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<Address>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteAddress(Address entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<Address>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteAddress", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
	   //  End of Service
    }
}
