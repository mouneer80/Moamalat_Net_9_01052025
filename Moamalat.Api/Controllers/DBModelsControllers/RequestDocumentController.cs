using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequestDocumentController : ApiBaseController
      {
         private readonly RequestDocumentRepository _requestdocumentRepository;
         public RequestDocumentController(RequestDocumentRepository requestdocumentRepository)
        {
           _requestdocumentRepository=requestdocumentRepository;
        }

        //*****************************************************************************
        //  RequestDocument
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllRequestDocuments")]
        public async Task<ActionResult<ApiResponse>> GetAllRequestDocuments()
        {
            IEnumerable<RequestDocument> data = await _requestdocumentRepository.GetAllRequestDocuments();
            if (!data.Any())
                data = Enumerable.Empty<RequestDocument>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<RequestDocument>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedRequestDocuments
        [HttpPost("GetPagedRequestDocuments")]
        public async Task<ActionResult<ApiResponse>> GetPagedRequestDocuments(ApiRequest apiRequest)
        { 
           PaginationResult<RequestDocument> _Pagination = Deserialize<PaginationResult<RequestDocument>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _requestdocumentRepository.GetPagedRequestDocuments(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<RequestDocument>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetRequestDocumentById
        [HttpPost("GetRequestDocumentById")]
        public async Task<ActionResult<ApiResponse>> GetRequestDocumentById(ApiRequest apiRequest)
        {
          string RequestDocumentId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _requestdocumentRepository.GetRequestDocumentById(Convert.ToInt32(RequestDocumentId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestDocument>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddRequestDocument
        [HttpPost("AddRequestDocument")]
        public async Task<ActionResult<ApiResponse>> AddRequestDocument(ApiRequest apiRequest)
        {
            var Entity = Deserialize<RequestDocument>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _requestdocumentRepository.AddRequestDocument(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestDocument>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateRequestDocument
        [HttpPost("UpdateRequestDocument")]
        public async Task<ActionResult<ApiResponse>> UpdateRequestDocument(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<RequestDocument>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<RequestDocument>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _requestdocumentRepository.UpdateRequestDocument(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<RequestDocument>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteRequestDocument
        [HttpPost("DeleteRequestDocument")]
        public async Task<ActionResult<ApiResponse>> DeleteRequestDocument(ApiRequest apiRequest)
        {

            var Entity = Deserialize<RequestDocument>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<RequestDocument>(Utilities.Decrypt(apiRequest.Content));
            await _requestdocumentRepository.DeleteRequestDocument(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<RequestDocument>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
