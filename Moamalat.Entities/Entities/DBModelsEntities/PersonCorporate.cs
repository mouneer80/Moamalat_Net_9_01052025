using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("PersonCorporate")]
public partial class PersonCorporate : IEntity
{
    [Key]
    public int PersonCorporateId { get; set; }

    public int PersonId { get; set; }

    public int CorporateId { get; set; }

    [ForeignKey("CorporateId")]
    [InverseProperty("PersonCorporates")]
    public virtual Corporate Corporate { get; set; } = null!;

    [ForeignKey("PersonId")]
    [InverseProperty("PersonCorporates")]
    public virtual Person Person { get; set; } = null!;
}

