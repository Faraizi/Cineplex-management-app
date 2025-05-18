using System;
using System.Collections.Generic;

namespace Cineplex_Management.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? Address { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
