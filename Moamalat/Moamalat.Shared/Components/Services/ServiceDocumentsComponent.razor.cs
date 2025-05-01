using Microsoft.AspNetCore.Components;
using Moamalat.Entities;
using Moamalat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Components
{
    public partial class ServiceDocumentsComponent : LocalizationComponent
    {
        
        [Parameter]
        public EventCallback<object> OnAllDocumentsChosen { get; set; }
        [Parameter]
        public List<ServiceRequiredDocument>? Documents { get; set; }

        private void DocumentChosen(List<UploadFileData> SelectedFiles)
        {
            //foreach (var item in Documents.Where(a => a.DocumentTypeId == SelectedFiles.First().TypeId))
            //{
            var Items = Documents.Where(a => a.DocumentTypeId == SelectedFiles.First().TypeId).ToList();
            Items.ForEach(f=>f.DocumentFiles?.Clear());
            Items.ForEach(f => f.DocumentFiles= SelectedFiles);
            //};


            StateHasChanged();
        }
        protected async Task OnNextButtonClicked()
        {
            Documents.ForEach(a =>
            {
                a.isValid = a.DocumentFiles.Count>0;

            });

            if(Documents.Any(a=>!a.isValid))
            {

            }
            else
            {
                await OnAllDocumentsChosen.InvokeAsync(Documents);
            }
            
        }

       

    }
}
