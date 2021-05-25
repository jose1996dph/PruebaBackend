using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Responses.Auth
{
    public class LoginReponse
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
