using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CorporateController : ApiBaseController
      {
         private readonly CorporateRepository _corporateRepository;
         public CorporateController(CorporateRepository corporateRepository)
        {
           _corporateRepository=corporateRepository;
        }

        //*****************************************************************************
        //  Corporate
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllCorporates")]
        public async Task<ActionResult<ApiResponse>> GetAllCorporates()
        {
            IEnumerable<Corporate> data = await _corporateRepository.GetAllCorporates();
            if (!data.Any())
                data = Enumerable.Empty<Corporate>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Corporate>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedCorporates
        [HttpPost("GetPagedCorporates")]
        public async Task<ActionResult<ApiResponse>> GetPagedCorporates(ApiRequest apiRequest)
        { 
           PaginationResult<Corporate> _Pagination = Deserialize<PaginationResult<Corporate>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _corporateRepository.GetPagedCorporates(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Corporate>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetCorporateById
        [HttpPost("GetCorporateById")]
        public async Task<ActionResult<ApiResponse>> GetCorporateById(ApiRequest apiRequest)
        {
          string CorporateId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _corporateRepository.GetCorporateById(Convert.ToInt32(CorporateId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Corporate>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddCorporate
        [HttpPost("AddCorporate")]
        public async Task<ActionResult<ApiResponse>> AddCorporate(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Corporate>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _corporateRepository.AddCorporate(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Corporate>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateCorporate
        [HttpPost("UpdateCorporate")]
        public async Task<ActionResult<ApiResponse>> UpdateCorporate(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Corporate>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Corporate>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _corporateRepository.UpdateCorporate(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Corporate>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteCorporate
        [HttpPost("DeleteCorporate")]
        public async Task<ActionResult<ApiResponse>> DeleteCorporate(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Corporate>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Corporate>(Utilities.Decrypt(apiRequest.Content));
            await _corporateRepository.DeleteCorporate(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Corporate>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
