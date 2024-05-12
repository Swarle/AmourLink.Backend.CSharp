using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Language
{
    public byte[] LanguageId { get; set; } = null!;

    public string LanguageName { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetailsUserDetails { get; set; } = new List<UserDetail>();
}
