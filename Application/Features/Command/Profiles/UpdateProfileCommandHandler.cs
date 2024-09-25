using Application.Services.Profiles;
using MediatR;

namespace Application.Features.Command.Profiles
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {

        private readonly IProfileService _profileService;

        public UpdateProfileCommandHandler(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            await _profileService.UpdateProfile(request.Id, request.User);
        }

    }
}
