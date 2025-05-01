using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("RequestDetail")]
public partial class RequestDetail : IEntity
{
    [Key]
    public int RequestDetailId { get; set; }

    public int RequestId { get; set; }

    public int ParameterId { get; set; }

    public string? ParameterValue { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? Status { get; set; }

    [Column(TypeName = "numeric(12, 0)")]
    public decimal? CreationDate { get; set; }

    public int? CreatedBy { get; set; }

    [ForeignKey("ParameterId")]
    [InverseProperty("RequestDetails")]
    public virtual Parameter Parameter { get; set; } = null!;

    [ForeignKey("RequestId")]
    [InverseProperty("RequestDetails")]
    public virtual RequestData Request { get; set; } = null!;
}

