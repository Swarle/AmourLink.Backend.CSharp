using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Response
{
    public byte[] ResponseId { get; set; } = null!;

    public string Match { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
