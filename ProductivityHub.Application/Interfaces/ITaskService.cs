using System.Collections.Generic;
using System.Threading.Tasks;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Domain.Entities;

namespace ProductivityHub.Application.Interfaces
{
    public interface ITaskService
    {
        public Task<List<TaskEntity>> GetAllTasksAsync();
        //Task<TaskEntity> GetTaskByIdAsync(int id);
        public Task AddTaskAsync(TaskDto task);
        //Task UpdateTaskAsync(TaskEntity task);
        //Task DeleteTaskAsync(TaskEntity id);
    }
}
