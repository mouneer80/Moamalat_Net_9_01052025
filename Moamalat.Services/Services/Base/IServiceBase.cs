using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moamalat.Entities;
//using MillSoft.Core;
namespace Moamalat.Services
{
    public interface IServiceBase <T> 
    {
         Task<PaginationResult<T>> GetPaged(PaginationResult<T> _Pagination);
         Task<IEnumerable<T>> GetByField(string field, string value);
        
    }
}
