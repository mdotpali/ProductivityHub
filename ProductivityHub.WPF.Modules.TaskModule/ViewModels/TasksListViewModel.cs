using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Events.TaskEvents;
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

        public ObservableCollection<TaskViewModel> TasksList { get; private set; }
        public TasksListViewModel(ITaskService taskSrvice, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _taskService = taskSrvice;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<TaskAddedEvent>().Subscribe(OnTaskAdded);
            _eventAggregator.GetEvent<TaskUpdatedEvent>().Subscribe(OnTaskUpdated);
            _eventAggregator.GetEvent<TaskDeletedEvent>().Subscribe(OnTaskDeleted);

            TasksList = new ObservableCollection<TaskViewModel>();
        }

        private void OnTaskUpdated(TaskEntity taskEntity)
        {

            var taskViewModel = TasksList.FirstOrDefault(t => t.Id == taskEntity.Id);
            if (taskViewModel != null)
            {
                // Update properties of the existing TaskDto
                taskViewModel.Title = taskEntity.Title;
                taskViewModel.DueDate = taskEntity.DueDate;
                taskViewModel.PlannedDate = taskEntity.PlannedDate;
                taskViewModel.TaskTypeName = taskEntity.TaskType.Name;
                taskViewModel.TypeId = taskEntity.TaskType.Id;
                taskViewModel.TaskStatusName = taskEntity.TaskStatus.Name;
                taskViewModel.TaskStatusId = taskEntity.TaskStatus.Id;
                taskViewModel.Description = taskEntity.Description;                
            }
        }
        private void OnTaskDeleted(Guid taskId)
        {

            var taskViewModel = TasksList.FirstOrDefault(t => t.Id == taskId);
            TasksList.Remove(taskViewModel);
        }

        private DelegateCommand<TaskViewModel> _itemClickCommand;
        public DelegateCommand<TaskViewModel> ItemClickCommand =>
            _itemClickCommand ?? (_itemClickCommand = new DelegateCommand<TaskViewModel>(ExecuteItemClickCommand));

        void ExecuteItemClickCommand(TaskViewModel task)
        {
            var taskDto = new TaskDto
            {
                // Map properties from TaskEntity to TaskDto
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                PlannedDate = task.PlannedDate,
                TaskTypeName = task.TaskTypeName,
                TaskStatusName = task.TaskStatusName,
                Description = task.Description,
                TypeId = task.TypeId,
                TaskStatusId = task.TaskStatusId
            };
            var parameters = new NavigationParameters()
            {
                { "selectedTask", taskDto }
            };
            _regionManager.RequestNavigate("ContentRegion", nameof(SelectedTask), parameters);
        }
        private void OnTaskAdded(TaskEntity task)
        {
            var taskDto = new TaskDto
            {
                // Map properties from TaskEntity to TaskDto
                Id = task.Id,
                Title = task.Title,
                DueDate = task.DueDate,
                PlannedDate = task.PlannedDate,
                TaskTypeName = task.TaskType.Name,
                TypeId = task.TaskType.Id,
                TaskStatusName = task.TaskStatus.Name,
                TaskStatusId = task.TaskStatus.Id,
                Description = task.Description
            };
            var taskViewModel = new TaskViewModel(taskDto);
            TasksList.Add(taskViewModel);
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
                    var taskDto = new TaskDto
                    {
                        // Map properties from TaskEntity to TaskDto
                        Id = task.Id,
                        Title = task.Title,
                        DueDate = task.DueDate,
                        PlannedDate = task.PlannedDate,
                        TaskTypeName = task.TaskType.Name,
                        TypeId = task.TaskType.Id,
                        TaskStatusName= task.TaskStatus.Name,
                        TaskStatusId = task.TaskStatus.Id,
                        Description = task.Description
                    };
                    var taskViewModel = new TaskViewModel(taskDto);
                    TasksList.Add(taskViewModel);
                }
            }
        }

        private DelegateCommand _newTaskCommand;
        public DelegateCommand NewTaskCommand =>
            _newTaskCommand ?? (_newTaskCommand = new DelegateCommand(ExecuteNewTaskCommand));

        void ExecuteNewTaskCommand()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(NewTask));
        }


    }
}
