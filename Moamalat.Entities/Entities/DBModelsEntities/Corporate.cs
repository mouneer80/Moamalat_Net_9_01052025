using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Corporate")]
public partial class Corporate : IEntity
{
    [Key]
    public int CorporateId { get; set; }

    [StringLength(50)]
    public string? LegalArName { get; set; }

    [StringLength(50)]
    public string? LegalEnName { get; set; }

    [Column("CRNo", TypeName = "numeric(18, 0)")]
    public decimal? Crno { get; set; }

    public int? GradeId { get; set; }

    public double? CurrentBalance { get; set; }

    [InverseProperty("Corporate")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [ForeignKey("GradeId")]
    [InverseProperty("Corporates")]
    public virtual Grade? Grade { get; set; }

    [InverseProperty("Corporate")]
    public virtual ICollection<PersonCorporate> PersonCorporates { get; set; } = new List<PersonCorporate>();

    [InverseProperty("Corporate")]
    public virtual ICollection<RequestData> RequestDatas { get; set; } = new List<RequestData>();
}

