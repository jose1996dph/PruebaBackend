using Data.Interfaces;
using Domain.Models;
using Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        public User Create(User user)
        {
            using var database = new DataBase();

            database.Add(user);

            database.SaveChanges();

            return user;
        }

        public IEnumerable<User> Get()
        {
            using var database = new DataBase();

            return database.Users.ToList();
        }

        public User Get(int id)
        {
            using var database = new DataBase();

            return database.Users.Find(id) ?? throw new NullReferenceException("User not fount");
        }

        public void Update(User user)
        {
            using var database = new DataBase();

            var oldUser = database.Users.Find(user.UserId);

            if (oldUser == null)
            {
                throw new NullReferenceException("User not fount");
            }

            oldUser.Email = user.Email;
            oldUser.FullName = user.FullName;
            oldUser.Password = string.IsNullOrEmpty(user.Password) ? oldUser.Password : user.Password;
            oldUser.Photo = string.IsNullOrEmpty(user.Photo) ? oldUser.Photo : user.Photo;

            database.Users.Update(oldUser);

            database.SaveChanges();
        }

        public void Delete(int id)
        {
            using var database = new DataBase();

            var user = database.Users.Find(id);

            if (user == null)
            {
                throw new NullReferenceException("User not fount");
            }

            database.Users.Remove(user);

            database.SaveChanges();
        }

        public User IsValidUserCredentials(string username, string password)
        {
            using var database = new DataBase();

            var i = database.Users.ToList();

            return database.Users.SingleOrDefault(x => x.Email == username && x.Password == password);
        }
    }
}
