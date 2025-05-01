using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VwRequestDataController : ApiBaseController
      {
         private readonly VwRequestDataRepository _vwrequestdataRepository;
         public VwRequestDataController(VwRequestDataRepository vwrequestdataRepository)
        {
           _vwrequestdataRepository=vwrequestdataRepository;
        }

        //*****************************************************************************
        //  VwRequestData
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllVwRequestDatas")]
        public async Task<ActionResult<ApiResponse>> GetAllVwRequestDatas()
        {
            IEnumerable<VwRequestData> data = await _vwrequestdataRepository.GetAllVwRequestDatas();
            if (!data.Any())
                data = Enumerable.Empty<VwRequestData>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<VwRequestData>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedVwRequestDatas
        [HttpPost("GetPagedVwRequestDatas")]
        public async Task<ActionResult<ApiResponse>> GetPagedVwRequestDatas(ApiRequest apiRequest)
        { 
           PaginationResult<VwRequestData> _Pagination = Deserialize<PaginationResult<VwRequestData>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _vwrequestdataRepository.GetPagedVwRequestDatas(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<VwRequestData>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetVwRequestDataById
        [HttpPost("GetVwRequestDataById")]
        public async Task<ActionResult<ApiResponse>> GetVwRequestDataById(ApiRequest apiRequest)
        {
          string RequestId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _vwrequestdataRepository.GetVwRequestDataById(Convert.ToInt32(RequestId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<VwRequestData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddVwRequestData
        [HttpPost("AddVwRequestData")]
        public async Task<ActionResult<ApiResponse>> AddVwRequestData(ApiRequest apiRequest)
        {
            var Entity = Deserialize<VwRequestData>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _vwrequestdataRepository.AddVwRequestData(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<VwRequestData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateVwRequestData
        [HttpPost("UpdateVwRequestData")]
        public async Task<ActionResult<ApiResponse>> UpdateVwRequestData(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<VwRequestData>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<VwRequestData>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _vwrequestdataRepository.UpdateVwRequestData(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<VwRequestData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteVwRequestData
        [HttpPost("DeleteVwRequestData")]
        public async Task<ActionResult<ApiResponse>> DeleteVwRequestData(ApiRequest apiRequest)
        {

            var Entity = Deserialize<VwRequestData>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<VwRequestData>(Utilities.Decrypt(apiRequest.Content));
            await _vwrequestdataRepository.DeleteVwRequestData(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<VwRequestData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
