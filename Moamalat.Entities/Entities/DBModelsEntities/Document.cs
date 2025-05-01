using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Document")]
public partial class Document : IEntity
{
    [Key]
    public int DocumentId { get; set; }

    public int DocumentTypeId { get; set; }

    public int PersonId { get; set; }

    public int? CorporateId { get; set; }

    [StringLength(50)]
    public string? DocumentUrl { get; set; }

    /// <summary>
    /// A active D Inactive
    /// </summary>
    [StringLength(1)]
    [Unicode(false)]
    public string? Status { get; set; }

    [ForeignKey("DocumentTypeId")]
    [InverseProperty("Documents")]
    public virtual DocumentType DocumentType { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("Documents")]
    public virtual Person Person { get; set; } = null!;

    [InverseProperty("Document")]
    public virtual ICollection<RequestDocument> RequestDocuments { get; set; } = new List<RequestDocument>();
}

