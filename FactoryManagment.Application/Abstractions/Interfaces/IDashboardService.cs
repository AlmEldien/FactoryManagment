using FactoryManagment.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryManagment.Application.Abstractions.Interfaces
{
    public interface IDashboardService
    {
        Task<CurrentActivityDto> GetCurrentActivityAsync();

    }
}
