using System.ComponentModel.DataAnnotations;

namespace backend.Model
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int SenderId { get; set; } // The user sending the message
        public string? Content { get; set; } // The content of the message
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int ConversationId { get; set; }
    }
}
