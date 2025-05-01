using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TopUpHistoryController : ApiBaseController
      {
         private readonly TopUpHistoryRepository _topuphistoryRepository;
         public TopUpHistoryController(TopUpHistoryRepository topuphistoryRepository)
        {
           _topuphistoryRepository=topuphistoryRepository;
        }

        //*****************************************************************************
        //  TopUpHistory
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllTopUpHistories")]
        public async Task<ActionResult<ApiResponse>> GetAllTopUpHistories()
        {
            IEnumerable<TopUpHistory> data = await _topuphistoryRepository.GetAllTopUpHistories();
            if (!data.Any())
                data = Enumerable.Empty<TopUpHistory>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<TopUpHistory>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedTopUpHistories
        [HttpPost("GetPagedTopUpHistories")]
        public async Task<ActionResult<ApiResponse>> GetPagedTopUpHistories(ApiRequest apiRequest)
        { 
           PaginationResult<TopUpHistory> _Pagination = Deserialize<PaginationResult<TopUpHistory>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _topuphistoryRepository.GetPagedTopUpHistories(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<TopUpHistory>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetTopUpHistoryById
        [HttpPost("GetTopUpHistoryById")]
        public async Task<ActionResult<ApiResponse>> GetTopUpHistoryById(ApiRequest apiRequest)
        {
          string TopupId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _topuphistoryRepository.GetTopUpHistoryById(Convert.ToInt32(TopupId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<TopUpHistory>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddTopUpHistory
        [HttpPost("AddTopUpHistory")]
        public async Task<ActionResult<ApiResponse>> AddTopUpHistory(ApiRequest apiRequest)
        {
            var Entity = Deserialize<TopUpHistory>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _topuphistoryRepository.AddTopUpHistory(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<TopUpHistory>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateTopUpHistory
        [HttpPost("UpdateTopUpHistory")]
        public async Task<ActionResult<ApiResponse>> UpdateTopUpHistory(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<TopUpHistory>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<TopUpHistory>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _topuphistoryRepository.UpdateTopUpHistory(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<TopUpHistory>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteTopUpHistory
        [HttpPost("DeleteTopUpHistory")]
        public async Task<ActionResult<ApiResponse>> DeleteTopUpHistory(ApiRequest apiRequest)
        {

            var Entity = Deserialize<TopUpHistory>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<TopUpHistory>(Utilities.Decrypt(apiRequest.Content));
            await _topuphistoryRepository.DeleteTopUpHistory(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<TopUpHistory>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
