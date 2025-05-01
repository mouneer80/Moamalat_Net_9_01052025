using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("RequestDocument")]
public partial class RequestDocument : IEntity
{
    [Key]
    public int RequestDocumentId { get; set; }

    public int RequestId { get; set; }

    public int RequiredDocumentId { get; set; }

    public int? DocumentId { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "numeric(12, 0)")]
    public decimal? CreationDate { get; set; }

    /// <summary>
    /// A active D Inactive
    /// </summary>
    [StringLength(1)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("DocumentId")]
    [InverseProperty("RequestDocuments")]
    public virtual Document? Document { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestDocuments")]
    public virtual RequestData Request { get; set; } = null!;
}

