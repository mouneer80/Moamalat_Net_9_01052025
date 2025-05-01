using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class PaymentDataRepository : IRepository<PaymentData>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public PaymentDataRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  PaymentData
        //*****************************************************************************
        public async Task<IEnumerable<PaymentData>> GetAllPaymentDatas()
        {
            return await _DBcontext.PaymentDatas.ToListAsync();
        }
        public async Task<PaginationResult<PaymentData>> GetPagedPaymentDatas(PaginationResult<PaymentData> _Pagination)
        {
            PaginationResult<PaymentData> paginationResult = _Pagination == null ? new PaginationResult<PaymentData>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<PaymentData, bool> Filter = Extensions.CompileExpression<PaymentData>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.PaymentDatas
                                                   .Where(Extensions.StringToExpression<PaymentData>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .PaymentDatas;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<PaymentData>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<PaymentData> AddPaymentData(PaymentData entity)
        {
            _DBcontext.PaymentDatas.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<PaymentData> GetPaymentDataById(int PaymentId)
        {
            return await _DBcontext.PaymentDatas.FirstOrDefaultAsync(b => b.PaymentId == PaymentId);
        }
        public async Task<PaymentData> UpdatePaymentData(PaymentData entity)
        {
            _DBcontext.PaymentDatas.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<PaymentData> DeletePaymentData(PaymentData entity)
        {
           if (entity != null)
            {
                _DBcontext.PaymentDatas.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
