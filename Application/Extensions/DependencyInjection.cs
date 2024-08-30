using Application.Services.Accounts;
using Application.Services.Chats;
using Application.Services.Follows;
using Application.Services.Posts;
using Application.Services.Profiles;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IChatService, ChatService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();

            services.AddScoped<DapperContext>();

            return services;
        }
    }
}
