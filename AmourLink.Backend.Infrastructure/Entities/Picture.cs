using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Picture
{
    public byte[] PictureId { get; set; } = null!;

    public string PictureUrl { get; set; } = null!;

    public byte[] UserDetailsId { get; set; } = null!;

    public DateTime AddedTime { get; set; }

    public virtual UserDetail UserDetails { get; set; } = null!;
}
