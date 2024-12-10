using Microsoft.EntityFrameworkCore;
using ProductivityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskEntity>().HasOne(t=>t.TaskType).WithMany().HasForeignKey(t => t.TypeId);
            modelBuilder.Entity<TaskEntity>().HasOne(t=>t.TaskStatus).WithMany().HasForeignKey(t => t.TaskStatusId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
