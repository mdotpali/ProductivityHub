using Microsoft.EntityFrameworkCore;
using ProductivityHub.Domain.Entities;
using ProductivityHub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _dbContext;

        public StatusRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddStatus(Status status)
        {
            await _dbContext.Statuses.AddAsync(status);
        }

        public async Task<Status> GetStatusById(int id)
        {
            return await _dbContext.Statuses.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
