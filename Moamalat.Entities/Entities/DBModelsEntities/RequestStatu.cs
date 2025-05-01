using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

public partial class RequestStatu : IEntity
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string? StatusArName { get; set; }

    [StringLength(50)]
    public string? StatusEnName { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<RequestTransaction> RequestTransactions { get; set; } = new List<RequestTransaction>();
}

