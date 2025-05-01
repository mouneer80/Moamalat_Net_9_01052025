using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : ApiBaseController
      {
         private readonly AddressRepository _addressRepository;
         public AddressController(AddressRepository addressRepository)
        {
           _addressRepository=addressRepository;
        }

        //*****************************************************************************
        //  Address
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllAddresses")]
        public async Task<ActionResult<ApiResponse>> GetAllAddresses()
        {
            IEnumerable<Address> data = await _addressRepository.GetAllAddresses();
            if (!data.Any())
                data = Enumerable.Empty<Address>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Address>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedAddresses
        [HttpPost("GetPagedAddresses")]
        public async Task<ActionResult<ApiResponse>> GetPagedAddresses(ApiRequest apiRequest)
        { 
           PaginationResult<Address> _Pagination = Deserialize<PaginationResult<Address>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _addressRepository.GetPagedAddresses(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Address>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetAddressById
        [HttpPost("GetAddressById")]
        public async Task<ActionResult<ApiResponse>> GetAddressById(ApiRequest apiRequest)
        {
          string AddressId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _addressRepository.GetAddressById((AddressId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Address>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddAddress
        [HttpPost("AddAddress")]
        public async Task<ActionResult<ApiResponse>> AddAddress(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Address>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _addressRepository.AddAddress(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Address>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateAddress
        [HttpPost("UpdateAddress")]
        public async Task<ActionResult<ApiResponse>> UpdateAddress(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Address>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Address>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _addressRepository.UpdateAddress(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Address>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteAddress
        [HttpPost("DeleteAddress")]
        public async Task<ActionResult<ApiResponse>> DeleteAddress(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Address>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Address>(Utilities.Decrypt(apiRequest.Content));
            await _addressRepository.DeleteAddress(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Address>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
