using FactoryManagment.Application.Abstractions.Interfaces;
using FactoryManagment.Application.Dtos;
using FactoryManagment.Domain.Entities;
using FactoryManagment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactoryManagment.Infrastructure.Persistence.Repositories
{
    public class TaskItemsRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskItemsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskItems>> GetActiveTasksByTypeAsync(string taskType)
        {
            return await _dbContext.TaskItems
                .Where(t => t.TaskType == taskType && t.Status == "Active")
                .ToListAsync();
        }
    }
}