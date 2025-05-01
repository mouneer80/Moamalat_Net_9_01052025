using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class ServiceCategoryRepository : IRepository<ServiceCategory>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public ServiceCategoryRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  ServiceCategory
        //*****************************************************************************
        public async Task<IEnumerable<ServiceCategory>> GetAllServiceCategories()
        {
            return await _DBcontext.ServiceCategories.ToListAsync();
        }
        public async Task<PaginationResult<ServiceCategory>> GetPagedServiceCategories(PaginationResult<ServiceCategory> _Pagination)
        {
            PaginationResult<ServiceCategory> paginationResult = _Pagination == null ? new PaginationResult<ServiceCategory>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<ServiceCategory, bool> Filter = Extensions.CompileExpression<ServiceCategory>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.ServiceCategories
                                                   .Where(Extensions.StringToExpression<ServiceCategory>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .ServiceCategories;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<ServiceCategory>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<ServiceCategory> AddServiceCategory(ServiceCategory entity)
        {
            _DBcontext.ServiceCategories.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceCategory> GetServiceCategoryById(int ServiceCategoryId)
        {
            return await _DBcontext.ServiceCategories.FirstOrDefaultAsync(b => b.ServiceCategoryId == ServiceCategoryId);
        }
        public async Task<ServiceCategory> UpdateServiceCategory(ServiceCategory entity)
        {
            _DBcontext.ServiceCategories.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceCategory> DeleteServiceCategory(ServiceCategory entity)
        {
           if (entity != null)
            {
                _DBcontext.ServiceCategories.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
