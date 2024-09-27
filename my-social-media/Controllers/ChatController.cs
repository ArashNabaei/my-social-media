using Application.Features.Query.Chats;
using Application.Services.Chats;
using MediatR;
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

        private readonly ISender _sender;

        public ChatController(IChatService chatService, ISender sender)
        {
            _chatService = chatService;
            _sender = sender;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(int receiverId, [FromBody] string message)
        {
            await _chatService.SendMessage(UserId, receiverId, message);

            return Ok("Message sent successfully.");
        }

        [HttpGet("GetAllMessages")]
        public async Task<IActionResult> GetAllMessages(int id)
        {
            var messages = await _sender.Send(new GetAllMessagesQuery(UserId, id));

            return Ok(messages);
        }

        [HttpDelete("DeleteMessage")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            await _chatService.DeleteMessage(UserId, messageId);

            return Ok("Message deleted successfully.");
        }

        [HttpPut("UpdateMessage")]
        public async Task<IActionResult> UpdateMessage(int messageId, [FromBody] string message)
        {
            await _chatService.UpdateMessage(UserId,messageId, message);

            return Ok("Message updated successfully.");
        }

        [HttpGet("SearchUserByName")]
        public async Task<IActionResult> SearchUserByName(string pattern)
        {
            var users = await _chatService.SearchUserByName(UserId, pattern);

            return Ok(users);
        }

    }
}
