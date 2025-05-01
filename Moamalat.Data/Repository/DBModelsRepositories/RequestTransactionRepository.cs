using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class RequestTransactionRepository : IRepository<RequestTransaction>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public RequestTransactionRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  RequestTransaction
        //*****************************************************************************
        public async Task<IEnumerable<RequestTransaction>> GetAllRequestTransactions()
        {
            return await _DBcontext.RequestTransactions.ToListAsync();
        }
        public async Task<PaginationResult<RequestTransaction>> GetPagedRequestTransactions(PaginationResult<RequestTransaction> _Pagination)
        {
            PaginationResult<RequestTransaction> paginationResult = _Pagination == null ? new PaginationResult<RequestTransaction>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<RequestTransaction, bool> Filter = Extensions.CompileExpression<RequestTransaction>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.RequestTransactions
                                                   .Where(Extensions.StringToExpression<RequestTransaction>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .RequestTransactions;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<RequestTransaction>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<RequestTransaction> AddRequestTransaction(RequestTransaction entity)
        {
            _DBcontext.RequestTransactions.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestTransaction> GetRequestTransactionById(int TransactionId)
        {
            return await _DBcontext.RequestTransactions.FirstOrDefaultAsync(b => b.TransactionId == TransactionId);
        }
        public async Task<RequestTransaction> UpdateRequestTransaction(RequestTransaction entity)
        {
            _DBcontext.RequestTransactions.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestTransaction> DeleteRequestTransaction(RequestTransaction entity)
        {
           if (entity != null)
            {
                _DBcontext.RequestTransactions.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
