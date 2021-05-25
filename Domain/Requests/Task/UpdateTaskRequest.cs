using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Models;

namespace Domain.Requests
{
    public class UpdateTaskRequest
    {
        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }
        [Required, JsonPropertyName("title")]
        public string Title { get; set; }
        [Required, JsonPropertyName("description")]
        public string Description { get; set; }
        [Required, JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }
        [Required, JsonPropertyName("due_date")]
        public DateTime? DueDate { get; set; }
        [Required, JsonPropertyName("user_id")]
        public int? UserId { get; set; }
        [Required, JsonPropertyName("priority")]
        public int? Priority { get; set; }

        public Task ToTask()
        {
            return new Task
            {
                TaskId = this.TaskId,
                Title = this.Title,
                Description = this.Description,
                StartDate = this.StartDate ?? default,
                DueDate = this.DueDate ?? default,
                UserId = this.UserId ?? 0,
                Priority = this.Priority ?? 0
            };
        }
    }
}
