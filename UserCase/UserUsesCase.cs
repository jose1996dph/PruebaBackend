using Data.Interfaces;
using Domain.Models;
using Domain.Requests.User;
using Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsesCase.Helpers;

namespace UserCase
{
    public class UserUsesCase
    {
        readonly IUserRepository userRepository;
        readonly IFileRepository fileRepository;
        public UserUsesCase(IUserRepository userRepository, IFileRepository fileRepository)
        {
            this.userRepository = userRepository;
            this.fileRepository = fileRepository;
        }
        public User Create(CreateUserRequest userRequest)
        {
            var user = userRequest.ToUser();
            user.Password = Encrypted.Sha256(user.Password);

            fileRepository.Save(userRequest.Photo, userRequest.PhotoName);

            return userRepository.Create(user);
        }

        public void Update(UpdateUserRequest userRequest)
        {
            var user = userRequest.ToUser();

            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = Encrypted.Sha256(user.Password);
            }

            if (!string.IsNullOrEmpty(userRequest.Photo))
            {
                fileRepository.Save(userRequest.Photo, userRequest.PhotoName);
            }

            userRepository.Update(userRequest.ToUser());
        }

        public IEnumerable<CreateUserResponse> Get()
        {
            var users = new List<CreateUserResponse>();
            foreach (var user in userRepository.Get())
            {
                users.Add(user.ToCreateUserResponse());
            }
            return users;
        }

        public User Get(int id)
        {
            return userRepository.Get(id);
        }

        public void Delete(int id)
        {
            userRepository.Delete(id);
        }
    }
}
