using backend.Model;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    public class MessagingHub : Hub
    {
        private static readonly List<MessageModel> MessageHistory = new List<MessageModel>();

        public async Task PostMessage(string content)
        {
            var senderId = Context.ConnectionId;
            MessageModel NewMessage = new()
            {
                SenderId = senderId,
                Content = content,
                Timestamp = DateTime.UtcNow,
            };

            MessageHistory.Add(NewMessage);
            await Clients.Others.SendAsync(
                "ReceiveMessage",
                senderId,
                content,
                NewMessage.Timestamp
            );
        }

        public async Task RetrieveMessageHistory() =>
            await Clients.Caller.SendAsync("MessageHistory", MessageHistory);
    }
}
