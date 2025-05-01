using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Address")]
public partial class Address : IEntity
{
    [Key]
    [StringLength(10)]
    public string AddressId { get; set; } = null!;

    public int PersonId { get; set; }

    public int? CorporateId { get; set; }

    public int RegionId { get; set; }

    public int WilayatId { get; set; }

    [StringLength(50)]
    public string? AddressDescr { get; set; }

    [StringLength(10)]
    public string? PostalCode { get; set; }

    [StringLength(10)]
    public string? PostalBox { get; set; }

    [ForeignKey("CorporateId")]
    [InverseProperty("Addresses")]
    public virtual Corporate? Corporate { get; set; }

    [ForeignKey("PersonId")]
    [InverseProperty("Addresses")]
    public virtual Person Person { get; set; } = null!;

    [ForeignKey("RegionId")]
    [InverseProperty("Addresses")]
    public virtual Region Region { get; set; } = null!;

    [ForeignKey("WilayatId")]
    [InverseProperty("Addresses")]
    public virtual Wilayat Wilayat { get; set; } = null!;
}

