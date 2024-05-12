using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class UserDetail
{
    public byte[] UserDetailsId { get; set; } = null!;

    public byte[] UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public uint Age { get; set; }

    public string? Bio { get; set; }

    public int? Height { get; set; }

    public string? Occupation { get; set; }

    public string? Nationality { get; set; }

    public byte[]? MusicId { get; set; }

    public string Gender { get; set; } = null!;

    public float? LastLocationLongitude { get; set; }

    public float? LastLocationLatitude { get; set; }

    public virtual ICollection<Degree> Degrees { get; set; } = new List<Degree>();

    public virtual ICollection<Hobbie> Hobbies { get; set; } = new List<Hobbie>();

    public virtual Music? Music { get; set; }

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Language> LanguageLanguages { get; set; } = new List<Language>();
}
