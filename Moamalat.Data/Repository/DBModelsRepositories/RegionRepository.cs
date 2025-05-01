using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class RegionRepository : IRepository<Region>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public RegionRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Region
        //*****************************************************************************
        public async Task<IEnumerable<Region>> GetAllRegions()
        {
            return await _DBcontext.Regions.ToListAsync();
        }
        public async Task<PaginationResult<Region>> GetPagedRegions(PaginationResult<Region> _Pagination)
        {
            PaginationResult<Region> paginationResult = _Pagination == null ? new PaginationResult<Region>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Region, bool> Filter = Extensions.CompileExpression<Region>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Regions
                                                   .Where(Extensions.StringToExpression<Region>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Regions;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Region>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Region> AddRegion(Region entity)
        {
            _DBcontext.Regions.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Region> GetRegionById(int RegionId)
        {
            return await _DBcontext.Regions.FirstOrDefaultAsync(b => b.RegionId == RegionId);
        }
        public async Task<Region> UpdateRegion(Region entity)
        {
            _DBcontext.Regions.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Region> DeleteRegion(Region entity)
        {
           if (entity != null)
            {
                _DBcontext.Regions.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
