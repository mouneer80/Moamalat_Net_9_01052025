using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("TopUpHistory")]
public partial class TopUpHistory : IEntity
{
    [Key]
    public int TopupId { get; set; }

    [Column(TypeName = "numeric(12, 0)")]
    public decimal? TopupDate { get; set; }

    public double? Amount { get; set; }

    public double? BalanceBefore { get; set; }

    public double? BalanceAfter { get; set; }

    public int? PersonId { get; set; }

    public int? CorporateId { get; set; }
}

