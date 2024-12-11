using ProductivityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Domain.Interfaces
{
    public interface IFormTypeRepository
    {
        Task<FormType> GetFormTypeById(int id);
        Task AddFormType(FormType formType);
    }
}
