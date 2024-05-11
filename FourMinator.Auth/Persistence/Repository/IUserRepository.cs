﻿using FourMinator.Persistence.Domain;

namespace FourMinator.Auth
{
    internal interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task<bool> CreateUser(string nickname, string email);
    }
}
