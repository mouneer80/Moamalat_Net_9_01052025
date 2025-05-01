using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class DocumentRepository : IRepository<Document>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public DocumentRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  Document
        //*****************************************************************************
        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            return await _DBcontext.Documents.ToListAsync();
        }
        public async Task<PaginationResult<Document>> GetPagedDocuments(PaginationResult<Document> _Pagination)
        {
            PaginationResult<Document> paginationResult = _Pagination == null ? new PaginationResult<Document>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<Document, bool> Filter = Extensions.CompileExpression<Document>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.Documents
                                                   .Where(Extensions.StringToExpression<Document>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .Documents;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<Document>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<Document> AddDocument(Document entity)
        {
            _DBcontext.Documents.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Document> GetDocumentById(int DocumentId)
        {
            return await _DBcontext.Documents.FirstOrDefaultAsync(b => b.DocumentId == DocumentId);
        }
        public async Task<Document> UpdateDocument(Document entity)
        {
            _DBcontext.Documents.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<Document> DeleteDocument(Document entity)
        {
           if (entity != null)
            {
                _DBcontext.Documents.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
