using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Wilayat")]
public partial class Wilayat : IEntity
{
    [Key]
    public int WilayatId { get; set; }

    public int RegionId { get; set; }

    [StringLength(50)]
    public string? WilayatArName { get; set; }

    [StringLength(50)]
    public string? WilayatEnName { get; set; }

    [InverseProperty("Wilayat")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [ForeignKey("RegionId")]
    [InverseProperty("Wilayats")]
    public virtual Region Region { get; set; } = null!;
}

