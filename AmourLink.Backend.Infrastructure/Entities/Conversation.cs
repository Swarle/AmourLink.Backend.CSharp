using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Conversation
{
    public byte[] ConversationId { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
