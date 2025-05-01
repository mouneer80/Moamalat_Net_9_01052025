using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("ServiceRequiredDocument")]
public partial class ServiceRequiredDocument : IEntity
{
    [Key]
    public int RequiredDocumentId { get; set; }

    public int ServiceId { get; set; }

    public int DocumentTypeId { get; set; }

    [StringLength(150)]
    public string? DocumentDescription { get; set; }

    /// <summary>
    /// A Active D Inactive
    /// </summary>
    [StringLength(1)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("ServiceRequiredDocuments")]
    public virtual DocumentType DocumentType { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("ServiceRequiredDocuments")]
    public virtual ServiceData Service { get; set; } = null!;
    [NotMapped]
    public bool isValid { get; set; }
    [NotMapped]
    public string? DocumentUrl { get; set; }

    [NotMapped]
    public List<UploadFileData>? DocumentFiles { get; set; }
}

