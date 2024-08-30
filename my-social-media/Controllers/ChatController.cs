using Application.Services.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_social_media.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : BaseController
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(int receiverId, [FromBody] string message)
        {
            await _chatService.SendMessage(UserId, receiverId, message);

            return Ok("Message sent successfully.");
        }

    }
}
