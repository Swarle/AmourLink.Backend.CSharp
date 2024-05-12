using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class User
{
    public byte[] UserId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public int Rating { get; set; }

    public virtual ICollection<Match> MatchUserAccountReceiveds { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchUserGivens { get; set; } = new List<Match>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Preference> Preferences { get; set; } = new List<Preference>();

    public virtual UserDetail? UserDetail { get; set; }

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
