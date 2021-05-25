using Domain.Responses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("users")]
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public UserResponse ToCreateUserResponse()
        {
            return new UserResponse
            {
                Email = Email,
                Photo = Photo,
                FullName = FullName,
                UserId = UserId
            };
        }
    }
}
