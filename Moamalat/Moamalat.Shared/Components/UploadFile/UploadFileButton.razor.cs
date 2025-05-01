using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Components
{
    public partial class UploadFileButton :LocalizationComponent
    {
        [CascadingParameter]
        private UploadFileComponent _UploadFileComponent { get; set; }

        List<IBrowserFile>? selectedFiles;
        // public EventCallback<List<IBrowserFile>> OnFilesChosen { get; set; }
        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles().ToList();
            /*
             image/jpeg
             image/gif
             image/png
             image/svg+xml
             application/pdf
             */


            await _UploadFileComponent.AddFiles(selectedFiles.ToList());
            StateHasChanged();
        }

    }
}
