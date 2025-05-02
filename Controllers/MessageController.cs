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

        [HttpPost("SaveMessage/{UserMessage}")]
        public async Task<IActionResult> StoreMessage(string UserMessage)
        {
            var storedMessage = await _chatService.StoreMessage(UserMessage);
            return Ok(storedMessage);
        }
    }
}
