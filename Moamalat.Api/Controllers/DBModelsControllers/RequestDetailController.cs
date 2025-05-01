using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestDetailController : ApiBaseController
      {
         private readonly RequestDetailRepository _requestdetailRepository;
         public RequestDetailController(RequestDetailRepository requestdetailRepository)
        {
           _requestdetailRepository=requestdetailRepository;
        }

        //*****************************************************************************
        //  RequestDetail
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllRequestDetails")]
        public async Task<ActionResult<ApiResponse>> GetAllRequestDetails()
        {
            IEnumerable<RequestDetail> data = await _requestdetailRepository.GetAllRequestDetails();
            if (!data.Any())
                data = Enumerable.Empty<RequestDetail>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<RequestDetail>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedRequestDetails
        [HttpPost("GetPagedRequestDetails")]
        public async Task<ActionResult<ApiResponse>> GetPagedRequestDetails(ApiRequest apiRequest)
        { 
           PaginationResult<RequestDetail> _Pagination = Deserialize<PaginationResult<RequestDetail>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _requestdetailRepository.GetPagedRequestDetails(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<RequestDetail>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetRequestDetailById
        [HttpPost("GetRequestDetailById")]
        public async Task<ActionResult<ApiResponse>> GetRequestDetailById(ApiRequest apiRequest)
        {
          string RequestDetailId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _requestdetailRepository.GetRequestDetailById(Convert.ToInt32(RequestDetailId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestDetail>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddRequestDetail
        [HttpPost("AddRequestDetail")]
        public async Task<ActionResult<ApiResponse>> AddRequestDetail(ApiRequest apiRequest)
        {
            var Entity = Deserialize<RequestDetail>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _requestdetailRepository.AddRequestDetail(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestDetail>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateRequestDetail
        [HttpPost("UpdateRequestDetail")]
        public async Task<ActionResult<ApiResponse>> UpdateRequestDetail(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<RequestDetail>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<RequestDetail>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _requestdetailRepository.UpdateRequestDetail(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<RequestDetail>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteRequestDetail
        [HttpPost("DeleteRequestDetail")]
        public async Task<ActionResult<ApiResponse>> DeleteRequestDetail(ApiRequest apiRequest)
        {

            var Entity = Deserialize<RequestDetail>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<RequestDetail>(Utilities.Decrypt(apiRequest.Content));
            await _requestdetailRepository.DeleteRequestDetail(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestDetail>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
