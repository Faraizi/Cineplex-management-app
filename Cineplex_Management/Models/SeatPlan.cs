using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class SeatPlan
{
    public int SeatPlanId { get; set; }

    public string? SeatNumber { get; set; }

    public int? HallId { get; set; }

    public int? Price { get; set; }

    public int? RowNumber { get; set; }

    public virtual Hall? Hall { get; set; }
}
