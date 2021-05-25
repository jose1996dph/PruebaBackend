using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Requests.User
{
    public class CreateUserRequest
    {
        [Required, EmailAddress, JsonPropertyName("email")]
        public string Email { get; set; }
        [Required, JsonPropertyName("password")]
        public string Password { get; set; }
        [Required, Compare("Password"), JsonPropertyName("confirm_password")]
        public string ConfirmPassword { get; set; }
        [Required, JsonPropertyName("full_name")]
        public string FullName { get; set; }
        [Required, JsonPropertyName("photo")]
        public string Photo { get; set; }

        [Required, JsonPropertyName("photo_name")]
        public string PhotoName { get; set; }
        public Models.User ToUser()
        {
            return new Models.User
            {
                UserId = 0,
                Email = this.Email,
                Password = this.Password,
                FullName = this.FullName,
                Photo = this.PhotoName,
            };
        }
    }
}
