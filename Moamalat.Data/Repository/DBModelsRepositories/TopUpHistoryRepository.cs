using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class TopUpHistoryRepository : IRepository<TopUpHistory>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public TopUpHistoryRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  TopUpHistory
        //*****************************************************************************
        public async Task<IEnumerable<TopUpHistory>> GetAllTopUpHistories()
        {
            return await _DBcontext.TopUpHistories.ToListAsync();
        }
        public async Task<PaginationResult<TopUpHistory>> GetPagedTopUpHistories(PaginationResult<TopUpHistory> _Pagination)
        {
            PaginationResult<TopUpHistory> paginationResult = _Pagination == null ? new PaginationResult<TopUpHistory>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<TopUpHistory, bool> Filter = Extensions.CompileExpression<TopUpHistory>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.TopUpHistories
                                                   .Where(Extensions.StringToExpression<TopUpHistory>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .TopUpHistories;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<TopUpHistory>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<TopUpHistory> AddTopUpHistory(TopUpHistory entity)
        {
            _DBcontext.TopUpHistories.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<TopUpHistory> GetTopUpHistoryById(int TopupId)
        {
            return await _DBcontext.TopUpHistories.FirstOrDefaultAsync(b => b.TopupId == TopupId);
        }
        public async Task<TopUpHistory> UpdateTopUpHistory(TopUpHistory entity)
        {
            _DBcontext.TopUpHistories.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<TopUpHistory> DeleteTopUpHistory(TopUpHistory entity)
        {
           if (entity != null)
            {
                _DBcontext.TopUpHistories.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
