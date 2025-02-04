﻿using Microsoft.AspNetCore.Identity;

namespace Webdulich.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}