using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Azure;
using System.Text.Json;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentRequestController : ApiBaseController
      {
        private readonly PaymentRequestRepository _paymentrequestRepository;
        private HttpClient _httpClient { get; set; }
        private IConfiguration configuration { get; set; }
        JsonSerializerOptions SerializeOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public PaymentRequestController(PaymentRequestRepository paymentrequestRepository, IHttpClientFactory _clientFactory, IConfiguration _configuration)
        {
            _paymentrequestRepository = paymentrequestRepository;
            _httpClient = _clientFactory.CreateClient("PGHttpClient");
            configuration = _configuration;
        }

        //*****************************************************************************
        //  PaymentRequest
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllPaymentRequests")]
        public async Task<ActionResult<ApiResponse>> GetAllPaymentRequests()
        {
            IEnumerable<PaymentRequest> data = await _paymentrequestRepository.GetAllPaymentRequests();
            if (!data.Any())
                data = Enumerable.Empty<PaymentRequest>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<PaymentRequest>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedPaymentRequests
        [HttpPost("GetPagedPaymentRequests")]
        public async Task<ActionResult<ApiResponse>> GetPagedPaymentRequests(ApiRequest apiRequest)
        { 
           PaginationResult<PaymentRequest> _Pagination = Deserialize<PaginationResult<PaymentRequest>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _paymentrequestRepository.GetPagedPaymentRequests(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<PaymentRequest>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        [HttpPost("InitiatePaymentRequest")]
        public async Task<ActionResult<ApiResponse>> InitiatePaymentRequest(ApiRequest apiRequest)
        {
            var Entity = Deserialize<PaymentRequest>(Utilities.Decrypt(apiRequest.Content));


            var Response = new ApiResponse();
            try
            {
                // 

                if (Entity != null)
                {
                    Entity = await _paymentrequestRepository.AddPaymentRequest(Entity);

                    PaymentSessionData sessionData = new()
                    {
                        ClientReferenceId = Entity.TrackId.ToString(),
                        Mode = "payment",
                        Products = new()
                        {
                            new()
                            {
                                Name="TEST PAYMENT",
                                Quantity=1,
                                UnitAmount=(int)(Math.Round(Entity.Amount, 3) * 1000)
                            }
                        },
                        TotalAmount = (int)(Math.Round(Entity.Amount, 3) * 1000),
                        Currency = "OMR",
                        SuccessUrl = configuration["Payment:SuccessUrl"] + "/" + Entity.PaymentRequestId,
                        CancelUrl = configuration["Payment:CancelUrl"] + "/" + Entity.PaymentRequestId,
                        Metadata = new()
                        {
                            { "paymentRequestId", Entity.PaymentRequestId },
                            { "userId", Entity.UserId },
                            { "companyId", Entity.CompanyId },
                            { "invoiceId", Entity.InvoiceId }
                        }
                    };


                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, configuration["Payment:ApiUrl"] + configuration["Payment:InitiateSession"]);
                    request.Headers.Add("thawani-api-key", configuration["Payment:UatSecretKey"]);

                    //_httpClient.DefaultRequestHeaders.Add("thawani-api-key", configuration["Payment:UatSecretKey"]);
                    //var response = await _httpClient.PostAsJsonAsync(configuration["Payment:InitiateSession"],Serialize<object>(requestBody));

                    var requestObjectString = JsonSerializer.Serialize<PaymentSessionData>(sessionData, Serializeoptions);
                    var content = new StringContent(requestObjectString, null, "application/json");

                    request.Content = content;

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var Result = JsonSerializer.Deserialize<PGResponse>(responseContent, Serializeoptions);
                    ///=============================================================
                    Entity.SessionId = Result?.Data?.SessionId;

                    Entity = await _paymentrequestRepository.UpdatePaymentRequest(Entity);

                    Entity.HostedPageUrl = string.Format(configuration["Payment:HostedPageUrl"], Result?.Data?.SessionId, configuration["Payment:PublishableKey"]);

                    Response.Succeeded = true;
                    Response.DataObject = Utilities.Encrypt(Serialize<PaymentRequest>(Entity));
                }

            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        [HttpPost("CheckPaymentRequest")]
        public async Task<ActionResult<ApiResponse>> CheckPaymentRequest(ApiRequest apiRequest)
        {
            int RequestId = Deserialize<int>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                var Entity = await _paymentrequestRepository.GetPaymentRequestById(RequestId);
                if (Entity != null)
                {
                    //var Result = await _paymentrequestRepository.CheckPaymentRequest(Entity);

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, $"{configuration["Payment:ApiUrl"]}checkout/session/{Entity.SessionId}");
                    request.Headers.Add("thawani-api-key", configuration["Payment:UatSecretKey"]);

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var Result = JsonSerializer.Deserialize<PGResponse>(responseContent, Serializeoptions);

                    

                    if (Result != null)
                    {
                        Response.Succeeded = true;
                        Result.OriginalPaymentRequest = Entity;
                        Response.DataObject = Utilities.Encrypt(Serialize<PGResponse>(Result));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetPaymentRequestById
        [HttpPost("GetPaymentRequestById")]
        public async Task<ActionResult<ApiResponse>> GetPaymentRequestById(ApiRequest apiRequest)
        {
          string PaymentRequestId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _paymentrequestRepository.GetPaymentRequestById(Convert.ToInt32(PaymentRequestId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaymentRequest>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddPaymentRequest
        [HttpPost("AddPaymentRequest")]
        public async Task<ActionResult<ApiResponse>> AddPaymentRequest(ApiRequest apiRequest)
        {
            var Entity = Deserialize<PaymentRequest>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _paymentrequestRepository.AddPaymentRequest(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaymentRequest>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdatePaymentRequest
        [HttpPost("UpdatePaymentRequest")]
        public async Task<ActionResult<ApiResponse>> UpdatePaymentRequest(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<PaymentRequest>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<PaymentRequest>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _paymentrequestRepository.UpdatePaymentRequest(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<PaymentRequest>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeletePaymentRequest
        [HttpPost("DeletePaymentRequest")]
        public async Task<ActionResult<ApiResponse>> DeletePaymentRequest(ApiRequest apiRequest)
        {

            var Entity = Deserialize<PaymentRequest>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<PaymentRequest>(Utilities.Decrypt(apiRequest.Content));
            await _paymentrequestRepository.DeletePaymentRequest(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaymentRequest>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
