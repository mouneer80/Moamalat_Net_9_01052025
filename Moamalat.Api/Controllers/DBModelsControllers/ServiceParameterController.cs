using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceParameterController : ApiBaseController
      {
         private readonly ServiceParameterRepository _serviceparameterRepository;
         public ServiceParameterController(ServiceParameterRepository serviceparameterRepository)
        {
           _serviceparameterRepository=serviceparameterRepository;
        }

        //*****************************************************************************
        //  ServiceParameter
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllServiceParameters")]
        public async Task<ActionResult<ApiResponse>> GetAllServiceParameters()
        {
            IEnumerable<ServiceParameter> data = await _serviceparameterRepository.GetAllServiceParameters();
            if (!data.Any())
                data = Enumerable.Empty<ServiceParameter>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<ServiceParameter>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedServiceParameters
        [HttpPost("GetPagedServiceParameters")]
        public async Task<ActionResult<ApiResponse>> GetPagedServiceParameters(ApiRequest apiRequest)
        { 
           PaginationResult<ServiceParameter> _Pagination = Deserialize<PaginationResult<ServiceParameter>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _serviceparameterRepository.GetPagedServiceParameters(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<ServiceParameter>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetServiceParameterById
        [HttpPost("GetServiceParameterById")]
        public async Task<ActionResult<ApiResponse>> GetServiceParameterById(ApiRequest apiRequest)
        {
          string ServiceParameterId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _serviceparameterRepository.GetServiceParameterById(Convert.ToInt32(ServiceParameterId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceParameter>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddServiceParameter
        [HttpPost("AddServiceParameter")]
        public async Task<ActionResult<ApiResponse>> AddServiceParameter(ApiRequest apiRequest)
        {
            var Entity = Deserialize<ServiceParameter>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _serviceparameterRepository.AddServiceParameter(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceParameter>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateServiceParameter
        [HttpPost("UpdateServiceParameter")]
        public async Task<ActionResult<ApiResponse>> UpdateServiceParameter(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<ServiceParameter>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<ServiceParameter>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _serviceparameterRepository.UpdateServiceParameter(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<ServiceParameter>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteServiceParameter
        [HttpPost("DeleteServiceParameter")]
        public async Task<ActionResult<ApiResponse>> DeleteServiceParameter(ApiRequest apiRequest)
        {

            var Entity = Deserialize<ServiceParameter>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<ServiceParameter>(Utilities.Decrypt(apiRequest.Content));
            await _serviceparameterRepository.DeleteServiceParameter(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceParameter>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
