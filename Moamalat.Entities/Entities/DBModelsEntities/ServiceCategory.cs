using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[EntityPluralName("ServiceCategories")]
[Table("ServiceCategory")]
public partial class ServiceCategory : IEntity
{
    [Key]
    public int ServiceCategoryId { get; set; }

    [StringLength(50)]
    public string? CategoryArName { get; set; }

    [StringLength(50)]
    public string? CategoryEnName { get; set; }

    [StringLength(150)]
    public string? Icon { get; set; }

    [InverseProperty("ServiceCategory")]
    public virtual ICollection<ServiceData> ServiceDatas { get; set; } = new List<ServiceData>();
}

