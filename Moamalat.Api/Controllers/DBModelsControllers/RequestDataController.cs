using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestDataController : ApiBaseController
      {
         private readonly RequestDataRepository _requestdataRepository;
         public RequestDataController(RequestDataRepository requestdataRepository)
        {
           _requestdataRepository=requestdataRepository;
        }

        //*****************************************************************************
        //  RequestData
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllRequestDatas")]
        public async Task<ActionResult<ApiResponse>> GetAllRequestDatas()
        {
            IEnumerable<RequestData> data = await _requestdataRepository.GetAllRequestDatas();
            if (!data.Any())
                data = Enumerable.Empty<RequestData>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<RequestData>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedRequestDatas
        [HttpPost("GetPagedRequestDatas")]
        public async Task<ActionResult<ApiResponse>> GetPagedRequestDatas(ApiRequest apiRequest)
        { 
           PaginationResult<RequestData> _Pagination = Deserialize<PaginationResult<RequestData>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _requestdataRepository.GetPagedRequestDatas(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<RequestData>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetRequestDataById
        [HttpPost("GetRequestDataById")]
        public async Task<ActionResult<ApiResponse>> GetRequestDataById(ApiRequest apiRequest)
        {
          string RequestId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _requestdataRepository.GetRequestDataById(Convert.ToInt32(RequestId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddRequestData
        [HttpPost("AddRequestData")]
        public async Task<ActionResult<ApiResponse>> AddRequestData(ApiRequest apiRequest)
        {
            var Entity = Deserialize<RequestData>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _requestdataRepository.AddRequestData(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateRequestData
        [HttpPost("UpdateRequestData")]
        public async Task<ActionResult<ApiResponse>> UpdateRequestData(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<RequestData>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<RequestData>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _requestdataRepository.UpdateRequestData(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<RequestData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteRequestData
        [HttpPost("DeleteRequestData")]
        public async Task<ActionResult<ApiResponse>> DeleteRequestData(ApiRequest apiRequest)
        {

            var Entity = Deserialize<RequestData>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<RequestData>(Utilities.Decrypt(apiRequest.Content));
            await _requestdataRepository.DeleteRequestData(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
