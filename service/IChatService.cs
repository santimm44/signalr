using backend.Context;
using backend.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.service
{
    public class IChatService
    {
        private readonly DataContext _context;

        public IChatService(DataContext context)
        {
            _context = context;
        }

        public async Task<ConversationModel> GetOrCreateConversation(int firstUser, int secondUser)
        {
            var conversation = await _context
                .Conversations.Include(c => c.Messages)
                .FirstOrDefaultAsync(c =>
                    (c.FirstUserId == firstUser && c.SecondUserId == secondUser)
                    || (c.FirstUserId == secondUser && c.SecondUserId == firstUser)
                );

            if (conversation == null)
            {
                conversation = new ConversationModel
                {
                    FirstUserId = firstUser,
                    SecondUserId = secondUser,
                };

                _context.Conversations.Add(conversation);
                await _context.SaveChangesAsync();
            }

            return conversation;
        }

        public async Task<MessageModel> StoreMessage(
            string messageContent,
            int userId,
            string timeStamp,
            int secondUser
        )
        {
            ConversationModel findConversation = await GetOrCreateConversation(userId, secondUser);
            DateTime messageTimeStamp = stringToDateTime(timeStamp);
            int theConversationId = findConversation.ConversationId;

            var messageToStore = new MessageModel
            {
                Content = messageContent,
                SenderId = userId,
                Timestamp = messageTimeStamp,
                ConversationId = theConversationId,
            };

            _context.Messages.Add(messageToStore);
            await _context.SaveChangesAsync();
            return messageToStore;
        }

        private DateTime stringToDateTime(string timeStamp)
        {
            return DateTime.Parse(timeStamp);
        }
    }
}
