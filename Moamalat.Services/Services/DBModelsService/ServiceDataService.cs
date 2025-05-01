using Moamalat.Entities;
using System.Text.Json;
using System.Net.Http.Json;

namespace Moamalat.Services
{
    public class ServiceDataService : ServiceBase<ServiceData, IHttpClientFactory>
    {
        private readonly HttpClient _httpClient;
        private const string _controllerName="ServiceData";
        public ServiceDataService(IHttpClientFactory _httpClientFactory) : base(_controllerName,_httpClientFactory)
        {
            _httpClient = base.GetHttpClient();
        }

          //*****************************************************************************
         //  ServiceData
        //*****************************************************************************
        public async Task<IEnumerable<ServiceData>> GetAllServiceDatas()
        {
            ApiResponse Response = new ApiResponse();
            try
            {
                Response = await _httpClient.GetFromJsonAsync<ApiResponse>($"{_controllerName}/GetAllServiceDatas");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            if (Response?.DataObject != null)
            {
                var decrypted = Utilities.Decrypt(Response.DataObject);
                var entities = await DeserializeAsync<IEnumerable<ServiceData>>(decrypted);
                return entities;
            }
            return null;
        }
        public async Task<PaginationResult<ServiceData>> GetPagedServiceDatas(PaginationResult<ServiceData> _Pagination)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<PaginationResult<ServiceData>>(_Pagination)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetPagedServiceDatas", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<PaginationResult<ServiceData>>(Utilities.Decrypt(result.DataObject));
            }
            return new PaginationResult<ServiceData>()
            {
                Items = Enumerable.Empty<ServiceData>()

            };

        }
        public async Task<ServiceData> GetServiceDataById(int ServiceId)
        {
            //            await SetAuthenticationHeader();
            //              ServiceData? _ServiceData;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(ServiceId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetServiceDataById", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceData>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<ServiceData> AddServiceData(ServiceData entity)
        {
            //            await SetAuthenticationHeader();
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/AddServiceData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }
        public async Task<ServiceData> UpdateServiceData(ServiceData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/UpdateServiceData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<ServiceData>(Utilities.Decrypt(result.DataObject));
            }
            return null;
        }

        public async Task<bool> DeleteServiceData(ServiceData entity)
        {
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(Serialize<ServiceData>(entity)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/DeleteServiceData", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<ServiceParameter>> GetServiceParameterByServiceId(int ServiceId)
        {
            //            await SetAuthenticationHeader();
            //              ServiceParameter? _ServiceParameter;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(ServiceId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetServiceParameterByServiceId", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<List<ServiceParameter>>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<List<ServiceData>> GetServicesByCategoryId(int CategoryId)
        {
            //            await SetAuthenticationHeader();
            //              ServiceData? _ServiceData;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(CategoryId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetServicesByCategoryId", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<List<ServiceData>>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }
        public async Task<List<ServiceRequiredDocument>> GetSericeRequiredDocumentByServiceId(int ServiceId)
        {
            //            await SetAuthenticationHeader();
            //              ServiceParameter? _ServiceParameter;
            ApiRequest apiRequest = new() { Content = Utilities.Encrypt(ServiceId.ToString()) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/GetSericeRequiredDocumentByServiceId", apiRequest);
            if (response.IsSuccessStatusCode)
            {
                var result = await DeserializeAsync<ApiResponse>(response);
                if (result.DataObject != null)
                    return await DeserializeAsync<List<ServiceRequiredDocument>>(Utilities.Decrypt(result.DataObject));
            }
            return null;

        }

        public async Task<bool> SaveServiceRequest(RequestData requestData, List<RequestDetail> requestDetails, List<RequestDocument> requestDocuments)
        {

            requestDocuments.ForEach(d => d.CreationDate = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss")));
            requestDetails.ForEach(p => p.CreationDate = Convert.ToDecimal(DateTime.Now.ToString("yyyyMMddHHmmss")));

            requestData.RequestDetails = requestDetails;
            requestData.RequestDocuments = requestDocuments;

            ApiRequest request = new() { Content = Utilities.Encrypt(Serialize<RequestData>(requestData)) };
            var response = await _httpClient.PostAsJsonAsync<ApiRequest>($"{_controllerName}/SaveServiceRequestData", request); //SendJsonAsync<TEntity>(HttpMethod.Post, ControllerName, entity);


            var apiResponse = await DeserializeAsync<ApiResponse>(response);

            return apiResponse.Succeeded;



        }
        //  End of Service
    }
}
