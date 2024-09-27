using Application.Dtos;
using MediatR;

namespace Application.Features.Query.Chats
{
    public record SearchUserByNameQuery(int UserId, string Pattern) : IRequest<IEnumerable<UserDto>>
    {
    }
}
