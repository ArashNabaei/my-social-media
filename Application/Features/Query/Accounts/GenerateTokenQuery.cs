using MediatR;

namespace Application.Features.Query.Accounts
{
    public record GenerateTokenQuery : IRequest<string>;
}
