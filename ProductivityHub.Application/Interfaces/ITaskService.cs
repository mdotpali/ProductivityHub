using System.Collections.Generic;
using System.Threading.Tasks;
using ProductivityHub.Domain.Entities;

namespace ProductivityHub.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskEntity>> GetAllTasksAsync();
        Task<TaskEntity> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskEntity task);
        Task UpdateTaskAsync(TaskEntity task);
        Task DeleteTaskAsync(TaskEntity id);
    }
}
