using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class MessageModel
    {
        [Key]
        public int MessageId { get; set; }
        public string? SenderId { get; set; } // The user sending the message
        public string? Content { get; set; } // The content of the message
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int ConversationId { get; set; }
        public ConversationModel? Conversation { get; set; }

        //public string ReceiverId { get; set; } // The user receiving the message
        //public Guid ConversationId { get; set; }
        //public ConversationModel Conversation { get; set; }  // Navigation property to Conversation

        // public required string Sender { get; set; }
        // public string receiver { get; set; }

        //the actual message and time it was sent
        // public required string Content { get; set; }
        // public DateTime Timestamp { get; set; }

        //The following is in prep for when we have private messages between users

        //To create a Globally Unique ID or GUID
        // public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key that links the message to a conversation
        // public Guid ConversationId { get; set; }
        // A navigation property that represents the full Conversation object this message belongs to.
        // public Conversation Conversation { get; set; }
    }
}
