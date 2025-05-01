using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestTransactionController : ApiBaseController
      {
         private readonly RequestTransactionRepository _requesttransactionRepository;
         public RequestTransactionController(RequestTransactionRepository requesttransactionRepository)
        {
           _requesttransactionRepository=requesttransactionRepository;
        }

        //*****************************************************************************
        //  RequestTransaction
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllRequestTransactions")]
        public async Task<ActionResult<ApiResponse>> GetAllRequestTransactions()
        {
            IEnumerable<RequestTransaction> data = await _requesttransactionRepository.GetAllRequestTransactions();
            if (!data.Any())
                data = Enumerable.Empty<RequestTransaction>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<RequestTransaction>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedRequestTransactions
        [HttpPost("GetPagedRequestTransactions")]
        public async Task<ActionResult<ApiResponse>> GetPagedRequestTransactions(ApiRequest apiRequest)
        { 
           PaginationResult<RequestTransaction> _Pagination = Deserialize<PaginationResult<RequestTransaction>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _requesttransactionRepository.GetPagedRequestTransactions(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<RequestTransaction>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetRequestTransactionById
        [HttpPost("GetRequestTransactionById")]
        public async Task<ActionResult<ApiResponse>> GetRequestTransactionById(ApiRequest apiRequest)
        {
          string TransactionId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _requesttransactionRepository.GetRequestTransactionById(Convert.ToInt32(TransactionId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestTransaction>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddRequestTransaction
        [HttpPost("AddRequestTransaction")]
        public async Task<ActionResult<ApiResponse>> AddRequestTransaction(ApiRequest apiRequest)
        {
            var Entity = Deserialize<RequestTransaction>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _requesttransactionRepository.AddRequestTransaction(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestTransaction>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateRequestTransaction
        [HttpPost("UpdateRequestTransaction")]
        public async Task<ActionResult<ApiResponse>> UpdateRequestTransaction(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<RequestTransaction>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<RequestTransaction>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _requesttransactionRepository.UpdateRequestTransaction(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<RequestTransaction>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteRequestTransaction
        [HttpPost("DeleteRequestTransaction")]
        public async Task<ActionResult<ApiResponse>> DeleteRequestTransaction(ApiRequest apiRequest)
        {

            var Entity = Deserialize<RequestTransaction>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<RequestTransaction>(Utilities.Decrypt(apiRequest.Content));
            await _requesttransactionRepository.DeleteRequestTransaction(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestTransaction>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
