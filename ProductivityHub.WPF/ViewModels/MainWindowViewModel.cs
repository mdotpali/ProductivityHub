using Prism.Commands;
using Prism.Mvvm;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Domain.Entities;
using System;

namespace ProductivityHub.WPF.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        //private readonly ITaskService _taskService;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {
            //_taskService = taskService;
        }

        //private DelegateCommand _taskTestCommand;
        //public DelegateCommand TaskTestCommand =>
        //    _taskTestCommand ?? (_taskTestCommand = new DelegateCommand(ExecuteTaskTestCommand));

        //void ExecuteTaskTestCommand()
        //{
        //    var taskDto = new TaskDto
        //    {
        //        Title = "Example Task",
        //        Description = "Example Description",
        //        DueDate = DateTime.Now,
        //        PlanedDate = DateTime.Now,
        //        TaskStatusId = 1,
        //        TaskStatusName = "Started",
        //        TaskTypeName = "Project Task",
        //        TypeId = 1,
        //        Id = new Guid()
        //    };
        //    _taskService.AddTaskAsync(taskDto);
        //}
    }
}
