using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("PaymentData")]
public partial class PaymentData : IEntity
{
    [Key]
    public int PaymentId { get; set; }

    [Column(TypeName = "numeric(12, 0)")]
    public decimal? PaymentDate { get; set; }

    public int? RequestId { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal? BalanceBefore { get; set; }

    [Column(TypeName = "decimal(18, 3)")]
    public decimal? BalanceAfter { get; set; }

    [ForeignKey("RequestId")]
    [InverseProperty("PaymentDatas")]
    public virtual RequestData? Request { get; set; }
}

