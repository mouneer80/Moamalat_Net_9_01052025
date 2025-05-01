using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

using Moamalat.Entities;

namespace Moamalat.Components
{
    public partial class UploadFileComponent : LocalizationComponent
    {

        [Parameter]
        public string? Title { get; set; }
        [Parameter]
        public bool? isValid { get; set; }
        [Parameter]
        public DocumentType? FileType { get; set; }
        [Parameter]
        public int MaxFileCount { get; set; }
        [Parameter]
        public int MaxAllowedSize { get; set; } = 512000;

        [Parameter]
        public string Id { get; set; } = Guid.NewGuid().ToString("D");
        [Parameter]
        public EventCallback<List<UploadFileData>> OnUploadFileChanged { get; set; }

        [Parameter]
        public EventCallback<UploadFileData> OnFileDelete { get; set; }
        [Parameter]
        public List<string>? ConfirmDeleteMessage { get; set; }
        [Parameter]
        public EventCallback OnFileSizeExceeds { get; set; }

        [Parameter]
        public EventCallback Initialized { get; set; }
        [Parameter]
        public bool isMultipleFiles { get; set; } = true;
        [Parameter]
        public List<string> SupportedFiles { get; set; }

        List<IBrowserFile> selectedFiles;
        [Parameter]
        public List<UploadFileData> files { get; set; }

        private UploadFileData SelectedFile { get; set; }
        public bool isDeleteShow { get; set; } = false;

        // private List<DomFile>? RequiredDomFiles { get; set; }

        public async Task AddFiles(List<IBrowserFile> _Files)
        {
            if (selectedFiles != null)
                selectedFiles.AddRange(_Files);
            else
                selectedFiles = _Files;

            if (files == null)
                files = new();

            if (_Files.Any(f => f.Size > MaxAllowedSize))
            {
                await OnFileSizeExceeds.InvokeAsync();
            }
            else
            {
                foreach (var item in _Files)
                {
                    Stream stream = item.OpenReadStream(MaxAllowedSize);
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    string FileExtn = System.IO.Path.GetExtension(item.Name);
                    if (FileExtn.StartsWith('.'))
                        FileExtn = FileExtn.Remove(0, 1);

                    UploadFileData file = new UploadFileData
                    {
                        ContentType = item.ContentType,
                        FileUrl = Guid.NewGuid().ToString() + "." + FileExtn,
                        FileContent = ms.ToArray(),
                        TypeId = FileType.DocumentTypeId,
                        FileId = 0,
                        FileExtn = FileExtn,
                        FileSize = item.Size
                    };
                    files.Add(file);

                }
                await OnUploadFileChanged.InvokeAsync(files);
            }

            StateHasChanged();

        }


        private async Task OnHideDeleteConfirm(string response)
        {

            if (isDeleteShow)
            {
                if (response == "Confirm")
                {
                    isDeleteShow = false;
                    files.Remove(SelectedFile);
                    StateHasChanged();
                    //if (SelectedFile.FileId > 0)
                    await OnFileDelete.InvokeAsync(SelectedFile);

                }


            }

            isDeleteShow = false;

        }
        private void HandleFileDeleted(UploadFileData deletedFile)
        {
            isDeleteShow = true;
            SelectedFile = deletedFile;
        }

        //protected override async Task OnInitializedAsync()
        //{


        //    await base.OnInitializedAsync();
        //    await Initialized.InvokeAsync();
           

        //}
       

    }

}
