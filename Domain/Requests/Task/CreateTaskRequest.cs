using Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Requests
{
    public class CreateTaskRequest
    {
        [Required,  JsonPropertyName("title")]
        public string Title { get; set; }
        [Required,  JsonPropertyName("description")]
        public string Description { get; set; }
        [Required,  JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [Required,  JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }
        [Required,  JsonPropertyName("user_id")]
        public int? UserId { get; set; }
        [Required,  JsonPropertyName("priority")]
        public int? Priority { get; set; }

        public Task ToTask()
        {
            return new Task
            {
                TaskId = 0,
                Title = this.Title,
                Description = this.Description,
                StartDate = this.StartDate ?? default,
                DueDate = this.DueDate ?? default,
                UserId = this.UserId ?? default,
                Priority = this.Priority ?? default
            };
        }
    }
}
