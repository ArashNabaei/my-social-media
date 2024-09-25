using Domain.Entities;
using MediatR;

namespace Application.Features.Command.Profiles
{
    public record UpdateProfileCommand(int Id, User user) : IRequest
    {
    }
}
