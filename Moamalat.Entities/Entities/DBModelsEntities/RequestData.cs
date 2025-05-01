using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Moamalat.Entities;

[Table("RequestData")]
public partial class RequestData : IEntity
{
    [Key]
    public int RequestId { get; set; }

    [Column(TypeName = "numeric(12, 0)")]
    public decimal? RequestDate { get; set; }

    public int? PersonId { get; set; }

    public int? CorporateId { get; set; }

    public int StatusId { get; set; }

    public int ServiceId { get; set; }

    public double? ServiceFees { get; set; }

    public int? Createdby { get; set; }

    [ForeignKey("CorporateId")]
    [InverseProperty("RequestDatas")]
    public virtual Corporate? Corporate { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<PaymentData> PaymentDatas { get; set; } = new List<PaymentData>();

    [ForeignKey("PersonId")]
    [InverseProperty("RequestDatas")]
    public virtual Person? Person { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<RequestDetail> RequestDetails { get; set; } = new List<RequestDetail>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestDocument> RequestDocuments { get; set; } = new List<RequestDocument>();

    [InverseProperty("Request")]
    public virtual ICollection<RequestTransaction> RequestTransactions { get; set; } = new List<RequestTransaction>();
}

