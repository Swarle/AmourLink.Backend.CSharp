using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Music
{
    public byte[] MusicId { get; set; } = null!;

    public byte[] SpotifyId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string ArtistName { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
