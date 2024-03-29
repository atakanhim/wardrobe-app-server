﻿using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using wardrobe.Application.Abstractions.Services;
using wardrobe.Application.Abstractions.Token;
using wardrobe.Application.DTOs;
using wardrobe.Application.Exceptions;
using wardrobe.Domain.Entities.Identity;

namespace wardrobe.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
        readonly IUserService _userService;
        public AuthService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            UserManager<Domain.Entities.Identity.AppUser> userManager,
            ITokenHandler tokenHandler,
            SignInManager<AppUser> signInManager,
            IUserService userService
         )
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;

        }
        async Task<Token> CreateUserExternalAsync(AppUser user, string email, string name, UserLoginInfo info, int accessTokenLifeTime,int refreshTokenLifeTimeSecond)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name,
                        RefreshToken = "",// zaten bir sonraki if dongusu icerisinde refresh token update ediyoruz
                        RefreshTokenEndDate = DateTime.UtcNow,
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }
            }

            if (result)
            {
                await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, refreshTokenLifeTimeSecond);
                return token;
            }
            throw new Exception("custom error,Invalid external authentication.");
        }
        
        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTimeSecond,int refreshTokenLifeTimeSecond)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
            };
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                return await CreateUserExternalAsync(user, payload.Email, payload.Name, info, accessTokenLifeTimeSecond, refreshTokenLifeTimeSecond);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 

         
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTimeSecond, int refreshTokenLifeTimeSecond)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded) //Authentication başarılı!
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTimeSecond, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, refreshTokenLifeTimeSecond);
                return token;
            }
            throw new AuthenticationErrorException();
        }

        public Task PasswordResetAsnyc(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken, int second)// var olan refreh token kullnarak yeni bir token olusturur ve onunda üzerine koyarak yeni bir resfresh token olusturur.
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(second, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, second);
                return token;
            }
            else
                throw new NotFoundUserException();
        }

    }
}
