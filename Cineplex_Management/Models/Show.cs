using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class Show
{
    public int ShowId { get; set; }

    public string? ShowName { get; set; }

    public int? MovieId { get; set; }

    public int? HallId { get; set; }

    public DateTime? ShowStart { get; set; }

    public DateTime? ShowEnd { get; set; }

    public virtual Hall? Hall { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<ShowDetail> ShowDetails { get; set; } = new List<ShowDetail>();
}
