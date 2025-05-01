using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class ServiceRequiredDocumentRepository : IRepository<ServiceRequiredDocument>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public ServiceRequiredDocumentRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  ServiceRequiredDocument
        //*****************************************************************************
        public async Task<IEnumerable<ServiceRequiredDocument>> GetAllServiceRequiredDocuments()
        {
            return await _DBcontext.ServiceRequiredDocuments.ToListAsync();
        }
        public async Task<PaginationResult<ServiceRequiredDocument>> GetPagedServiceRequiredDocuments(PaginationResult<ServiceRequiredDocument> _Pagination)
        {
            PaginationResult<ServiceRequiredDocument> paginationResult = _Pagination == null ? new PaginationResult<ServiceRequiredDocument>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<ServiceRequiredDocument, bool> Filter = Extensions.CompileExpression<ServiceRequiredDocument>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.ServiceRequiredDocuments
                                                   .Where(Extensions.StringToExpression<ServiceRequiredDocument>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .ServiceRequiredDocuments;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<ServiceRequiredDocument>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<ServiceRequiredDocument> AddServiceRequiredDocument(ServiceRequiredDocument entity)
        {
            _DBcontext.ServiceRequiredDocuments.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceRequiredDocument> GetServiceRequiredDocumentById(int RequiredDocumentId)
        {
            return await _DBcontext.ServiceRequiredDocuments.FirstOrDefaultAsync(b => b.RequiredDocumentId == RequiredDocumentId);
        }
        public async Task<ServiceRequiredDocument> UpdateServiceRequiredDocument(ServiceRequiredDocument entity)
        {
            _DBcontext.ServiceRequiredDocuments.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<ServiceRequiredDocument> DeleteServiceRequiredDocument(ServiceRequiredDocument entity)
        {
           if (entity != null)
            {
                _DBcontext.ServiceRequiredDocuments.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
