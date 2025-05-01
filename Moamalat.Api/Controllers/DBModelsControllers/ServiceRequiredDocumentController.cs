using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceRequiredDocumentController : ApiBaseController
      {
         private readonly ServiceRequiredDocumentRepository _servicerequireddocumentRepository;
         public ServiceRequiredDocumentController(ServiceRequiredDocumentRepository servicerequireddocumentRepository)
        {
           _servicerequireddocumentRepository=servicerequireddocumentRepository;
        }

        //*****************************************************************************
        //  ServiceRequiredDocument
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllServiceRequiredDocuments")]
        public async Task<ActionResult<ApiResponse>> GetAllServiceRequiredDocuments()
        {
            IEnumerable<ServiceRequiredDocument> data = await _servicerequireddocumentRepository.GetAllServiceRequiredDocuments();
            if (!data.Any())
                data = Enumerable.Empty<ServiceRequiredDocument>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<ServiceRequiredDocument>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedServiceRequiredDocuments
        [HttpPost("GetPagedServiceRequiredDocuments")]
        public async Task<ActionResult<ApiResponse>> GetPagedServiceRequiredDocuments(ApiRequest apiRequest)
        { 
           PaginationResult<ServiceRequiredDocument> _Pagination = Deserialize<PaginationResult<ServiceRequiredDocument>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _servicerequireddocumentRepository.GetPagedServiceRequiredDocuments(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<ServiceRequiredDocument>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetServiceRequiredDocumentById
        [HttpPost("GetServiceRequiredDocumentById")]
        public async Task<ActionResult<ApiResponse>> GetServiceRequiredDocumentById(ApiRequest apiRequest)
        {
          string RequiredDocumentId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _servicerequireddocumentRepository.GetServiceRequiredDocumentById(Convert.ToInt32(RequiredDocumentId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceRequiredDocument>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddServiceRequiredDocument
        [HttpPost("AddServiceRequiredDocument")]
        public async Task<ActionResult<ApiResponse>> AddServiceRequiredDocument(ApiRequest apiRequest)
        {
            var Entity = Deserialize<ServiceRequiredDocument>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _servicerequireddocumentRepository.AddServiceRequiredDocument(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceRequiredDocument>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateServiceRequiredDocument
        [HttpPost("UpdateServiceRequiredDocument")]
        public async Task<ActionResult<ApiResponse>> UpdateServiceRequiredDocument(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<ServiceRequiredDocument>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<ServiceRequiredDocument>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _servicerequireddocumentRepository.UpdateServiceRequiredDocument(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<ServiceRequiredDocument>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteServiceRequiredDocument
        [HttpPost("DeleteServiceRequiredDocument")]
        public async Task<ActionResult<ApiResponse>> DeleteServiceRequiredDocument(ApiRequest apiRequest)
        {

            var Entity = Deserialize<ServiceRequiredDocument>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<ServiceRequiredDocument>(Utilities.Decrypt(apiRequest.Content));
            await _servicerequireddocumentRepository.DeleteServiceRequiredDocument(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceRequiredDocument>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
