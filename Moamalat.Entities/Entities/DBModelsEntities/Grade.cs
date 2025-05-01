using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("Grade")]
public partial class Grade : IEntity
{
    [Key]
    public int GradeId { get; set; }

    [StringLength(50)]
    public string? GradeArName { get; set; }

    [StringLength(50)]
    public string? GradeEnName { get; set; }

    [InverseProperty("Grade")]
    public virtual ICollection<Corporate> Corporates { get; set; } = new List<Corporate>();
}

