using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ConversationModel> Conversations { get; set; }
    }
}
