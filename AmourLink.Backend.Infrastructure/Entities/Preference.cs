using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Preference
{
    public byte[] PreferenceId { get; set; } = null!;

    public int AgeRange { get; set; }

    public byte[] UsertId { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public decimal DistanceRange { get; set; }

    public float CompatibilityPreference { get; set; }

    public virtual User Usert { get; set; } = null!;
}
