using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Parameter")]
public partial class Parameter : IEntity
{
    [Key]
    public int ParameterId { get; set; }

    [StringLength(50)]
    public string? ParameterArName { get; set; }

    [StringLength(50)]
    public string? ParameterEnName { get; set; }

    [StringLength(50)]
    public string DataTypeId { get; set; } = null!;

    [Column("pLength")]
    public int? PLength { get; set; }

    [InverseProperty("Parameter")]
    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    [InverseProperty("Parameter")]
    public virtual ICollection<ServiceParameter> ServiceParameters { get; set; } = new List<ServiceParameter>();
}

