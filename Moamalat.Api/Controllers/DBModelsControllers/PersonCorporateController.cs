using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonCorporateController : ApiBaseController
      {
         private readonly PersonCorporateRepository _personcorporateRepository;
         public PersonCorporateController(PersonCorporateRepository personcorporateRepository)
        {
           _personcorporateRepository=personcorporateRepository;
        }

        //*****************************************************************************
        //  PersonCorporate
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllPersonCorporates")]
        public async Task<ActionResult<ApiResponse>> GetAllPersonCorporates()
        {
            IEnumerable<PersonCorporate> data = await _personcorporateRepository.GetAllPersonCorporates();
            if (!data.Any())
                data = Enumerable.Empty<PersonCorporate>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<PersonCorporate>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedPersonCorporates
        [HttpPost("GetPagedPersonCorporates")]
        public async Task<ActionResult<ApiResponse>> GetPagedPersonCorporates(ApiRequest apiRequest)
        { 
           PaginationResult<PersonCorporate> _Pagination = Deserialize<PaginationResult<PersonCorporate>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _personcorporateRepository.GetPagedPersonCorporates(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<PersonCorporate>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetPersonCorporateById
        [HttpPost("GetPersonCorporateById")]
        public async Task<ActionResult<ApiResponse>> GetPersonCorporateById(ApiRequest apiRequest)
        {
          string PersonCorporateId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _personcorporateRepository.GetPersonCorporateById(Convert.ToInt32(PersonCorporateId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PersonCorporate>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddPersonCorporate
        [HttpPost("AddPersonCorporate")]
        public async Task<ActionResult<ApiResponse>> AddPersonCorporate(ApiRequest apiRequest)
        {
            var Entity = Deserialize<PersonCorporate>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _personcorporateRepository.AddPersonCorporate(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PersonCorporate>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdatePersonCorporate
        [HttpPost("UpdatePersonCorporate")]
        public async Task<ActionResult<ApiResponse>> UpdatePersonCorporate(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<PersonCorporate>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<PersonCorporate>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _personcorporateRepository.UpdatePersonCorporate(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<PersonCorporate>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeletePersonCorporate
        [HttpPost("DeletePersonCorporate")]
        public async Task<ActionResult<ApiResponse>> DeletePersonCorporate(ApiRequest apiRequest)
        {

            var Entity = Deserialize<PersonCorporate>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<PersonCorporate>(Utilities.Decrypt(apiRequest.Content));
            await _personcorporateRepository.DeletePersonCorporate(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PersonCorporate>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
