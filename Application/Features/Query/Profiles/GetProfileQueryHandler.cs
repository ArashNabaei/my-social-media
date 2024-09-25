using Application.Services.Profiles;
using Domain.Entities;
using MediatR;

namespace Application.Features.Query.Profiles
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, User>
    {

        private readonly IProfileService _ProfileService;

        public GetProfileQueryHandler(IProfileService profileService)
        {
            _ProfileService = profileService;
        }

        public async Task<User> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var result = await _ProfileService.GetProfile(request.Id);

            return result;
        }
    }
}
