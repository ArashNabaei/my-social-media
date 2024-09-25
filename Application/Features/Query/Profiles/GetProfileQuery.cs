using Domain.Entities;
using MediatR;

namespace Application.Features.Query.Profiles
{
    public record GetProfileQuery(int Id) : IRequest<User>
    {
    }
}
