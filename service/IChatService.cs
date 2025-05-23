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
            var conversation = await GetOrCreateConversationHelper(firstUser, secondUser);

            if (conversation == null)
            {
                conversation = new ConversationModel
                {
                    FirstUserId = firstUser,
                    SecondUserId = secondUser,
                };

                await _context.Conversations.AddAsync(conversation);
                await _context.SaveChangesAsync();
            }

            return conversation;
        }

        private async Task<ConversationModel> GetOrCreateConversationHelper(
            int firstUser,
            int secondUser
        )
        {
            return await _context
                .Conversations.Include(c => c.Messages)
                .FirstOrDefaultAsync(c =>
                    (c.FirstUserId == firstUser && c.SecondUserId == secondUser)
                    || (c.FirstUserId == secondUser && c.SecondUserId == firstUser)
                );
            ;
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
            int theConversationId = findConversation.Id;

            var messageToStore = new MessageModel
            {
                Content = messageContent,
                SenderId = userId.ToString(),
                Timestamp = messageTimeStamp,
                ConversationId = theConversationId,
            };

            _context.Messages.Add(messageToStore);
            await _context.SaveChangesAsync();
            return messageToStore;
        }

        private DateTime stringToDateTime(string timeStamp)
        {
            timeStamp = timeStamp.Replace(",", "");
            timeStamp = Uri.UnescapeDataString(timeStamp);
            /*  Notes:
            The string that is given to DateTime.Parse was returning "5%2F5%2F2025 2:47:03 PM"
            This cannot be converted to a datetime format
            Method above is responsible for converting it to the following
            "5/5/2025 2:47:03 PM"
            Uri is a class in the System namespace.
            It represents a Uniform Resource Identifier (URI) and contains various static methods and properties for URI handling.
            (UnescapedDataString) This is a static method of the Uri class.
            It converts percent-encoded characters (e.g., %2F, %20) back to their normal readable form (/, space, etc.).
            This is necessary when you're receiving URL-encoded input, like from query strings or form data submitted over HTTP.
            */
            return DateTime.Parse(timeStamp);
        }

        public async Task<List<MessageModel>> GetAllMessagesMethod(int firstUser, int secondUser)
        {
            ConversationModel findConversation = await GetOrCreateConversation(
                firstUser,
                secondUser
            );

            bool DoesConversationExist = await GetAllMessagesHelper(firstUser, secondUser);

            if (DoesConversationExist)
            {
                return null;
            }

            return await _context
                .Messages.Where(m => m.ConversationId == findConversation.Id)
                .ToListAsync();
        }

        private async Task<bool> GetAllMessagesHelper(int firstUser, int secondUser)
        {
            ConversationModel findConversation = await GetOrCreateConversation(
                firstUser,
                secondUser
            );
            //if statement
            return false;
        }
        /*
        Create helper function(s)
        DoesConversationExist return Boolean
        Inside GetAllMessagesMethod create an if statement based on the results of helper function
            Do Logic to find where the error is at
            New conversations or the existing conversations
        */
    }
}
