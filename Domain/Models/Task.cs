using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("tasks")]
    public class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        [ForeignKey("users")]
        public int UserId { get; set; }
        public int Priority { get; set; }
    }
}
