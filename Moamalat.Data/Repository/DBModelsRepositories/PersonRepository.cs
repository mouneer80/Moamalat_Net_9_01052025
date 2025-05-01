using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public PersonRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Person
        //*****************************************************************************
        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return await _DBcontext.Persons.ToListAsync();
        }
        public async Task<PaginationResult<Person>> GetPagedPersons(PaginationResult<Person> _Pagination)
        {
            PaginationResult<Person> paginationResult = _Pagination == null ? new PaginationResult<Person>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Person, bool> Filter = Extensions.CompileExpression<Person>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Persons
                                                   .Where(Extensions.StringToExpression<Person>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Persons;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Person>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Person> AddPerson(Person entity)
        {
            _DBcontext.Persons.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Person> GetPersonById(int PersonId)
        {
            return await _DBcontext.Persons.FirstOrDefaultAsync(b => b.PersonId == PersonId);
        }
        public async Task<Person> UpdatePerson(Person entity)
        {
            _DBcontext.Persons.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Person> DeletePerson(Person entity)
        {
           if (entity != null)
            {
                _DBcontext.Persons.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
