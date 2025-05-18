using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class Sale
{
    public int SalesId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? Time { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? TotalPay { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();
}
