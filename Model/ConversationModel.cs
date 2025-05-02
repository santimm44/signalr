using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend.Model
{
    public class ConversationModel
    {
        [Key]
        public int ConversationId { get; set; }

        public int FirstUserId { get; set; }

        public int SecondUserId { get; set; }

        public List<MessageModel> Messages { get; set; } = new();
    }
}
