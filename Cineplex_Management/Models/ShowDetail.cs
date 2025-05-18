using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class ShowDetail
{
    public int ShowDetailsId { get; set; }

    public int? ShowId { get; set; }

    public int? HallId { get; set; }

    public string? SeatNumber { get; set; }

    public bool? IsBooked { get; set; }

    public int? CustomerId { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual ICollection<SalesDetail> SalesDetails { get; set; } = new List<SalesDetail>();

    public virtual Show? Show { get; set; }
}
