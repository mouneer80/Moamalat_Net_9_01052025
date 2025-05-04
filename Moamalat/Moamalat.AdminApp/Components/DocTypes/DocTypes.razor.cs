using Microsoft.AspNetCore.Components;
using Moamalat.Entities;
using Moamalat.Services;

namespace Moamalat.AdminApp.Components.DocTypes
{
    public partial class DocTypes : ComponentBase
    {
        [Inject]
        private DocumentTypeService _DocTypeService { get; set; }

        private IQueryable<DocumentType> DocTypesList;


        protected override async Task OnInitializedAsync()
        {

            DocTypesList = (await _DocTypeService.GetAllDocumentTypes()).AsQueryable();
            // return base.OnInitializedAsync();
            StateHasChanged();
        }
        private void EditItem(int id)
        {
            // Handle edit logic here
        }

        private void DeleteItem(int id)
        {
            // Handle delete logic here
        }

        private void CreateNew()
        {
            // Navigate to Create page or show a modal for creating a new item
        }

        public bool alertisShown { get; set; }
        public string sResponse { get; set; }
        private void SaveButtonClicked()
        {
            alertisShown = true;
            //StateHasChanged();
        }
        private async Task GetAlertResponse(string value)
        {
            sResponse = value;
            alertisShown = false;

        }
    }
}
