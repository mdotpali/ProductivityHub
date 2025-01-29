using System.Collections.Generic;
using System.Threading.Tasks;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Domain.Entities;

namespace ProductivityHub.Application.Interfaces
{
    public interface ITaskService
    {
        public Task<List<TaskEntity>> GetAllTasksAsync();
        Task<TaskEntity> GetTaskByIdAsync(Guid id);
        public Task AddTaskAsync(TaskDto task);
        Task UpdateTaskAsync(TaskDto task);
        Task DeleteTaskAsync(Guid taskId);
    }
}
