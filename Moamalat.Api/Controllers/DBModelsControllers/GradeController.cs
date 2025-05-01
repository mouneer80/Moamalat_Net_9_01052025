using Moamalat.Data; 
using Moamalat.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
namespace Moamalat.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GradeController : ApiBaseController
      {
         private readonly GradeRepository _gradeRepository;
         public GradeController(GradeRepository gradeRepository)
        {
           _gradeRepository=gradeRepository;
        }

        //*****************************************************************************
        //  Grade
        //*****************************************************************************
        // GET: api/[controller]
        [HttpGet("GetAllGrades")]
        public async Task<ActionResult<ApiResponse>> GetAllGrades()
        {
            IEnumerable<Grade> data = await _gradeRepository.GetAllGrades();
            if (!data.Any())
                data = Enumerable.Empty<Grade>();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<IEnumerable<Grade>>(data)) ;
            return Ok(Serialize<ApiResponse>(Response));
        }
        // GET: api/[controller]/GetpagedGrades
        [HttpPost("GetPagedGrades")]
        public async Task<ActionResult<ApiResponse>> GetPagedGrades(ApiRequest apiRequest)
        { 
           PaginationResult<Grade> _Pagination = Deserialize<PaginationResult<Grade>>(Utilities.Decrypt(apiRequest.Content));
          var Entity = await _gradeRepository.GetPagedGrades(_Pagination);
          if (Entity == null)
             throw new NotFoundException();
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<PaginationResult<Grade>>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }

        // POST: api/[controller]/GetGradeById
        [HttpPost("GetGradeById")]
        public async Task<ActionResult<ApiResponse>> GetGradeById(ApiRequest apiRequest)
        {
          string GradeId=Utilities.Decrypt(apiRequest.Content);
          var Entity = await _gradeRepository.GetGradeById(Convert.ToInt32(GradeId));
            var Response=new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Grade>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/AddGrade
        [HttpPost("AddGrade")]
        public async Task<ActionResult<ApiResponse>> AddGrade(ApiRequest apiRequest)
        {
            var Entity = Deserialize<Grade>(Utilities.Decrypt(apiRequest.Content));
            
            var Response=new ApiResponse();
            try
            {
                Entity = await _gradeRepository.AddGrade(Entity);
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Grade>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message = ex.Message;
            }
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/UpdateGrade
        [HttpPost("UpdateGrade")]
        public async Task<ActionResult<ApiResponse>> UpdateGrade(ApiRequest apiRequest)
        {
          
       var Entity = Deserialize<Grade>(Utilities.Decrypt(apiRequest.Content)); 
          if (Entity == null)
               {
                  throw new NotFoundException();
                  
                }
            //var Entity = Deserialize<Grade>(Utilities.Decrypt(apiRequest.Content));
            var Response = new ApiResponse();
            try
            {
                await _gradeRepository.UpdateGrade(Entity);

                Response.Succeeded = true;
                Response.DataObject = Utilities.Encrypt(Serialize<Grade>(Entity));
            }
            catch (Exception ex)
            {

                Response.Succeeded = false;
                Response.Message=ex.Message;
            }
            
            return Ok(Serialize<ApiResponse>(Response));
        }
        // POST: api/[controller]/DeleteGrade
        [HttpPost("DeleteGrade")]
        public async Task<ActionResult<ApiResponse>> DeleteGrade(ApiRequest apiRequest)
        {

            var Entity = Deserialize<Grade>(Utilities.Decrypt(apiRequest.Content));
            if (Entity == null)
            {
                throw new NotFoundException();

            }
            //var Entity = Deserialize<Grade>(Utilities.Decrypt(apiRequest.Content));
            await _gradeRepository.DeleteGrade(Entity);
            var Response = new ApiResponse();
            Response.Succeeded = true;
            Response.DataObject = Utilities.Encrypt(Serialize<Grade>(Entity));
            return Ok(Serialize<ApiResponse>(Response));
        }
        
	  // End Controller
    }
}
