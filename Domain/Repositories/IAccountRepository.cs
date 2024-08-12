﻿using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<string> GenerateToekn();

        Task<ApplicationUser> GetAllUsers();
    }
}
