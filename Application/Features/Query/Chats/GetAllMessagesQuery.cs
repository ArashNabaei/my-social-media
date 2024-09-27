using Domain.Entities;
using MediatR;

namespace Application.Features.Query.Chats
{
    public record GetAllMessagesQuery : IRequest<IEnumerable<Message>>
    {
    }
}
