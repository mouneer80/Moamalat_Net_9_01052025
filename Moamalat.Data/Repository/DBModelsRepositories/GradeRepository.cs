using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class GradeRepository : IRepository<Grade>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public GradeRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Grade
        //*****************************************************************************
        public async Task<IEnumerable<Grade>> GetAllGrades()
        {
            return await _DBcontext.Grades.ToListAsync();
        }
        public async Task<PaginationResult<Grade>> GetPagedGrades(PaginationResult<Grade> _Pagination)
        {
            PaginationResult<Grade> paginationResult = _Pagination == null ? new PaginationResult<Grade>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Grade, bool> Filter = Extensions.CompileExpression<Grade>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Grades
                                                   .Where(Extensions.StringToExpression<Grade>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Grades;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Grade>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Grade> AddGrade(Grade entity)
        {
            _DBcontext.Grades.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Grade> GetGradeById(int GradeId)
        {
            return await _DBcontext.Grades.FirstOrDefaultAsync(b => b.GradeId == GradeId);
        }
        public async Task<Grade> UpdateGrade(Grade entity)
        {
            _DBcontext.Grades.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Grade> DeleteGrade(Grade entity)
        {
           if (entity != null)
            {
                _DBcontext.Grades.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
