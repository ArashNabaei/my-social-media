using Application.Dtos;
using MediatR;

namespace Application.Features.Query.Accounts
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;

}
