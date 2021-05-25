using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Requests.User
{
    public class UpdateUserRequest
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [Required, EmailAddress,  JsonPropertyName("email")]
        public string Email { get; set; }
        [Required, JsonPropertyName("full_name")]
        public string FullName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [Compare("Password"), JsonPropertyName("confirm_password")]
        public string ConfirmPassword { get; set; }
        [JsonPropertyName("photo")]
        public string Photo { get; set; }
        [JsonPropertyName("photo_name"), ]
        public string PhotoName { get; set; }
        public Models.User ToUser()
        {
            return new Models.User
            {
                UserId = this.UserId,
                Email = this.Email,
                Password = this.Password,
                FullName = this.FullName,
                Photo = this.Photo,
            };
        }
    }
}
