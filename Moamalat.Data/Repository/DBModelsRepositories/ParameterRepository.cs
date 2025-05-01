using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class ParameterRepository : IRepository<Parameter>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public ParameterRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Parameter
        //*****************************************************************************
        public async Task<IEnumerable<Parameter>> GetAllParameters()
        {
            return await _DBcontext.Parameters.ToListAsync();
        }
        public async Task<PaginationResult<Parameter>> GetPagedParameters(PaginationResult<Parameter> _Pagination)
        {
            PaginationResult<Parameter> paginationResult = _Pagination == null ? new PaginationResult<Parameter>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Parameter, bool> Filter = Extensions.CompileExpression<Parameter>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Parameters
                                                   .Where(Extensions.StringToExpression<Parameter>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Parameters;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Parameter>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Parameter> AddParameter(Parameter entity)
        {
            _DBcontext.Parameters.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Parameter> GetParameterById(int ParameterId)
        {
            return await _DBcontext.Parameters.FirstOrDefaultAsync(b => b.ParameterId == ParameterId);
        }
        public async Task<Parameter> UpdateParameter(Parameter entity)
        {
            _DBcontext.Parameters.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Parameter> DeleteParameter(Parameter entity)
        {
           if (entity != null)
            {
                _DBcontext.Parameters.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
