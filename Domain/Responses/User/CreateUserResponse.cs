using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Responses
{
    public class CreateUserResponse
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
