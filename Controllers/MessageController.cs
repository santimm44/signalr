using backend.service;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IChatService _chatService;

        public MessageController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("Conversation/{FirstUser}/{secondUser}")]
        public async Task<IActionResult> GetConversation(int FirstUser, int secondUser)
        {
            var conversation = await _chatService.GetOrCreateConversation(FirstUser, secondUser);
            return Ok(conversation);
        }

        [HttpPost("SaveMessage/{UserMessage}/{User}/{TimeStamp}/{SecondUser}")]
        public async Task<IActionResult> StoreMessage(
            string UserMessage,
            int User,
            string TimeStamp,
            int SecondUser
        )
        {
            var storedMessage = await _chatService.StoreMessage(
                UserMessage,
                User,
                TimeStamp,
                SecondUser
            );
            return Ok(storedMessage);
        }

        [HttpGet("GetAllMessages/{FirstUser}/{SecondUser}")]
        public async Task<IActionResult> GetAllMessages(int FirstUser, int SecondUser)
        {
            var logs = await _chatService.GetAllMessagesMethod(FirstUser, SecondUser);
            return Ok(logs);
        }
    }
}
