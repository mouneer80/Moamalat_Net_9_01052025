using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public AddressRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Address
        //*****************************************************************************
        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await _DBcontext.Addresses.ToListAsync();
        }
        public async Task<PaginationResult<Address>> GetPagedAddresses(PaginationResult<Address> _Pagination)
        {
            PaginationResult<Address> paginationResult = _Pagination == null ? new PaginationResult<Address>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Address, bool> Filter = Extensions.CompileExpression<Address>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Addresses
                                                   .Where(Extensions.StringToExpression<Address>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Addresses;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Address>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Address> AddAddress(Address entity)
        {
            _DBcontext.Addresses.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Address> GetAddressById(string AddressId)
        {
            return await _DBcontext.Addresses.FirstOrDefaultAsync(b => b.AddressId == AddressId);
        }
        public async Task<Address> UpdateAddress(Address entity)
        {
            _DBcontext.Addresses.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Address> DeleteAddress(Address entity)
        {
           if (entity != null)
            {
                _DBcontext.Addresses.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
