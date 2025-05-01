using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class CorporateRepository : IRepository<Corporate>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public CorporateRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Corporate
        //*****************************************************************************
        public async Task<IEnumerable<Corporate>> GetAllCorporates()
        {
            return await _DBcontext.Corporates.ToListAsync();
        }
        public async Task<PaginationResult<Corporate>> GetPagedCorporates(PaginationResult<Corporate> _Pagination)
        {
            PaginationResult<Corporate> paginationResult = _Pagination == null ? new PaginationResult<Corporate>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Corporate, bool> Filter = Extensions.CompileExpression<Corporate>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Corporates
                                                   .Where(Extensions.StringToExpression<Corporate>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Corporates;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Corporate>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Corporate> AddCorporate(Corporate entity)
        {
            _DBcontext.Corporates.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Corporate> GetCorporateById(int CorporateId)
        {
            return await _DBcontext.Corporates.FirstOrDefaultAsync(b => b.CorporateId == CorporateId);
        }
        public async Task<Corporate> UpdateCorporate(Corporate entity)
        {
            _DBcontext.Corporates.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Corporate> DeleteCorporate(Corporate entity)
        {
           if (entity != null)
            {
                _DBcontext.Corporates.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
