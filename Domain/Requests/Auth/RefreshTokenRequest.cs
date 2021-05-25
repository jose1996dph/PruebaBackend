using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Requests.Auth
{
    public class RefreshTokenRequest
    {
        [Required, JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
