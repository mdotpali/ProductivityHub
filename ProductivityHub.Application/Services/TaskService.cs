using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Domain.Entities;
using ProductivityHub.Domain.Interfaces;

namespace ProductivityHub.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IFormTypeRepository _formTypeRepository;

        public TaskService(ITaskRepository taskRepository, IFormTypeRepository formTypeRepository)
        {
            _taskRepository = taskRepository;
            _formTypeRepository = formTypeRepository;
        }
        public async Task AddTaskAsync(TaskDto taskDto)
        {


            FormType formType = await _formTypeRepository.GetFormTypeById(taskDto.TypeId);
            if (formType == null)
            {
                formType = new FormType { Id = taskDto.TypeId, Name = taskDto.TaskTypeName };
                 await _formTypeRepository.CreateFormType(formType);
            }

            var task = new TaskEntity
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                TaskStatusId = taskDto.TaskStatusId,
                TypeId = taskDto.TypeId,
                TaskType = formType,
                PlanedDate = taskDto.PlanedDate,
            };

            await _taskRepository.Add(task);
        }

        //public Task DeleteTaskAsync(TaskEntity id)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<List<TaskEntity>> GetAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        //public Task<TaskEntity> GetTaskByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateTaskAsync(TaskEntity task)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
