using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("ServiceParameter")]
public partial class ServiceParameter : IEntity
{
    [Key]
    public int ServiceParameterId { get; set; }

    public int ServiceId { get; set; }

    public int ParameterId { get; set; }

    [ForeignKey("ParameterId")]
    [InverseProperty("ServiceParameters")]
    public virtual Parameter Parameter { get; set; } = null!;

    [ForeignKey("ServiceId")]
    [InverseProperty("ServiceParameters")]
    public virtual ServiceData Service { get; set; } = null!;

    [NotMapped]
    public bool isValid { get; set; }

    [NotMapped]
    public string? ParameterValue { get; set; }// = "TEST DATA";

}

