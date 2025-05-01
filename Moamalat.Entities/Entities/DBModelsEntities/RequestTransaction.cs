using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("RequestTransaction")]
public partial class RequestTransaction : IEntity
{
    [Key]
    public int TransactionId { get; set; }

    public int RequestId { get; set; }

    [Column(TypeName = "numeric(12, 0)")]
    public decimal? TransactionDate { get; set; }

    public int StatusId { get; set; }

    public int? CreatedBy { get; set; }

    [StringLength(50)]
    public string? Notes { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("RequestTransactions")]
    public virtual RequestData Request { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("RequestTransactions")]
    public virtual RequestStatu Status { get; set; } = null!;
}

