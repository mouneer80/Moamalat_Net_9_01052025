using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class RequestDetailRepository : IRepository<RequestDetail>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public RequestDetailRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  RequestDetail
        //*****************************************************************************
        public async Task<IEnumerable<RequestDetail>> GetAllRequestDetails()
        {
            return await _DBcontext.RequestDetails.ToListAsync();
        }
        public async Task<PaginationResult<RequestDetail>> GetPagedRequestDetails(PaginationResult<RequestDetail> _Pagination)
        {
            PaginationResult<RequestDetail> paginationResult = _Pagination == null ? new PaginationResult<RequestDetail>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<RequestDetail, bool> Filter = Extensions.CompileExpression<RequestDetail>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.RequestDetails
                                                   .Where(Extensions.StringToExpression<RequestDetail>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .RequestDetails;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<RequestDetail>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<RequestDetail> AddRequestDetail(RequestDetail entity)
        {
            _DBcontext.RequestDetails.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestDetail> GetRequestDetailById(int RequestDetailId)
        {
            return await _DBcontext.RequestDetails.FirstOrDefaultAsync(b => b.RequestDetailId == RequestDetailId);
        }
        public async Task<RequestDetail> UpdateRequestDetail(RequestDetail entity)
        {
            _DBcontext.RequestDetails.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestDetail> DeleteRequestDetail(RequestDetail entity)
        {
           if (entity != null)
            {
                _DBcontext.RequestDetails.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
