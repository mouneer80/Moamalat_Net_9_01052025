using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class ServiceParameterRepository : IRepository<ServiceParameter>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public ServiceParameterRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  ServiceParameter
        //*****************************************************************************
        public async Task<IEnumerable<ServiceParameter>> GetAllServiceParameters()
        {
            return await _DBcontext.ServiceParameters.ToListAsync();
        }
        public async Task<PaginationResult<ServiceParameter>> GetPagedServiceParameters(PaginationResult<ServiceParameter> _Pagination)
        {
            PaginationResult<ServiceParameter> paginationResult = _Pagination == null ? new PaginationResult<ServiceParameter>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<ServiceParameter, bool> Filter = Extensions.CompileExpression<ServiceParameter>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.ServiceParameters
                                                   .Where(Extensions.StringToExpression<ServiceParameter>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .ServiceParameters;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<ServiceParameter>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<ServiceParameter> AddServiceParameter(ServiceParameter entity)
        {
            _DBcontext.ServiceParameters.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceParameter> GetServiceParameterById(int ServiceParameterId)
        {
            return await _DBcontext.ServiceParameters.FirstOrDefaultAsync(b => b.ServiceParameterId == ServiceParameterId);
        }
        public async Task<ServiceParameter> UpdateServiceParameter(ServiceParameter entity)
        {
            _DBcontext.ServiceParameters.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceParameter> DeleteServiceParameter(ServiceParameter entity)
        {
           if (entity != null)
            {
                _DBcontext.ServiceParameters.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
