using Application.Dtos;
using Application.Services.Accounts;
using MediatR;

namespace Application.Features.Query.Accounts
{
    public class GetAllUsersQueryHandker : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {

        private readonly IAccountService _accountService;

        public GetAllUsersQueryHandker(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _accountService.GetAllUsers();

            return users;
        }
    }
}
