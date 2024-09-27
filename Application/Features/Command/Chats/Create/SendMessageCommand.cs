using MediatR;

namespace Application.Features.Command.Chats.Create
{
    public record SendMessageCommand(int SenderId, int ReceiverId, string Message) : IRequest
    {
    }
}
