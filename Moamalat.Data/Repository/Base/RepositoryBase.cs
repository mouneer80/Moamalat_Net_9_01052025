
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Moamalat.Entities;
//
using System.Reflection;

namespace Moamalat.Data
{
    
    public abstract class RepositoryBase<TEntity, TContext> : IRepository<TEntity>
          where TEntity : class, IEntity
          where TContext : DbContext
    {
        private readonly TContext context;
        private readonly DbSet<TEntity> dbSet;
        private string TableName;
        private string PkName;
        private string SQLQuery = ""; //"select t.*,count(1) over () as RecordsCount from ";

        public RepositoryBase(TContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();

            if (typeof(TEntity).Name.ToUpper().StartsWith("QUERY"))
            {
                TableName = typeof(TEntity).Name.Replace("Query", "");//context.Model.FindEntityType(typeof(TEntity)).GetSchema() + "." + typeof(TEntity).Name.Replace("Query", "");
            }
            else
            {
                TableName = typeof(TEntity).Name;//context.Model.FindEntityType(typeof(TEntity)).GetSchema() + "." + typeof(TEntity).Name;
                if (context.Model.FindEntityType(typeof(TEntity)) != null)
                    if (context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey() != null)
                        PkName = context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
                           .Select(x => x.Name).Single();

            }

            List<PropertyInfo> propertyInfos = typeof(TEntity).GetProperties().ToList();
            int icount = 0;
            SQLQuery = "Select ";
            foreach (var Info in propertyInfos)
            {
                if (icount == 0)
                    SQLQuery += Info.Name;
                else
                    SQLQuery += "," + Info.Name;
                icount++;
            }
            SQLQuery += " ,count(1) over () as RecordsCount from " + TableName;

        }

        public object GetKey(TEntity entity)
        {
            var keyName = context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
               .Select(x => x.Name).Single();

            return entity.GetType().GetProperty(keyName).GetValue(entity, null);
        }

        public string GetKeyName(TEntity entity)
        {
            var keyName = context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties
               .Select(x => x.Name).Single();

            return keyName;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            //context.Database.ExecuteSqlCommand("");
            //context.Add(entity);

            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        //public async void GetCodeById(decimal Id)
        //{
        //    var entity = await context.Set<TEntity>().FindAsync(Id);
            
        //}

        public async Task<TEntity> Delete(decimal id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        public async void GetId(decimal id)
        {
            List<TEntity> pEntityList = await dbSet.FromSqlRaw<TEntity>(SQLQuery + " t where t." + PkName + "=" + id).ToListAsync();
            //if (pEntityList.Count > 0)
           // return pEntityList.FirstOrDefault();
            // return Ba 
        }
        public async Task<TEntity> Get(decimal id)
        {
            List<TEntity> pEntityList = await dbSet.FromSqlRaw<TEntity>(SQLQuery + " t where t." + PkName + "=" + id).ToListAsync();
            //if (pEntityList.Count > 0)
            return pEntityList.FirstOrDefault();
            // return Ba 
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.FromSqlRaw<TEntity>(SQLQuery + " t ").ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        //public async Task<IEnumerable<TEntity>> GetPaged(DataTableParameter pDataTableParameter)
        //{
            
        //    //string SchemaName= context.Database.GetDbConnection().GetSchema().
        //    int Offset = pDataTableParameter.PageSize * (pDataTableParameter.PageNumber - 1);
        //    string SqlQuery = SQLQuery + " t " +
        //                (!string.IsNullOrEmpty(pDataTableParameter.WhereClause) ? " WHERE " + pDataTableParameter.WhereClause : "") +
        //                " ORDER BY " + (!string.IsNullOrEmpty(pDataTableParameter.SortingList) ? pDataTableParameter.SortingList : "t." + PkName) +
        //                (pDataTableParameter.PageSize > 0 ? " OFFSET " + Offset + " ROWS FETCH NEXT " + pDataTableParameter.PageSize + " ROWS ONLY" : "");


        //    return await dbSet.FromSqlRaw<TEntity>(SqlQuery).ToListAsync();

        //}

        public async Task<IEnumerable<TEntity>> RunSQL(string SQLCommand)
        {

            return await dbSet.FromSqlRaw<TEntity>(SQLCommand).ToListAsync();
        }

        public async Task<TEntity> GetByPK(object PKValue)
        {
            string SqlQuery = SQLQuery + TableName + $" t  WHERE t.{PkName}={PKValue}";

            return await dbSet.FromSqlRaw<TEntity>(SqlQuery).FirstOrDefaultAsync();

        }

        //public async Task<IEnumerable<TEntity>> GetByIndex(List<FieldParameter> IndexedFields)
        //{
        //    string WhereClause = "";
        //    for (int i = 0; i < IndexedFields.Count; i++)
        //    {
        //        if (i == 0)
        //            WhereClause = $"Where {IndexedFields[i].FieldName}={IndexedFields[i].FieldValue}";
        //        else
        //            WhereClause = $" AND {IndexedFields[i].FieldName}={IndexedFields[i].FieldValue}";
        //    }

        //    string SqlQuery = SQLQuery + TableName + $" t {WhereClause}";

        //    return await dbSet.FromSqlRaw<TEntity>(SqlQuery).ToListAsync();
        //}

        //public async Task<IEnumerable<TEntity>> GetByIndexPaged(DataTableParameter pDataTableParameter, List<FieldParameter> IndexedFields)
        //{
        //    string WhereClause = "";
        //    int Offset = pDataTableParameter.PageSize * (pDataTableParameter.PageNumber - 1);
        //    for (int i = 0; i < IndexedFields.Count; i++)
        //    {
        //        if (i == 0)
        //            WhereClause = $"Where {IndexedFields[i].FieldName}={IndexedFields[i].FieldValue}";
        //        else
        //            WhereClause = $" AND {IndexedFields[i].FieldName}={IndexedFields[i].FieldValue}";
        //    }
        //    string SqlQuery = SQLQuery + TableName + $" t {WhereClause}" +
        //                    " ORDER BY " + (!string.IsNullOrEmpty(pDataTableParameter.SortingList) ? pDataTableParameter.SortingList : "t." + PkName) +
        //                    (pDataTableParameter.PageSize > 0 ? " OFFSET " + Offset + " ROWS FETCH NEXT " + pDataTableParameter.PageSize + " ROWS ONLY" : "");

        //    return await dbSet.FromSqlRaw<TEntity>(SqlQuery).ToListAsync();
        //}

        public async Task<IEnumerable<TEntity>> GetByField(string field, string value)
        {
            string WhereClause = $"Where t.{field}='{value}'";
            string SqlQuery = SQLQuery + $" t {WhereClause}";

            return await dbSet.FromSqlRaw<TEntity>(SqlQuery).ToListAsync();
        }
    }
}
