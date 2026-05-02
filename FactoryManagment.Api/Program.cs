using FactoryManagment.Infrastructure;

using FactoryManagment.Application.Abstractions.Interfaces;
using FactoryManagment.Application.Services;
using FactoryManagment.Infrastructure.Persistence;
using FactoryManagment.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddScoped<ITaskItemRepository, TaskItemsRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("FactoryManagment.Infrastructure")
    ));

var app = builder.Build();

builder.Services.AddDependencies(builder.Configuration);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapControllers();
app.Run();

