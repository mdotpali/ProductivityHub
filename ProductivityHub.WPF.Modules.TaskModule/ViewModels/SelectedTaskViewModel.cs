﻿using Microsoft.VisualBasic;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ProductivityHub.WPF.Modules.TaskModule.ViewModels
{
    public class SelectedTaskViewModel : BindableBase, INavigationAware
    {
        private readonly ITaskService _taskService;
        private readonly IRegionManager _regionManager;
        private TaskDto _originalTask;

        public SelectedTaskViewModel(ITaskService taskService, IRegionManager regionManager)
        {
            _taskService = taskService;
            _regionManager = regionManager;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate; }
            set { SetProperty(ref _dueDate, value); }
        }

        private DateTime _plannedDate;
        public DateTime PlannedDate
        {
            get { return _plannedDate; }
            set { SetProperty(ref _plannedDate, value); }
        }

        private string _taskTypeName;
        public string TaskTypeName
        {
            get { return _taskTypeName; }
            set { SetProperty(ref _taskTypeName, value); }
        }

        private string _taskStatusName;
        public string TaskStatusName
        {
            get { return _taskStatusName; }
            set { SetProperty(ref _taskStatusName, value); }
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
            if (navigationContext.Parameters["selectedTask"] is TaskDto task)
            {
                _originalTask = task;
                Title = task.Title;
                Description = task.Description;
                DueDate = task.DueDate;
                PlannedDate = task.PlannedDate;
                TaskTypeName = task.TaskTypeName;
                TaskStatusName = task.TaskStatusName;
                DocumentName = task.Title; // Set the DocumentName property based on the selected task's title
            }
        }

        private string _documentName;
        public string DocumentName
        {
            get { return _documentName; }
            set { SetProperty(ref _documentName, value); }
        }

        private DelegateCommand _saveUpdatedTaskCommand;
        public DelegateCommand SaveUpdatedTaskCommand =>
            _saveUpdatedTaskCommand ?? (_saveUpdatedTaskCommand = new DelegateCommand(ExecuteSaveUpdatedTaskCommand));

        void ExecuteSaveUpdatedTaskCommand()
        {
            _originalTask.Title = Title;
            _originalTask.Description = Description;
            _originalTask.DueDate = DueDate;
            _originalTask.PlannedDate = PlannedDate;
            _originalTask.TaskTypeName = TaskTypeName;
            _originalTask.TaskStatusName = TaskStatusName;

            _taskService.UpdateTaskAsync(_originalTask);
        }

        private DelegateCommand _deleteTask;
        public DelegateCommand DeleteTask =>
            _deleteTask ?? (_deleteTask = new DelegateCommand(ExecuteDeleteTask));

        void ExecuteDeleteTask()
        {
            _taskService.DeleteTaskAsync(_originalTask.Id);

            // Remove the view from the region
            var view = _regionManager.Regions["ContentRegion"].ActiveViews.FirstOrDefault(v => ((FrameworkElement)v).DataContext == this);
            if (view != null)
            {
                _regionManager.Regions["ContentRegion"].Remove(view);
            }
        }
    }
}
