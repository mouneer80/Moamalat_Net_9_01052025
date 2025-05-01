using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class RequestDocumentRepository : IRepository<RequestDocument>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public RequestDocumentRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  RequestDocument
        //*****************************************************************************
        public async Task<IEnumerable<RequestDocument>> GetAllRequestDocuments()
        {
            return await _DBcontext.RequestDocuments.ToListAsync();
        }
        public async Task<PaginationResult<RequestDocument>> GetPagedRequestDocuments(PaginationResult<RequestDocument> _Pagination)
        {
            PaginationResult<RequestDocument> paginationResult = _Pagination == null ? new PaginationResult<RequestDocument>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<RequestDocument, bool> Filter = Extensions.CompileExpression<RequestDocument>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.RequestDocuments
                                                   .Where(Extensions.StringToExpression<RequestDocument>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .RequestDocuments;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<RequestDocument>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<RequestDocument> AddRequestDocument(RequestDocument entity)
        {
            _DBcontext.RequestDocuments.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestDocument> GetRequestDocumentById(int RequestDocumentId)
        {
            return await _DBcontext.RequestDocuments.FirstOrDefaultAsync(b => b.RequestDocumentId == RequestDocumentId);
        }
        public async Task<RequestDocument> UpdateRequestDocument(RequestDocument entity)
        {
            _DBcontext.RequestDocuments.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<RequestDocument> DeleteRequestDocument(RequestDocument entity)
        {
           if (entity != null)
            {
                _DBcontext.RequestDocuments.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
