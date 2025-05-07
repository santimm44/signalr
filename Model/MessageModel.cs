using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public string? SenderId { get; set; } // The user sending the message
        public string? Content { get; set; } // The content of the message
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public virtual ConversationModel Conversation { get; set; } = null!;
    }
}
