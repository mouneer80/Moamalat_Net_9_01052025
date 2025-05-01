using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestStatuController : ApiBaseController
      {
         private readonly RequestStatuRepository _requeststatuRepository;
         public RequestStatuController(RequestStatuRepository requeststatuRepository)
        {
           _requeststatuRepository=requeststatuRepository;
        }

        //*****************************************************************************
        //  RequestStatu
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllRequestStatus")]
        public async Task<ActionResult<ApiResponse>> GetAllRequestStatus()
        {
            IEnumerable<RequestStatu> data = await _requeststatuRepository.GetAllRequestStatus();
            if (!data.Any())
                data = Enumerable.Empty<RequestStatu>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<RequestStatu>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedRequestStatus
        [HttpPost("GetPagedRequestStatus")]
        public async Task<ActionResult<ApiResponse>> GetPagedRequestStatus(ApiRequest apiRequest)
        { 
           PaginationResult<RequestStatu> _Pagination = Deserialize<PaginationResult<RequestStatu>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _requeststatuRepository.GetPagedRequestStatus(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<RequestStatu>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetRequestStatuById
        [HttpPost("GetRequestStatuById")]
        public async Task<ActionResult<ApiResponse>> GetRequestStatuById(ApiRequest apiRequest)
        {
          string StatusId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _requeststatuRepository.GetRequestStatuById(Convert.ToInt32(StatusId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestStatu>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddRequestStatu
        [HttpPost("AddRequestStatu")]
        public async Task<ActionResult<ApiResponse>> AddRequestStatu(ApiRequest apiRequest)
        {
            var Entity = Deserialize<RequestStatu>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _requeststatuRepository.AddRequestStatu(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestStatu>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateRequestStatu
        [HttpPost("UpdateRequestStatu")]
        public async Task<ActionResult<ApiResponse>> UpdateRequestStatu(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<RequestStatu>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<RequestStatu>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _requeststatuRepository.UpdateRequestStatu(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<RequestStatu>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteRequestStatu
        [HttpPost("DeleteRequestStatu")]
        public async Task<ActionResult<ApiResponse>> DeleteRequestStatu(ApiRequest apiRequest)
        {

            var Entity = Deserialize<RequestStatu>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<RequestStatu>(Utilities.Decrypt(apiRequest.Content));
            await _requeststatuRepository.DeleteRequestStatu(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestStatu>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
