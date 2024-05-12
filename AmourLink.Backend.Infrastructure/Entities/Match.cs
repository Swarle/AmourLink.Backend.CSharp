using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Match
{
    public byte[] MatchId { get; set; } = null!;

    public string Match1 { get; set; } = null!;

    public byte[] UserGivenId { get; set; } = null!;

    public byte[] UserAccountReceivedId { get; set; } = null!;

    public byte[] ResponseId { get; set; } = null!;

    public string CompatibilityScore { get; set; } = null!;

    public virtual Response Response { get; set; } = null!;

    public virtual User UserAccountReceived { get; set; } = null!;

    public virtual User UserGiven { get; set; } = null!;
}
