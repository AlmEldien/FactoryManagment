using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FactoryManagment.Domain.Entities
{
    public class TaskItems
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string TaskName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string TaskType { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [MaxLength(100)]
        public string MachineName { get; set; } = string.Empty;

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }
}
