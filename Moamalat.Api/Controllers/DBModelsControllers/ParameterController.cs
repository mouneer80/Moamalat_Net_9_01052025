using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParameterController : ApiBaseController
      {
         private readonly ParameterRepository _parameterRepository;
         public ParameterController(ParameterRepository parameterRepository)
        {
           _parameterRepository=parameterRepository;
        }

        //*****************************************************************************
        //  Parameter
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllParameters")]
        public async Task<ActionResult<ApiResponse>> GetAllParameters()
        {
            IEnumerable<Parameter> data = await _parameterRepository.GetAllParameters();
            if (!data.Any())
                data = Enumerable.Empty<Parameter>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Parameter>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedParameters
        [HttpPost("GetPagedParameters")]
        public async Task<ActionResult<ApiResponse>> GetPagedParameters(ApiRequest apiRequest)
        { 
           PaginationResult<Parameter> _Pagination = Deserialize<PaginationResult<Parameter>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _parameterRepository.GetPagedParameters(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Parameter>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetParameterById
        [HttpPost("GetParameterById")]
        public async Task<ActionResult<ApiResponse>> GetParameterById(ApiRequest apiRequest)
        {
          string ParameterId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _parameterRepository.GetParameterById(Convert.ToInt32(ParameterId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Parameter>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddParameter
        [HttpPost("AddParameter")]
        public async Task<ActionResult<ApiResponse>> AddParameter(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Parameter>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _parameterRepository.AddParameter(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Parameter>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateParameter
        [HttpPost("UpdateParameter")]
        public async Task<ActionResult<ApiResponse>> UpdateParameter(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Parameter>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Parameter>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _parameterRepository.UpdateParameter(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Parameter>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteParameter
        [HttpPost("DeleteParameter")]
        public async Task<ActionResult<ApiResponse>> DeleteParameter(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Parameter>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Parameter>(Utilities.Decrypt(apiRequest.Content));
            await _parameterRepository.DeleteParameter(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Parameter>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
