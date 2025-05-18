using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class Hall
{
    public int HallId { get; set; }

    public string? HallName { get; set; }

    public int? Row { get; set; }

    public int? Column { get; set; }

    public virtual ICollection<SeatPlan> SeatPlans { get; set; } = new List<SeatPlan>();

    public virtual ICollection<ShowDetail> ShowDetails { get; set; } = new List<ShowDetail>();

    public virtual ICollection<Show> Shows { get; set; } = new List<Show>();
}
