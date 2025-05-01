using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WilayatController : ApiBaseController
      {
         private readonly WilayatRepository _wilayatRepository;
         public WilayatController(WilayatRepository wilayatRepository)
        {
           _wilayatRepository=wilayatRepository;
        }

        //*****************************************************************************
        //  Wilayat
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllWilayats")]
        public async Task<ActionResult<ApiResponse>> GetAllWilayats()
        {
            IEnumerable<Wilayat> data = await _wilayatRepository.GetAllWilayats();
            if (!data.Any())
                data = Enumerable.Empty<Wilayat>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Wilayat>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedWilayats
        [HttpPost("GetPagedWilayats")]
        public async Task<ActionResult<ApiResponse>> GetPagedWilayats(ApiRequest apiRequest)
        { 
           PaginationResult<Wilayat> _Pagination = Deserialize<PaginationResult<Wilayat>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _wilayatRepository.GetPagedWilayats(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Wilayat>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetWilayatById
        [HttpPost("GetWilayatById")]
        public async Task<ActionResult<ApiResponse>> GetWilayatById(ApiRequest apiRequest)
        {
          string WilayatId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _wilayatRepository.GetWilayatById(Convert.ToInt32(WilayatId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Wilayat>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddWilayat
        [HttpPost("AddWilayat")]
        public async Task<ActionResult<ApiResponse>> AddWilayat(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Wilayat>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _wilayatRepository.AddWilayat(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Wilayat>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateWilayat
        [HttpPost("UpdateWilayat")]
        public async Task<ActionResult<ApiResponse>> UpdateWilayat(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Wilayat>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Wilayat>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _wilayatRepository.UpdateWilayat(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Wilayat>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteWilayat
        [HttpPost("DeleteWilayat")]
        public async Task<ActionResult<ApiResponse>> DeleteWilayat(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Wilayat>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Wilayat>(Utilities.Decrypt(apiRequest.Content));
            await _wilayatRepository.DeleteWilayat(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Wilayat>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
