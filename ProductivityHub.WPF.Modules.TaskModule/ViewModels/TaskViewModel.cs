using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Mvvm;
using ProductivityHub.Application.DTOs;

namespace ProductivityHub.WPF.Modules.TaskModule.ViewModels
{
    public class TaskViewModel : BindableBase, INotifyPropertyChanged
    {
        private TaskDto _taskDto;
        public TaskDto TaskDto => _taskDto;

        public TaskViewModel(TaskDto taskDto)
        {
            _taskDto = taskDto;
        }

        public Guid Id => _taskDto.Id;

        public string Title
        {
            get => _taskDto.Title;
            set
            {
                if (_taskDto.Title != value)
                {
                    _taskDto.Title = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DueDate
        {
            get => _taskDto.DueDate;
            set
            {
                if (_taskDto.DueDate != value)
                {
                    _taskDto.DueDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime PlannedDate
        {
            get => _taskDto.PlannedDate;
            set
            {
                if (_taskDto.PlannedDate != value)
                {
                    _taskDto.PlannedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TaskTypeName
        {
            get => _taskDto.TaskTypeName;
            set
            {
                if (_taskDto.TaskTypeName != value)
                {
                    _taskDto.TaskTypeName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TypeId
        {
            get => _taskDto.TypeId;
            set
            {
                if (_taskDto.TypeId != value)
                {
                    _taskDto.TypeId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TaskStatusName
        {
            get => _taskDto.TaskStatusName;
            set
            {
                if (_taskDto.TaskStatusName != value)
                {
                    _taskDto.TaskStatusName = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TaskStatusId
        {
            get => _taskDto.TaskStatusId;
            set
            {
                if (_taskDto.TaskStatusId != value)
                {
                    _taskDto.TaskStatusId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _taskDto.Description;
            set
            {
                if (_taskDto.Description != value)
                {
                    _taskDto.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
