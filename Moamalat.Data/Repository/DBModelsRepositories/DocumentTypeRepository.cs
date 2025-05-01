using Moamalat.Entities;
using Microsoft.EntityFrameworkCore;
namespace Moamalat.Data
{
    public class DocumentTypeRepository : IRepository<DocumentType>
    {
        private readonly MoamalatDb_Context _DBcontext;
        public DocumentTypeRepository(MoamalatDb_Context _Context)
        {
            _DBcontext = _Context;
        }

        //*****************************************************************************
        //  DocumentType
        //*****************************************************************************
        public async Task<IEnumerable<DocumentType>> GetAllDocumentTypes()
        {
            return await _DBcontext.DocumentTypes.ToListAsync();
        }
        public async Task<PaginationResult<DocumentType>> GetPagedDocumentTypes(PaginationResult<DocumentType> _Pagination)
        {
            PaginationResult<DocumentType> paginationResult = _Pagination == null ? new PaginationResult<DocumentType>() : _Pagination;
            paginationResult.PageNumber = _Pagination.PageNumber;
            paginationResult.PageSize = _Pagination.PageSize;


            if (!string.IsNullOrEmpty(_Pagination.FilterExpression))
            {

                //Func<DocumentType, bool> Filter = Extensions.CompileExpression<DocumentType>(_Pagination.FilterExpression);

                paginationResult.Items = _DBcontext.DocumentTypes
                                                   .Where(Extensions.StringToExpression<DocumentType>(_Pagination.FilterExpression));
            }
            else
                paginationResult.Items = _DBcontext
                                                .DocumentTypes;
            if (!paginationResult.Items.Any())
                paginationResult.Items = Enumerable.Empty<DocumentType>();
            paginationResult.RecordsCount = paginationResult.Items.Count();

            if (_Pagination.PageSize != -1)
            {

                paginationResult.Items = paginationResult.Items
                       .Skip(_Pagination.PageSize * (_Pagination.PageNumber - 1))
                       .Take(_Pagination.PageSize);
            }
            return paginationResult;


        }


        public async Task<DocumentType> AddDocumentType(DocumentType entity)
        {
            _DBcontext.DocumentTypes.Add(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<DocumentType> GetDocumentTypeById(int DocumentTypeId)
        {
            return await _DBcontext.DocumentTypes.FirstOrDefaultAsync(b => b.DocumentTypeId == DocumentTypeId);
        }
        public async Task<DocumentType> UpdateDocumentType(DocumentType entity)
        {
            _DBcontext.DocumentTypes.Update(entity);
            await _DBcontext.SaveChangesAsync();
            return entity;
        }
        public async Task<DocumentType> DeleteDocumentType(DocumentType entity)
        {
           if (entity != null)
            {
                _DBcontext.DocumentTypes.Remove(entity);
                await _DBcontext.SaveChangesAsync();
            }
            return entity;
        }
       
       
	   //End repository
    }
}
