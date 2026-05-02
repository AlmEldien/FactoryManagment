using FactoryManagment.Application.Abstractions.Interfaces;
using FactoryManagment.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
namespace FactoryManagment.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ITaskItemRepository _taskRepository;

        public DashboardService(ITaskItemRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<CurrentActivityDto> GetCurrentActivityAsync()
        {

            var assemblyEntities = await _taskRepository.GetActiveTasksByTypeAsync("Assembly");
            var armoringEntities = await _taskRepository.GetActiveTasksByTypeAsync("Armoring");
            var extrusionEntities = await _taskRepository.GetActiveTasksByTypeAsync("Extrusion");  


            return new CurrentActivityDto
            {
                AssemblyTasks = assemblyEntities.Select(t => new ActiveTaskDto { Id = t.Id, Name = t.TaskName, Type = t.TaskType , Status = t.Status}),
                ArmoringTasks = armoringEntities.Select(t => new ActiveTaskDto { Id = t.Id, Name = t.TaskName , Type = t.TaskType , Status = t.Status }),
                ExtrusionTasks = extrusionEntities.Select(t => new ActiveTaskDto { Id = t.Id, Name = t.TaskName, Type = t.TaskType, Status = t.Status })
            };
        }
    }
}
