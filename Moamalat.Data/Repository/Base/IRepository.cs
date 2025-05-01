using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moamalat.Entities;
//
namespace Moamalat.Data
{
    public interface IRepository<T> where T : class, IEntity
    {
       

        // public object GetKey(T entity);
        //Task<IEnumerable<T>> GetAll();
       // Task<IEnumerable<T>> GetByField(string field,string value);
        //Task<T> Get(decimal id);
        //Task<T> Add(T entity);
        //Task<T> Update(T entity);
        //Task<T> Delete(decimal id);

       // Task<IEnumerable<T>> GetPaged(DataTableParameter pDataTableParameter);

       // Task<IEnumerable<T>> RunSQL(string SQLCommand);

    }

   
}
