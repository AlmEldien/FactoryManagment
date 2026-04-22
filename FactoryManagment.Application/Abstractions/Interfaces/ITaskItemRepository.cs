using FactoryManagment.Application.Dtos;
using FactoryManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryManagment.Application.Abstractions.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItems>> GetActiveTasksByTypeAsync(string taskType);

    }
}
