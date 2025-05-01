using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class RequestStatuRepository : IRepository<RequestStatu>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public RequestStatuRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  RequestStatu
        //*****************************************************************************
        public async Task<IEnumerable<RequestStatu>> GetAllRequestStatus()
        {
            return await _DBcontext.RequestStatus.ToListAsync();
        }
        public async Task<PaginationResult<RequestStatu>> GetPagedRequestStatus(PaginationResult<RequestStatu> _Pagination)
        {
            PaginationResult<RequestStatu> paginationResult = _Pagination == null ? new PaginationResult<RequestStatu>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<RequestStatu, bool> Filter = Extensions.CompileExpression<RequestStatu>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.RequestStatus
                                                   .Where(Extensions.StringToExpression<RequestStatu>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .RequestStatus;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<RequestStatu>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<RequestStatu> AddRequestStatu(RequestStatu entity)
        {
            _DBcontext.RequestStatus.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestStatu> GetRequestStatuById(int StatusId)
        {
            return await _DBcontext.RequestStatus.FirstOrDefaultAsync(b => b.StatusId == StatusId);
        }
        public async Task<RequestStatu> UpdateRequestStatu(RequestStatu entity)
        {
            _DBcontext.RequestStatus.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestStatu> DeleteRequestStatu(RequestStatu entity)
        {
           if (entity != null)
            {
                _DBcontext.RequestStatus.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
