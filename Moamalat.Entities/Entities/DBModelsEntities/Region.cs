using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Region")]
public partial class Region : IEntity
{
    [Key]
    public int RegionId { get; set; }

    [StringLength(50)]
    public string? RegionArName { get; set; }

    [StringLength(50)]
    public string? RegionEnName { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("Region")]
    public virtual ICollection<Wilayat> Wilayats { get; set; } = new List<Wilayat>();
}

