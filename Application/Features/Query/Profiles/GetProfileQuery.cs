using Domain.Entities;
using MediatR;

namespace Application.Features.Query.Profiles
{
    public record GetProfileQuery(int Id, string FirstName, 
        string LastName, string Username, 
        string Password, string Bio, 
        string Email, string PhoneNumber, 
        string ImageUrl, DateTime DateOfBirth) : IRequest<User>
    {
    }
}
