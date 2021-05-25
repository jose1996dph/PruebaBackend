﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Requests.Auth
{
    public class LoginRequest
    {
        [Required, JsonPropertyName("username")]
        public string Username { get; set; }
        [Required, JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
