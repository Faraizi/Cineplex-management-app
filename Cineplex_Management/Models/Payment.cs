using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? SalesId { get; set; }

    public int? CustomerId { get; set; }

    public int? PaymentTypeId { get; set; }

    public string? AccNo { get; set; }

    public string? BankName { get; set; }

    public int? Amount { get; set; }

    public TimeOnly? Time { get; set; }

    public virtual PaymentType? PaymentType { get; set; }

    public virtual Sale? Sales { get; set; }
}
