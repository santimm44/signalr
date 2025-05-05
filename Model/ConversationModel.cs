using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend.Model
{
    public class ConversationModel
    {
        public int Id { get; set; }

        public int FirstUserId { get; set; }

        public int SecondUserId { get; set; }

        public virtual ICollection<MessageModel>? Messages { get; set; }
    }
}
