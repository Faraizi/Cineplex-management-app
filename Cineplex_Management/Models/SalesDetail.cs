using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class SalesDetail
{
    public int SalesDetailsId { get; set; }

    public int? SalesId { get; set; }

    public int? CustomerId { get; set; }

    public int? Price { get; set; }

    public int? ShowDetailsId { get; set; }

    public virtual Sale? Sales { get; set; }

    public virtual ShowDetail? ShowDetails { get; set; }
}
