using Moamalat.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Moamalat.Services
{
    public class HttpRequestHandler : DelegatingHandler
    {
        private readonly LoadingSpinnerService _loadingSpinnerService;
        private readonly ISecureDataStoreService _secureDataStoreService;
       public HttpRequestHandler(LoadingSpinnerService loadingSpinnerService, ISecureDataStoreService secureDataStoreService)
        {
            _loadingSpinnerService = loadingSpinnerService;
            _secureDataStoreService = secureDataStoreService;
            //_Messagehandler= messageHandler;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _loadingSpinnerService.Show();
            var AuthToken = await _secureDataStoreService.GetAsync<string>("AuthToken");
            if (AuthToken.Success)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", AuthToken.Value);
            }
            else
            {
               // response.StatusCode
            }
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                _loadingSpinnerService.Hide();
                return response;
            }
            catch (Exception ex)
            {
                _loadingSpinnerService.Hide();

              //  await _Messagehandler.ShowAlert("Connection Error", $"Failed to Connect to the server{Environment.NewLine}{ex.InnerException?.Message}", "Ok","Cancel");
                
                ApiResponse apiResponse = new ApiResponse() { 
                    Succeeded=false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    isConnected= false
                };

                var response = new HttpResponseMessage()
                {
                    Content = new StringContent(JsonSerializer.Serialize(apiResponse), Encoding.UTF8, "application/json")
                };
                response.StatusCode=System.Net.HttpStatusCode.ServiceUnavailable;
                
                return response;

            }




        }
    }

    public class LoadingSpinnerService
    {
        public event Action OnShow;
        public event Action OnHide;
        public void Show()
        {
            OnShow?.Invoke();
        }

        public void Hide()
        {
            OnHide?.Invoke();
        }
    }
}
