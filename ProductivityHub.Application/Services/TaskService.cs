using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Domain.Entities;
using ProductivityHub.Infrastructure;

namespace ProductivityHub.Application.Services
{
    public class TaskService : ITaskService
    {
        public Task AddTaskAsync(TaskEntity task)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(TaskEntity id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TaskEntity>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskEntity> GetTaskByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTaskAsync(TaskEntity task)
        {
            throw new NotImplementedException();
        }
    }
}
