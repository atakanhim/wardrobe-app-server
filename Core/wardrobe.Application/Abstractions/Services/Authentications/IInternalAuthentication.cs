using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wardrobe.Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication
    {
        Task<DTOs.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTimeSecond, int refreshTokenLifeTimeSecond);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken,int refreshTokenLifeTimeSecond);
    }
}
