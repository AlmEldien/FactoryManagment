using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryManagment.Application.Dtos
{
    public class ActiveTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
    }
}
