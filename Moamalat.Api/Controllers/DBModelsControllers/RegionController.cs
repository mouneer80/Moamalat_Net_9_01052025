using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionController : ApiBaseController
      {
         private readonly RegionRepository _regionRepository;
         public RegionController(RegionRepository regionRepository)
        {
           _regionRepository=regionRepository;
        }

        //*****************************************************************************
        //  Region
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllRegions")]
        public async Task<ActionResult<ApiResponse>> GetAllRegions()
        {
            IEnumerable<Region> data = await _regionRepository.GetAllRegions();
            if (!data.Any())
                data = Enumerable.Empty<Region>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Region>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedRegions
        [HttpPost("GetPagedRegions")]
        public async Task<ActionResult<ApiResponse>> GetPagedRegions(ApiRequest apiRequest)
        { 
           PaginationResult<Region> _Pagination = Deserialize<PaginationResult<Region>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _regionRepository.GetPagedRegions(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Region>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetRegionById
        [HttpPost("GetRegionById")]
        public async Task<ActionResult<ApiResponse>> GetRegionById(ApiRequest apiRequest)
        {
          string RegionId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _regionRepository.GetRegionById(Convert.ToInt32(RegionId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Region>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddRegion
        [HttpPost("AddRegion")]
        public async Task<ActionResult<ApiResponse>> AddRegion(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Region>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _regionRepository.AddRegion(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Region>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateRegion
        [HttpPost("UpdateRegion")]
        public async Task<ActionResult<ApiResponse>> UpdateRegion(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Region>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Region>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _regionRepository.UpdateRegion(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Region>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteRegion
        [HttpPost("DeleteRegion")]
        public async Task<ActionResult<ApiResponse>> DeleteRegion(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Region>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Region>(Utilities.Decrypt(apiRequest.Content));
            await _regionRepository.DeleteRegion(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Region>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
