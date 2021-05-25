using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestStingTest.Models
{
    class UserResult
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("photo")]
        public string Photo { get; set; }
    }
}
