using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Degree
{
    public byte[] DegreeId { get; set; } = null!;

    public string SchoolName { get; set; } = null!;

    public string DegreeType { get; set; } = null!;

    public DateTime StartYear { get; set; }

    public byte[] UserDetailsId { get; set; } = null!;

    public virtual UserDetail UserDetails { get; set; } = null!;
}
