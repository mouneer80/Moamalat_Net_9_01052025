using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class ServiceDataRepository : IRepository<ServiceData>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public ServiceDataRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  ServiceData
        //*****************************************************************************
        public async Task<IEnumerable<ServiceData>> GetAllServiceDatas()
        {
            return await _DBcontext.ServiceDatas.ToListAsync();
        }
        public async Task<PaginationResult<ServiceData>> GetPagedServiceDatas(PaginationResult<ServiceData> _Pagination)
        {
            PaginationResult<ServiceData> paginationResult = _Pagination == null ? new PaginationResult<ServiceData>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<ServiceData, bool> Filter = Extensions.CompileExpression<ServiceData>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.ServiceDatas
                                                   .Where(Extensions.StringToExpression<ServiceData>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .ServiceDatas;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<ServiceData>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<ServiceData> AddServiceData(ServiceData entity)
        {
            _DBcontext.ServiceDatas.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceData> GetServiceDataById(int ServiceId)
        {
            return await _DBcontext.ServiceDatas.FirstOrDefaultAsync(b => b.ServiceId == ServiceId);
        }
        public async Task<ServiceData> UpdateServiceData(ServiceData entity)
        {
            _DBcontext.ServiceDatas.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceData> DeleteServiceData(ServiceData entity)
        {
           if (entity != null)
            {
                _DBcontext.ServiceDatas.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<List<ServiceParameter>> GetServiceParameterByServiceId(int ServiceId)
        {

            var parameters = await _DBcontext.ServiceParameters
                                   .Where(b => b.ServiceId == ServiceId)
                                   .Include(a => a.Parameter).ToListAsync();

            return parameters;
        }
        public async Task<List<ServiceData>> GetByCategoryId(int ServiceCategoryId)
        {
            var services = await _DBcontext.ServiceDatas
                                         .Where(s => s.ServiceCategoryId == ServiceCategoryId)
                                         .Include(c => c.ServiceCategory)
                                         .ToListAsync();
            return services;
        }
        public async Task<List<ServiceRequiredDocument>> GetSericeRequiredDocumentByServiceId(int ServiceId)
        {
            var documents = await _DBcontext.ServiceRequiredDocuments
                                   .Where(b => b.ServiceId == ServiceId)
                                   .Include(a => a.DocumentType)
                                   .ToListAsync();

            return documents;
        }

        public async Task<bool> SaveServiceRequest(RequestData requestData)//,List<RequestDetail> requestDetails, List<RequestDocument> requestDocuments)
        {
            _DBcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var Transaction = _DBcontext.Database.BeginTransaction();


            try
            {


                await _DBcontext.RequestDatas.AddAsync(requestData);

                //await _DBcontext.SaveChangesAsync();

                //await _DBcontext.RequestDocuments.AddRangeAsync(requestDocuments);    

                await _DBcontext.SaveChangesAsync();

                Transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return false;
                //throw;
            }
        }

        //End repository
    }
}
