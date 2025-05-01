using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class VwRequestDataRepository : IRepository<VwRequestData>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public VwRequestDataRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  VwRequestData
        //*****************************************************************************
        public async Task<IEnumerable<VwRequestData>> GetAllVwRequestDatas()
        {
            return await _DBcontext.VwRequestDatas.ToListAsync();
        }
        public async Task<PaginationResult<VwRequestData>> GetPagedVwRequestDatas(PaginationResult<VwRequestData> _Pagination)
        {
            PaginationResult<VwRequestData> paginationResult = _Pagination == null ? new PaginationResult<VwRequestData>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<VwRequestData, bool> Filter = Extensions.CompileExpression<VwRequestData>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.VwRequestDatas
                                                   .Where(Extensions.StringToExpression<VwRequestData>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .VwRequestDatas;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<VwRequestData>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<VwRequestData> AddVwRequestData(VwRequestData entity)
        {
            _DBcontext.VwRequestDatas.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<VwRequestData> GetVwRequestDataById(int RequestId)
        {
            return await _DBcontext.VwRequestDatas.FirstOrDefaultAsync(b => b.RequestId == RequestId);
        }
        public async Task<VwRequestData> UpdateVwRequestData(VwRequestData entity)
        {
            _DBcontext.VwRequestDatas.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<VwRequestData> DeleteVwRequestData(VwRequestData entity)
        {
           if (entity != null)
            {
                _DBcontext.VwRequestDatas.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
