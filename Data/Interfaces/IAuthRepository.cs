using Domain.Models;
using Domain.Responses.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IAuthRepository
    {
        LoginReponse Refresh(string refreshToken, string accessToken);
        LoginReponse GenerateTokens(User user);
        void Logout(string username);
    }
}
