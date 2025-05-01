using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentController : ApiBaseController
      {
         private readonly DocumentRepository _documentRepository;
         public DocumentController(DocumentRepository documentRepository)
        {
           _documentRepository=documentRepository;
        }

        //*****************************************************************************
        //  Document
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllDocuments")]
        public async Task<ActionResult<ApiResponse>> GetAllDocuments()
        {
            IEnumerable<Document> data = await _documentRepository.GetAllDocuments();
            if (!data.Any())
                data = Enumerable.Empty<Document>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Document>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedDocuments
        [HttpPost("GetPagedDocuments")]
        public async Task<ActionResult<ApiResponse>> GetPagedDocuments(ApiRequest apiRequest)
        { 
           PaginationResult<Document> _Pagination = Deserialize<PaginationResult<Document>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _documentRepository.GetPagedDocuments(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Document>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetDocumentById
        [HttpPost("GetDocumentById")]
        public async Task<ActionResult<ApiResponse>> GetDocumentById(ApiRequest apiRequest)
        {
          string DocumentId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _documentRepository.GetDocumentById(Convert.ToInt32(DocumentId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Document>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddDocument
        [HttpPost("AddDocument")]
        public async Task<ActionResult<ApiResponse>> AddDocument(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Document>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _documentRepository.AddDocument(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Document>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateDocument
        [HttpPost("UpdateDocument")]
        public async Task<ActionResult<ApiResponse>> UpdateDocument(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Document>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Document>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _documentRepository.UpdateDocument(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Document>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteDocument
        [HttpPost("DeleteDocument")]
        public async Task<ActionResult<ApiResponse>> DeleteDocument(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Document>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Document>(Utilities.Decrypt(apiRequest.Content));
            await _documentRepository.DeleteDocument(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Document>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
