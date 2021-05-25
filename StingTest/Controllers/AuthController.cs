using Data.Interfaces;
using Domain.Requests.Auth;
using Domain.Responses.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using UserCase;

namespace StingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly AuthUsesCase authUsesCase;

        public AuthController(AuthUsesCase authUsesCase)
        {
            this.authUsesCase = authUsesCase;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request">data from login</param>
        /// <returns>
        ///     Access token and Refresh Token.
        ///     Unauthorized if Login fail.
        ///     Server Error if any error occurred.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                return Ok(authUsesCase.Login(request));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns>
        ///     Ok.
        ///     Unauthorized if Token is invalid.
        ///     Server Error if any error occurred.
        /// </returns>
        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            try
            {
                var userName = User.Identity.Name;
                authUsesCase.Logout(userName);
                return Ok();
            }
            catch (SecurityTokenException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="request">data from refresh token</param>
        /// <returns>
        ///     Access token and Refresh Token.
        ///     Unauthorized if Token is invalid.
        ///     Server Error if any error occurred.
        /// </returns>
        [HttpPost("refresh")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity.Name;

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = authUsesCase.Refresh(request.RefreshToken, accessToken);

                return Ok(jwtResult);
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized( new { message = e.Message});
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
