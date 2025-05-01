using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamalat.Entities
{
    public class UploadFileData
    {
        public string? FileUrl { get; set; }
        public byte[]? FileContent { get; set; }
        public int? TypeId { get; set; }
        public int? SortIndex { get; set; }
        public string? ContentType { get; set; }
        public int? FileId { get; set; }
        public string? FileExtn { get; set; }
        public long? FileSize { get; set; }


    }
    public class FilePreview
    {
        public int? FileId { get; set; }
        public string? Base64 { get; set; }
        public long? Size { get; set; }
        public bool FileSizeExceeds { get; set; } = false;

    }
    
}
