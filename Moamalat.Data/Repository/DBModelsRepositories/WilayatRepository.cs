using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class WilayatRepository : IRepository<Wilayat>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public WilayatRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Wilayat
        //*****************************************************************************
        public async Task<IEnumerable<Wilayat>> GetAllWilayats()
        {
            return await _DBcontext.Wilayats.ToListAsync();
        }
        public async Task<PaginationResult<Wilayat>> GetPagedWilayats(PaginationResult<Wilayat> _Pagination)
        {
            PaginationResult<Wilayat> paginationResult = _Pagination == null ? new PaginationResult<Wilayat>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Wilayat, bool> Filter = Extensions.CompileExpression<Wilayat>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Wilayats
                                                   .Where(Extensions.StringToExpression<Wilayat>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Wilayats;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Wilayat>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Wilayat> AddWilayat(Wilayat entity)
        {
            _DBcontext.Wilayats.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Wilayat> GetWilayatById(int WilayatId)
        {
            return await _DBcontext.Wilayats.FirstOrDefaultAsync(b => b.WilayatId == WilayatId);
        }
        public async Task<Wilayat> UpdateWilayat(Wilayat entity)
        {
            _DBcontext.Wilayats.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Wilayat> DeleteWilayat(Wilayat entity)
        {
           if (entity != null)
            {
                _DBcontext.Wilayats.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
