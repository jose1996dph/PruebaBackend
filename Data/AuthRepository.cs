using Data.Interfaces;
using Domain.Configures;
using Domain.Models;
using Domain.Responses.Auth;
using Framework;
using System;
using System.Linq;
using System.Security.Claims;

namespace Data
{
    public class AuthRepository : IAuthRepository
    {
        readonly JWTAuthenticate JWTAuthenticate;
        public AuthRepository(JWTAuthenticate jwtAuthenticate)
        {
            this.JWTAuthenticate = jwtAuthenticate;
        }
        public LoginReponse GenerateTokens(User user)
        {
            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim("FullName", user.FullName),
                new Claim("Photo", user.Photo),
            };

            return JWTAuthenticate.GenerateTokens(user.Email, claims, DateTime.Now);
        }

        public void Logout(string username)
        {
            JWTAuthenticate.RemoveRefreshTokenByUserName(username);
        }

        public LoginReponse Refresh(string refreshToken, string accessToken)
        {
            return JWTAuthenticate.Refresh(refreshToken, accessToken, DateTime.Now);
        }
    }
}
