using System;
using System.Collections.Generic;

namespace AmourLink.Backend.Infrastructure.Entities;

public partial class Message
{
    public byte[] MessageId { get; set; } = null!;

    public string MessageBody { get; set; } = null!;

    public bool Read { get; set; }

    public DateTime CreationDatre { get; set; }

    public byte[] ConversationId { get; set; } = null!;

    public byte[] UserId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
