using ProductivityHub.Domain.Entities;
using ProductivityHub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Infrastructure.Repositories
{
    public class FormTypeRepository :IFormTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public FormTypeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateFormType(FormType formType)
        {
            await _dbContext.ForTypes.AddAsync(formType);
        }

        public async Task<FormType> GetFormTypeById(int id)
        {
            return await _dbContext.ForTypes.FindAsync(id);
        }
    }
}
