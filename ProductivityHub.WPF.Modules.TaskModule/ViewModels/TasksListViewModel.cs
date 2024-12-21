using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Events;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Domain.Entities;
using ProductivityHub.WPF.Modules.TaskModule.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.WPF.Modules.TaskModule.ViewModels
{
    public class TasksListViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private ITaskService _taskService;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<TaskEntity> TasksList { get; private set; }
        public TasksListViewModel(ITaskService taskSrvice, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _taskService = taskSrvice;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<TaskAddedEvent>().Subscribe(OnTaskAdded);
            TasksList = new ObservableCollection<TaskEntity>();
        }

        private void OnTaskAdded(TaskEntity entity)
        {
            TasksList.Add(entity);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public  void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!TasksList.Any())
            {
                var tasks = await _taskService.GetAllTasksAsync();
                foreach (var task in tasks)
                {
                    TasksList.Add(task);
                }
            }
        }

        private DelegateCommand _NewTaskCommand;
        public DelegateCommand NewTaskCommand =>
            _NewTaskCommand ?? (_NewTaskCommand = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            _regionManager.AddToRegion("ContentRegion", nameof(NewTask));
        }

        private DelegateCommand _taskTestCommand;
        public DelegateCommand TaskTestCommand =>
            _taskTestCommand ?? (_taskTestCommand = new DelegateCommand(ExecuteTaskTestCommand));

        void ExecuteTaskTestCommand()
        {
            var taskDto = new TaskDto
            {
                Title = "Example Task",
                Description = "Example Description",
                DueDate = DateTime.Now,
                PlanedDate = DateTime.Now,
                TaskStatusId = 1,
                TaskStatusName = "Started",
                TaskTypeName = "Project Task",
                TypeId = 1,
                Id = new Guid()
            };
            _taskService.AddTaskAsync(taskDto);

        }
    }
}
