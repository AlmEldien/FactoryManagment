using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryManagment.Application.Dtos
{
    public class CurrentActivityDto
    {
        public IEnumerable<ActiveTaskDto> AssemblyTasks { get; set; } = new List<ActiveTaskDto>();

        public IEnumerable<ActiveTaskDto> ArmoringTasks { get; set; } = new List<ActiveTaskDto>();

        public IEnumerable<ActiveTaskDto> ExtrusionTasks { get; set; } = new List<ActiveTaskDto>();
    }
}
