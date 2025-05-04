using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Moamalat.Entities;
using Moamalat.Services;

namespace Moamalat.AdminApp.Components.DocTypes
{
    public partial class AddDocType : ComponentBase
    {
        [Inject]
        private DocumentTypeService _DocTypeService { get; set; }

        [Inject]
        private NavigationManager _NavigationManager { get; set; }

        [Parameter] public string DocTypeId { get; set; }

        public DocumentType docTypeModel { get; set; } = new DocumentType();

        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(DocTypeId))
            {
                //var _docTypeId = Utilities.Encrypt(DocTypeId);
                var DocType = await _DocTypeService.GetDocumentTypeById(int.Parse(DocTypeId));

                if (DocType != null)
                    docTypeModel = DocType as DocumentType;
            }
            //StateHasChanged();
        }

        protected void InvalidFormSubmitted(EditContext editContext)
        {
            Message = "Something went wrong.....";
        }

        protected async Task ValidFormSubmitted()
        {
            if (string.IsNullOrEmpty(DocTypeId))
            {
                var newDocType = new DocumentType()
                {
                    DocumentTypeEnName = docTypeModel.DocumentTypeEnName,
                    DocumentTypeArName = docTypeModel.DocumentTypeArName,
                    DocumentTypeId = docTypeModel.DocumentTypeId
                };

                var result = await _DocTypeService.AddDocumentType(newDocType);

                if (result != null)
                    _NavigationManager.NavigateTo("/docTypes");
                else
                    Message = "Something went wrong, please try again later....";
            }
            else
            {
                var result = await _DocTypeService.UpdateDocumentType(docTypeModel);

                if (result != null)
                    _NavigationManager.NavigateTo("/docTypes");
                else
                    Message = "Something went wrong, please try again later....";
            }
            StateHasChanged();
        }

        protected async Task Delete()
        {
            if (!string.IsNullOrEmpty(DocTypeId))
            {
                //var result = await _DocTypeService.Delete(docTypeModel.DocumentTypeId);

                //if (result != null)
                //    _NavigationManager.NavigateTo("/docTypes");
                //else
                //    Message = "Something went wrong, please try again later....";

            }
        }
    }
}
