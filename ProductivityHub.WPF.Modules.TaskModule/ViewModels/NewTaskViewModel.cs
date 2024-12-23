using Accessibility;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductivityHub.WPF.Modules.TaskModule.ViewModels
{
    public class NewTaskViewModel : BindableBase, INavigationAware
    {
        private readonly ITaskService _taskService;

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PlannedDate { get; set; }
        public string TaskTypeName { get; set; }
        public string TaskStatusName { get; set; }
        

        public NewTaskViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        private DelegateCommand _saveNewTaskCommand;
        public DelegateCommand SaveNewTaskCommand =>
            _saveNewTaskCommand ?? (_saveNewTaskCommand = new DelegateCommand(ExecuteSaveNewTaskCommand));

        void ExecuteSaveNewTaskCommand()
        {
            var taskDto = new TaskDto
            {
                Title = Title,
                Description = Description,
                DueDate = DueDate  ?? DateTime.Now,
                PlannedDate = PlannedDate ?? DateTime.Now,
                TaskStatusName = TaskStatusName,
                TaskTypeName = TaskTypeName,
                Id = new Guid()
            };
            _taskService.AddTaskAsync(taskDto);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
