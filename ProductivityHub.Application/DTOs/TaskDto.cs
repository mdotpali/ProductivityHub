using ProductivityHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.Application.DTOs
{
    public class TaskDto
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime PlannedDate { get; set; }
        public string TaskTypeName { get; set; }
        public int TypeId { get; set; }
        public string TaskStatusName { get; set; }
        public int TaskStatusId { get; set; }
        //private Guid _id;
        //private string _title;
        //private string _description;
        //private DateTime _dueDate;
        //private DateTime _plannedDate;
        //private string _taskTypeName;
        //private int _typeId;
        //private string _taskStatusName;
        //private int _taskStatusId;

        //public Guid Id
        //{
        //    get => _id;
        //    set => SetProperty(ref _id, value);
        //}

        //public string Title
        //{
        //    get => _title;
        //    set => SetProperty(ref _title, value);
        //}

        //public string Description
        //{
        //    get => _description;
        //    set => SetProperty(ref _description, value);
        //}

        //public DateTime DueDate
        //{
        //    get => _dueDate;
        //    set => SetProperty(ref _dueDate, value);
        //}

        //public DateTime PlannedDate
        //{
        //    get => _plannedDate;
        //    set => SetProperty(ref _plannedDate, value);
        //}

        //public string TaskTypeName
        //{
        //    get => _taskTypeName;
        //    set => SetProperty(ref _taskTypeName, value);
        //}

        //public int TypeId
        //{
        //    get => _typeId;
        //    set => SetProperty(ref _typeId, value);
        //}

        //public string TaskStatusName
        //{
        //    get => _taskStatusName;
        //    set => SetProperty(ref _taskStatusName, value);
        //}

        //public int TaskStatusId
        //{
        //    get => _taskStatusId;
        //    set => SetProperty(ref _taskStatusId, value);
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        //{
        //    if (Equals(storage, value)) return false;
        //    storage = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}
    }
}
