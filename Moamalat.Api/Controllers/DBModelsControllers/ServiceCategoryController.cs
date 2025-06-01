using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceCategoryController : ApiBaseController
      {
         private readonly ServiceCategoryRepository _servicecategoryRepository;
         public ServiceCategoryController(ServiceCategoryRepository servicecategoryRepository)
        {
           _servicecategoryRepository=servicecategoryRepository;
        }

        //*****************************************************************************
        //  ServiceCategory
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllServiceCategories")]
        public async Task<ActionResult<ApiResponse>> GetAllServiceCategories()
        {
            IEnumerable<ServiceCategory> data = await _servicecategoryRepository.GetAllServiceCategories();
            if (!data.Any())
                data = Enumerable.Empty<ServiceCategory>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<ServiceCategory>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedServiceCategories
        [HttpPost("GetPagedServiceCategories")]
        public async Task<ActionResult<ApiResponse>> GetPagedServiceCategories(ApiRequest apiRequest)
        { 
           PaginationResult<ServiceCategory> _Pagination = Deserialize<PaginationResult<ServiceCategory>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _servicecategoryRepository.GetPagedServiceCategories(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<ServiceCategory>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // GET: api/[controller]/GetpagedServiceCategorys
        [HttpPost("GetPagedServiceCategorys")]
        public async Task<ActionResult<ApiResponse>> GetPagedServiceCategorys(ApiRequest apiRequest)
        {
            PaginationResult<ServiceCategory> _Pagination = Deserialize<PaginationResult<ServiceCategory>>(Utilities.Decrypt(apiRequest.Content));
            var Entity = await _servicecategoryRepository.GetPagedServiceCategories(_Pagination);
            if (Entity == null)
                throw new NotFoundException();
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<ServiceCategory>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetServiceCategoryById
        [HttpPost("GetServiceCategoryById")]
        public async Task<ActionResult<ApiResponse>> GetServiceCategoryById(ApiRequest apiRequest)
        {
          string ServiceCategoryId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _servicecategoryRepository.GetServiceCategoryById(Convert.ToInt32(ServiceCategoryId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceCategory>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddServiceCategory
        [HttpPost("AddServiceCategory")]
        public async Task<ActionResult<ApiResponse>> AddServiceCategory(ApiRequest apiRequest)
        {
            var Entity = Deserialize<ServiceCategory>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _servicecategoryRepository.AddServiceCategory(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceCategory>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateServiceCategory
        [HttpPost("UpdateServiceCategory")]
        public async Task<ActionResult<ApiResponse>> UpdateServiceCategory(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<ServiceCategory>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<ServiceCategory>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _servicecategoryRepository.UpdateServiceCategory(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<ServiceCategory>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteServiceCategory
        [HttpPost("DeleteServiceCategory")]
        public async Task<ActionResult<ApiResponse>> DeleteServiceCategory(ApiRequest apiRequest)
        {

            var Entity = Deserialize<ServiceCategory>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<ServiceCategory>(Utilities.Decrypt(apiRequest.Content));
            await _servicecategoryRepository.DeleteServiceCategory(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceCategory>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
