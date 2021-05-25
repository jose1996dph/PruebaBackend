using Domain.Requests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using UserCase;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserUsesCase userUsesCase;
        public UsersController(UserUsesCase userUsesCase)
        {
            this.userUsesCase = userUsesCase;
        }

        /// <summary>
        /// Show all users
        /// </summary>
        /// <returns>
        ///     A list of users
        /// </returns>
        [Authorize]
        [HttpGet]
        public IActionResult Get() => Ok(userUsesCase.Get());

        /// <summary>
        /// Show a user by Id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>
        ///     User if it ok.
        ///     Not Found if user id does not exist.
        ///     Server Error if any error occurred.
        /// </returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var user = userUsesCase.Get(id);

                return Ok(user.ToCreateUserResponse());
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="userRequest">New User</param>
        /// <returns>
        ///     User if it ok.
        ///     Bad Request if email is registered, photo is wrong or form is wrong.
        ///     Server Error if any error occurred.
        /// </returns>
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserRequest userRequest)
        {
            try
            {
                var user = userUsesCase.Create(userRequest).ToCreateUserResponse();
                return StatusCode((int)HttpStatusCode.Created, user);
            }
            catch (FormatException)
            {
                return BadRequest(new { message = "Photo invalid"});
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Email has already been registered" });
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update a task by Id
        /// </summary>
        /// <param name="id">Task's Id</param>
        /// <param name="userRequest">Task data</param>
        /// <returns>
        ///     No Content if it ok.
        ///     Bad Request if email is registered or form is wrong
        ///     Not Found if user id does not exist.
        ///     Server Error if any error occurred.
        /// </returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserRequest userRequest)
        {
            try
            {
                userRequest.UserId = id;
                userUsesCase.Update(userRequest);
                return NoContent();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Email has already been registered" });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete a user by Id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>
        ///     No Content if it ok.
        ///     Not Found if user id does not exist.
        ///     Server Error if any error occurred.
        /// </returns>
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                userUsesCase.Delete(id);
                return NoContent();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (System.Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
