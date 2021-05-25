using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class UserResponse
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [Required, EmailAddress, JsonPropertyName("email")]
        public string Email { get; set; }
        [Required, JsonPropertyName("full_name")]
        public string FullName { get; set; }
        [Required, JsonPropertyName("photo")]
        public string Photo { get; set; }
    }
}
