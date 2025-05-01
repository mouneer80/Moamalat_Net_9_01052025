using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Keyless]
public partial class VwRequestData : IEntity
{
    public int RequestId { get; set; }

    [Column(TypeName = "numeric(12, 0)")]
    public decimal? RequestDate { get; set; }

    public int StatusId { get; set; }

    [StringLength(50)]
    public string? StatusArName { get; set; }

    [StringLength(50)]
    public string? StatusEnName { get; set; }

    public int ServiceId { get; set; }

    [StringLength(50)]
    public string? ServiceArName { get; set; }

    [StringLength(50)]
    public string? ServiceEnName { get; set; }

    [StringLength(150)]
    public string? DescriptionAr { get; set; }

    public double? ServiceFees { get; set; }

    public int? Createdby { get; set; }

    public int ServiceCategoryId { get; set; }

    [StringLength(50)]
    public string? CategoryArName { get; set; }

    [StringLength(50)]
    public string? CategoryEnName { get; set; }
}

