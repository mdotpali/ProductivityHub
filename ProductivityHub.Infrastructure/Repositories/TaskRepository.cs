using Microsoft.EntityFrameworkCore;
using ProductivityHub.Domain.Entities;
using ProductivityHub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductivityHub.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _dbContext;

        public TaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(TaskEntity task)
        {
           await _dbContext.AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskEntity>> GetAll()
        {
             return await _dbContext.Tasks.Include(t => t.TaskStatus).Include(t => t.TaskType).ToListAsync();
        }

        public async Task<TaskEntity> GetById(Guid id)
        {
            return await _dbContext.Tasks.Include(t => t.TaskStatus).Include(t => t.TaskType).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task Update(TaskEntity task)
        {
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
