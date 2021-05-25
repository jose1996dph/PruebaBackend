using Domain.Requests;
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
    public class TasksController : ControllerBase
    {
        readonly TaskUsesCase taskUsesCase;
        public TasksController(TaskUsesCase taskUsesCase)
        {
            this.taskUsesCase = taskUsesCase;
        }

        /// <summary>
        /// Show all tasks
        /// </summary>
        /// <returns>
        ///     A list of users
        /// </returns>
        [HttpGet]
        [Authorize]
        public IActionResult All([FromQuery] int userId = 0)
        {
            return Ok(this.taskUsesCase.All(userId));
        }

        /// <summary>
        /// Show a task by Id
        /// </summary>
        /// <param name="id">Task's Id</param>
        /// <returns>
        ///     Task if it ok.
        ///     Not Found if task id does not exist.
        ///     Server Error if any error occurred.
        /// </returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(taskUsesCase.Get(id));
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
        /// Create a new Task
        /// </summary>
        /// <param name="userRequest">New Task</param>
        /// <returns>
        ///     Task if it ok.
        ///     Bad Request if user is not registered or form is wrong.
        ///     Server Error if any error occurred.
        /// </returns>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] CreateTaskRequest taskRequest)
        {
            try
            {
                var task = taskUsesCase.Create(taskRequest);
                return StatusCode((int)HttpStatusCode.Created, task);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Message = "User not found"});
            }
            catch (Exception)
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
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] UpdateTaskRequest taskRequest)
        {
            try
            {
                taskRequest.TaskId = id;
                taskUsesCase.Update(taskRequest);
                return NoContent();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Message = "User not found" });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete a task by Id
        /// </summary>
        /// <param name="id">Task's Id</param>
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
                taskUsesCase.Delete(id);
                return Ok();
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
    }
}
