using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("DocumentType")]
public partial class DocumentType : IEntity
{
    [Key]
    public int DocumentTypeId { get; set; }

    [StringLength(50)]
    public string? DocumentTypeArName { get; set; }

    [StringLength(50)]
    public string? DocumentTypeEnName { get; set; }

    [InverseProperty("DocumentType")]
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    [InverseProperty("DocumentType")]
    public virtual ICollection<ServiceRequiredDocument> ServiceRequiredDocuments { get; set; } = new List<ServiceRequiredDocument>();
}

