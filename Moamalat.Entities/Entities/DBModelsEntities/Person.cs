using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Person")]
public partial class Person : IEntity
{
    [Key]
    public int PersonId { get; set; }

    [StringLength(50)]
    public string? NationalId { get; set; }

    [StringLength(150)]
    public string? FullArName { get; set; }

    [StringLength(150)]
    public string? FullEnName { get; set; }

    [Column("DOB")]
    public int? Dob { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? GenderId { get; set; }

    [StringLength(12)]
    public string? Mobile { get; set; }

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? Password { get; set; }

    public double? CurrentBalance { get; set; }

    [InverseProperty("Person")]
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    [InverseProperty("Person")]
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    [InverseProperty("Person")]
    public virtual ICollection<PersonCorporate> PersonCorporates { get; set; } = new List<PersonCorporate>();

    [InverseProperty("Person")]
    public virtual ICollection<RequestData> RequestDatas { get; set; } = new List<RequestData>();
}

