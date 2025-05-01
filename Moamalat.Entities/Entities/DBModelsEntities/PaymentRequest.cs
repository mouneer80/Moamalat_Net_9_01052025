using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("PaymentRequest")]
public partial class PaymentRequest : IEntity
{
    [Key]
    public int PaymentRequestId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TrackId { get; set; }

    [Column("pStatus")]
    public int PStatus { get; set; }

    public int UserId { get; set; }

    public int CompanyId { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? CreateDate { get; set; }

    [StringLength(50)]
    public string? BankResultDescription { get; set; }

    [StringLength(150)]
    public string? SessionId { get; set; }

    [Column(TypeName = "decimal(6, 3)")]
    public decimal Amount { get; set; }

    [StringLength(150)]
    public string? InvoiceId { get; set; }

    [NotMapped]
    public string? HostedPageUrl { get; set; }
}

