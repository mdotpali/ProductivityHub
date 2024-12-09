using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PlanedDate { get; set; }
        public FormType TaskType { get; set; }
        public int TypeId { get; set; }
        public Status TaskStatus { get; set; }
        public int TaskStatusId { get; set; }

    }
}
