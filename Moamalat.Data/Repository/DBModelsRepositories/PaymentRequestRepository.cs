using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class PaymentRequestRepository : IRepository<PaymentRequest>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public PaymentRequestRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  PaymentRequest
        //*****************************************************************************
        public async Task<IEnumerable<PaymentRequest>> GetAllPaymentRequests()
        {
            return await _DBcontext.PaymentRequests.ToListAsync();
        }
        public async Task<PaginationResult<PaymentRequest>> GetPagedPaymentRequests(PaginationResult<PaymentRequest> _Pagination)
        {
            PaginationResult<PaymentRequest> paginationResult = _Pagination == null ? new PaginationResult<PaymentRequest>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<PaymentRequest, bool> Filter = Extensions.CompileExpression<PaymentRequest>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.PaymentRequests
                                                   .Where(Extensions.StringToExpression<PaymentRequest>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .PaymentRequests;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<PaymentRequest>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<PaymentRequest> AddPaymentRequest(PaymentRequest entity)
        {
            try
            {
                _DBcontext.PaymentRequests.Add(entity);
                await _DBcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
          
            return entity;
        }
        public async Task<PaymentRequest> GetPaymentRequestById(int PaymentRequestId)
        {
            return await _DBcontext.PaymentRequests.FirstOrDefaultAsync(b => b.PaymentRequestId == PaymentRequestId);
        }
        public async Task<PaymentRequest> UpdatePaymentRequest(PaymentRequest entity)
        {
            _DBcontext.PaymentRequests.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<PaymentRequest> DeletePaymentRequest(PaymentRequest entity)
        {
           if (entity != null)
            {
                _DBcontext.PaymentRequests.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
