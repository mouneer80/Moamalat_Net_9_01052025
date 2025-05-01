using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ApiBaseController
      {
         private readonly PersonRepository _personRepository;
         public PersonController(PersonRepository personRepository)
        {
           _personRepository=personRepository;
        }

        //*****************************************************************************
        //  Person
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllPersons")]
        public async Task<ActionResult<ApiResponse>> GetAllPersons()
        {
            IEnumerable<Person> data = await _personRepository.GetAllPersons();
            if (!data.Any())
                data = Enumerable.Empty<Person>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Person>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedPersons
        [HttpPost("GetPagedPersons")]
        public async Task<ActionResult<ApiResponse>> GetPagedPersons(ApiRequest apiRequest)
        { 
           PaginationResult<Person> _Pagination = Deserialize<PaginationResult<Person>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _personRepository.GetPagedPersons(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Person>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetPersonById
        [HttpPost("GetPersonById")]
        public async Task<ActionResult<ApiResponse>> GetPersonById(ApiRequest apiRequest)
        {
          string PersonId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _personRepository.GetPersonById(Convert.ToInt32(PersonId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Person>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddPerson
        [HttpPost("AddPerson")]
        public async Task<ActionResult<ApiResponse>> AddPerson(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Person>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _personRepository.AddPerson(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Person>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdatePerson
        [HttpPost("UpdatePerson")]
        public async Task<ActionResult<ApiResponse>> UpdatePerson(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Person>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Person>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _personRepository.UpdatePerson(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Person>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeletePerson
        [HttpPost("DeletePerson")]
        public async Task<ActionResult<ApiResponse>> DeletePerson(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Person>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Person>(Utilities.Decrypt(apiRequest.Content));
            await _personRepository.DeletePerson(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Person>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
