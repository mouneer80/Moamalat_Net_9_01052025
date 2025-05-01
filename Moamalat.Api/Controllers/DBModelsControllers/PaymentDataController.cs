using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentDataController : ApiBaseController
      {
         private readonly PaymentDataRepository _paymentdataRepository;
         public PaymentDataController(PaymentDataRepository paymentdataRepository)
        {
           _paymentdataRepository=paymentdataRepository;
        }

        //*****************************************************************************
        //  PaymentData
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllPaymentDatas")]
        public async Task<ActionResult<ApiResponse>> GetAllPaymentDatas()
        {
            IEnumerable<PaymentData> data = await _paymentdataRepository.GetAllPaymentDatas();
            if (!data.Any())
                data = Enumerable.Empty<PaymentData>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<PaymentData>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedPaymentDatas
        [HttpPost("GetPagedPaymentDatas")]
        public async Task<ActionResult<ApiResponse>> GetPagedPaymentDatas(ApiRequest apiRequest)
        { 
           PaginationResult<PaymentData> _Pagination = Deserialize<PaginationResult<PaymentData>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _paymentdataRepository.GetPagedPaymentDatas(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<PaymentData>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetPaymentDataById
        [HttpPost("GetPaymentDataById")]
        public async Task<ActionResult<ApiResponse>> GetPaymentDataById(ApiRequest apiRequest)
        {
          string PaymentId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _paymentdataRepository.GetPaymentDataById(Convert.ToInt32(PaymentId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaymentData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddPaymentData
        [HttpPost("AddPaymentData")]
        public async Task<ActionResult<ApiResponse>> AddPaymentData(ApiRequest apiRequest)
        {
            var Entity = Deserialize<PaymentData>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _paymentdataRepository.AddPaymentData(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaymentData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdatePaymentData
        [HttpPost("UpdatePaymentData")]
        public async Task<ActionResult<ApiResponse>> UpdatePaymentData(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<PaymentData>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<PaymentData>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _paymentdataRepository.UpdatePaymentData(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<PaymentData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeletePaymentData
        [HttpPost("DeletePaymentData")]
        public async Task<ActionResult<ApiResponse>> DeletePaymentData(ApiRequest apiRequest)
        {

            var Entity = Deserialize<PaymentData>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<PaymentData>(Utilities.Decrypt(apiRequest.Content));
            await _paymentdataRepository.DeletePaymentData(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaymentData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
