using ProductivityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task Add(TaskEntity task);
        Task<TaskEntity> GetById(Guid id);
        Task <IEnumerable<TaskEntity>> GetAll();
        Task Update(TaskEntity task);
        Task Delete(Guid id);
    }
}
