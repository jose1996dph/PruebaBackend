using Domain.Models;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);
        void Update(User user);
        IEnumerable<User> Get();
        User Get(int id);
        void Delete(int id);
        User IsValidUserCredentials(string username, string password);
    }
}
