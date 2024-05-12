using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Hobbie
{
    public byte[] HobbieId { get; set; } = null!;

    public byte[] UserDetailsId { get; set; } = null!;

    public string HobbieName { get; set; } = null!;

    public virtual UserDetail UserDetails { get; set; } = null!;
}
