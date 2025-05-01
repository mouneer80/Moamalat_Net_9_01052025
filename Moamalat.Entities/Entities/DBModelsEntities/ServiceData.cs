using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("ServiceData")]
public partial class ServiceData : IEntity
{
    [Key]
    public int ServiceId { get; set; }

    [StringLength(50)]
    public string? ServiceArName { get; set; }

    [StringLength(50)]
    public string? ServiceEnName { get; set; }

    [StringLength(150)]
    public string? DescriptionAr { get; set; }

    [StringLength(150)]
    public string? DescriptionEn { get; set; }

    public int ServiceCategoryId { get; set; }

    public double? SanadFees { get; set; }

    public double? ServiceFees { get; set; }

    [StringLength(150)]
    public string? Icon { get; set; }

    [ForeignKey("ServiceCategoryId")]
    [InverseProperty("ServiceDatas")]
    public virtual ServiceCategory ServiceCategory { get; set; } = null!;

    [InverseProperty("Service")]
    public virtual ICollection<ServiceParameter> ServiceParameters { get; set; } = new List<ServiceParameter>();

    [InverseProperty("Service")]
    public virtual ICollection<ServiceRequiredDocument> ServiceRequiredDocuments { get; set; } = new List<ServiceRequiredDocument>();
}

