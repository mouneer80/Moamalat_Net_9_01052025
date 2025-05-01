using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class PersonCorporateRepository : IRepository<PersonCorporate>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public PersonCorporateRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  PersonCorporate
        //*****************************************************************************
        public async Task<IEnumerable<PersonCorporate>> GetAllPersonCorporates()
        {
            return await _DBcontext.PersonCorporates.ToListAsync();
        }
        public async Task<PaginationResult<PersonCorporate>> GetPagedPersonCorporates(PaginationResult<PersonCorporate> _Pagination)
        {
            PaginationResult<PersonCorporate> paginationResult = _Pagination == null ? new PaginationResult<PersonCorporate>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<PersonCorporate, bool> Filter = Extensions.CompileExpression<PersonCorporate>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.PersonCorporates
                                                   .Where(Extensions.StringToExpression<PersonCorporate>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .PersonCorporates;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<PersonCorporate>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<PersonCorporate> AddPersonCorporate(PersonCorporate entity)
        {
            _DBcontext.PersonCorporates.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<PersonCorporate> GetPersonCorporateById(int PersonCorporateId)
        {
            return await _DBcontext.PersonCorporates.FirstOrDefaultAsync(b => b.PersonCorporateId == PersonCorporateId);
        }
        public async Task<PersonCorporate> UpdatePersonCorporate(PersonCorporate entity)
        {
            _DBcontext.PersonCorporates.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<PersonCorporate> DeletePersonCorporate(PersonCorporate entity)
        {
           if (entity != null)
            {
                _DBcontext.PersonCorporates.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
