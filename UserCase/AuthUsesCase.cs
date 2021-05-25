using Data.Interfaces;
using Domain.Requests.Auth;
using Domain.Responses.Auth;
using System;
using System.Security.Claims;
using UsesCase.Helpers;

namespace UserCase
{
    public class AuthUsesCase
    {
        readonly IAuthRepository authRepository;
        readonly IUserRepository userRepository;
        public AuthUsesCase(IAuthRepository authRepository, IUserRepository userRepository)
        {
            this.authRepository = authRepository;
            this.userRepository = userRepository;
        }
        public LoginReponse Refresh(string refreshToken, string accessToken)
        {
            return authRepository.Refresh(refreshToken, accessToken);
        }

        public LoginReponse Login(LoginRequest loginRequest)
        {
            var user = userRepository.IsValidUserCredentials(loginRequest.Username, Encrypted.Sha256(loginRequest.Password));
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            return authRepository.GenerateTokens(user);
        }


        public void Logout(string userName)
        {
            authRepository.Logout(userName);
        }
    }
}
