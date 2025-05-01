using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class RequestDataRepository : IRepository<RequestData>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public RequestDataRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  RequestData
        //*****************************************************************************
        public async Task<IEnumerable<RequestData>> GetAllRequestDatas()
        {
            return await _DBcontext.RequestDatas.ToListAsync();
        }
        public async Task<PaginationResult<RequestData>> GetPagedRequestDatas(PaginationResult<RequestData> _Pagination)
        {
            PaginationResult<RequestData> paginationResult = _Pagination == null ? new PaginationResult<RequestData>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<RequestData, bool> Filter = Extensions.CompileExpression<RequestData>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.RequestDatas
                                                   .Where(Extensions.StringToExpression<RequestData>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .RequestDatas;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<RequestData>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<RequestData> AddRequestData(RequestData entity)
        {
            _DBcontext.RequestDatas.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestData> GetRequestDataById(int RequestId)
        {
            return await _DBcontext.RequestDatas.FirstOrDefaultAsync(b => b.RequestId == RequestId);
        }
        public async Task<RequestData> UpdateRequestData(RequestData entity)
        {
            _DBcontext.RequestDatas.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestData> DeleteRequestData(RequestData entity)
        {
           if (entity != null)
            {
                _DBcontext.RequestDatas.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
