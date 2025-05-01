using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentTypeController : ApiBaseController
      {
         private readonly DocumentTypeRepository _documenttypeRepository;
         public DocumentTypeController(DocumentTypeRepository documenttypeRepository)
        {
           _documenttypeRepository=documenttypeRepository;
        }

        //*****************************************************************************
        //  DocumentType
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllDocumentTypes")]
        public async Task<ActionResult<ApiResponse>> GetAllDocumentTypes()
        {
            IEnumerable<DocumentType> data = await _documenttypeRepository.GetAllDocumentTypes();
            if (!data.Any())
                data = Enumerable.Empty<DocumentType>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<DocumentType>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedDocumentTypes
        [HttpPost("GetPagedDocumentTypes")]
        public async Task<ActionResult<ApiResponse>> GetPagedDocumentTypes(ApiRequest apiRequest)
        { 
           PaginationResult<DocumentType> _Pagination = Deserialize<PaginationResult<DocumentType>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _documenttypeRepository.GetPagedDocumentTypes(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<DocumentType>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetDocumentTypeById
        [HttpPost("GetDocumentTypeById")]
        public async Task<ActionResult<ApiResponse>> GetDocumentTypeById(ApiRequest apiRequest)
        {
          string DocumentTypeId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _documenttypeRepository.GetDocumentTypeById(Convert.ToInt32(DocumentTypeId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<DocumentType>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddDocumentType
        [HttpPost("AddDocumentType")]
        public async Task<ActionResult<ApiResponse>> AddDocumentType(ApiRequest apiRequest)
        {
            var Entity = Deserialize<DocumentType>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _documenttypeRepository.AddDocumentType(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<DocumentType>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateDocumentType
        [HttpPost("UpdateDocumentType")]
        public async Task<ActionResult<ApiResponse>> UpdateDocumentType(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<DocumentType>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<DocumentType>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _documenttypeRepository.UpdateDocumentType(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<DocumentType>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteDocumentType
        [HttpPost("DeleteDocumentType")]
        public async Task<ActionResult<ApiResponse>> DeleteDocumentType(ApiRequest apiRequest)
        {

            var Entity = Deserialize<DocumentType>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<DocumentType>(Utilities.Decrypt(apiRequest.Content));
            await _documenttypeRepository.DeleteDocumentType(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<DocumentType>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
