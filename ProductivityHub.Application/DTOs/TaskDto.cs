using ProductivityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Application.DTOs
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PlannedDate { get; set; }
        public string TaskTypeName { get; set; }
        public int TypeId { get; set; }
        public string TaskStatusName { get; set; }
        public int TaskStatusId { get; set; }
    }
}
