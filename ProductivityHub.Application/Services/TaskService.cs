using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prism.Events;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Events;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Domain.Entities;
using ProductivityHub.Domain.Interfaces;

namespace ProductivityHub.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IFormTypeRepository _formTypeRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IEventAggregator _eventAggregator;

        public TaskService(ITaskRepository taskRepository, IFormTypeRepository formTypeRepository, IStatusRepository statusRepository
            , IEventAggregator eventAggregator)
        {
            _taskRepository = taskRepository;
            _formTypeRepository = formTypeRepository;
            _statusRepository = statusRepository;
            _eventAggregator = eventAggregator;
        }
        public async Task AddTaskAsync(TaskDto taskDto)
        {


            FormType formType = await _formTypeRepository.GetFormTypeById(taskDto.TypeId);
            if (formType == null)
            {
                formType = new FormType { Id = taskDto.TypeId, Name = taskDto.TaskTypeName };
                await _formTypeRepository.AddFormType(formType);
            }

            Status taskStatus = await _statusRepository.GetStatusById(taskDto.TaskStatusId);
            if (taskStatus == null)
            {
                taskStatus = new Status { Id = taskDto.TaskStatusId, Name = taskDto.TaskStatusName };
                await _statusRepository.AddStatus(taskStatus);
            }

            var task = new TaskEntity
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                TaskStatusId = taskDto.TaskStatusId,
                TaskStatus = taskStatus,
                TypeId = taskDto.TypeId,
                TaskType = formType,
                PlannedDate = taskDto.PlannedDate,
                Id = taskDto.Id
            };

            await _taskRepository.Add(task);
            _eventAggregator.GetEvent<TaskAddedEvent>().Publish(task);
        }

        public async Task DeleteTaskAsync(TaskEntity id)
        {
            await _taskRepository.Delete(id.Id);
        }

        public async Task<List<TaskEntity>> GetAllTasksAsync()
        {
            List<TaskEntity> allTasksList = new List<TaskEntity>();

            var tasks = await _taskRepository.GetAll();
            allTasksList = tasks.ToList();

            return allTasksList;
        }

        public async Task<TaskEntity> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetById(id);
        }

        public async Task UpdateTaskAsync(TaskDto taskDto)
        {
            try
            {


                TaskEntity task = await _taskRepository.GetById(taskDto.Id);
                if (task is not null)
                {
                    task.Title = taskDto.Title;
                    task.DueDate = taskDto.DueDate;
                    task.PlannedDate = taskDto.PlannedDate;
                    task.TaskType.Name = taskDto.TaskTypeName;
                    task.TaskType.Id = taskDto.TypeId;
                    task.TaskStatus.Name = taskDto.TaskStatusName;
                    task.TaskStatus.Id = taskDto.TaskStatusId;
                    task.Description = taskDto.Description;

                    await _taskRepository.Update(task);
                }
                _eventAggregator.GetEvent<TaskUpdatedEvent>().Publish(task);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception occurred: {ex.Message}");
                throw;
            }
        }
    }
}
