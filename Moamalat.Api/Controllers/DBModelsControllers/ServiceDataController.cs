using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceDataController : ApiBaseController
      {
         private readonly ServiceDataRepository _servicedataRepository;
         public ServiceDataController(ServiceDataRepository servicedataRepository)
        {
           _servicedataRepository=servicedataRepository;
        }

        //*****************************************************************************
        //  ServiceData
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllServiceDatas")]
        public async Task<ActionResult<ApiResponse>> GetAllServiceDatas()
        {
            IEnumerable<ServiceData> data = await _servicedataRepository.GetAllServiceDatas();
            if (!data.Any())
                data = Enumerable.Empty<ServiceData>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<ServiceData>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedServiceDatas
        [HttpPost("GetPagedServiceDatas")]
        public async Task<ActionResult<ApiResponse>> GetPagedServiceDatas(ApiRequest apiRequest)
        { 
           PaginationResult<ServiceData> _Pagination = Deserialize<PaginationResult<ServiceData>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _servicedataRepository.GetPagedServiceDatas(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<ServiceData>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetServiceDataById
        [HttpPost("GetServiceDataById")]
        public async Task<ActionResult<ApiResponse>> GetServiceDataById(ApiRequest apiRequest)
        {
          string ServiceId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _servicedataRepository.GetServiceDataById(Convert.ToInt32(ServiceId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddServiceData
        [HttpPost("AddServiceData")]
        public async Task<ActionResult<ApiResponse>> AddServiceData(ApiRequest apiRequest)
        {
            var Entity = Deserialize<ServiceData>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _servicedataRepository.AddServiceData(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateServiceData
        [HttpPost("UpdateServiceData")]
        public async Task<ActionResult<ApiResponse>> UpdateServiceData(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<ServiceData>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<ServiceData>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _servicedataRepository.UpdateServiceData(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<ServiceData>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteServiceData
        [HttpPost("DeleteServiceData")]
        public async Task<ActionResult<ApiResponse>> DeleteServiceData(ApiRequest apiRequest)
        {

            var Entity = Deserialize<ServiceData>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<ServiceData>(Utilities.Decrypt(apiRequest.Content));
            await _servicedataRepository.DeleteServiceData(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<ServiceData>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        [HttpPost("GetServiceParameterByServiceId")]
        public async Task<ActionResult<ApiResponse>> GetServiceParameterByServiceId(ApiRequest apiRequest)
        {
            string ServiceId = Utilities.Decrypt(apiRequest.Content);
            var Entity = await _servicedataRepository.GetServiceParameterByServiceId(Convert.ToInt32(ServiceId));
            if (Entity == null)
                throw new NotFoundException();
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<List<ServiceParameter>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        [HttpPost("GetSericeRequiredDocumentByServiceId")]
        public async Task<ActionResult<ApiResponse>> GetSericeRequiredDocumentByServiceId(ApiRequest apiRequest)
        {
            string ServiceId = Utilities.Decrypt(apiRequest.Content);
            var Entity = await _servicedataRepository.GetSericeRequiredDocumentByServiceId(Convert.ToInt32(ServiceId));
            if (Entity == null)
                throw new NotFoundException();
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<List<ServiceRequiredDocument>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        [HttpPost("GetServicesByCategoryId")]
        public async Task<ActionResult<ApiResponse>> GetServicesByCategoryId(ApiRequest apiRequest)
        {
            string CategoryId = Utilities.Decrypt(apiRequest.Content);
            var Entity = await _servicedataRepository.GetByCategoryId(Convert.ToInt32(CategoryId));
            if (Entity == null)
                throw new NotFoundException();
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<List<ServiceData>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        [HttpPost("SaveServiceRequestData")]
        public async Task<ActionResult<ApiResponse>> SaveServiceRequestData(ApiRequest apiRequest)
        {

            //ArrayList arrayList = Deserialize<ArrayList>(Utilities.Decrypt(apiRequest.Content));
            RequestData requestData = Deserialize<RequestData>(Utilities.Decrypt(apiRequest.Content));

            //RequestDocument requestDocument = (RequestDocument)arrayList[1];

            var Entity = await _servicedataRepository.SaveServiceRequest(requestData);

            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<bool>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // End Controller
    }
}
