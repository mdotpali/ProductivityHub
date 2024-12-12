﻿using Prism.Commands;
using Prism.Mvvm;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityHub.WPF.Modules.TaskModule.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private readonly ITaskService _taskService;

        public ViewAViewModel(ITaskService taskService)
        {
            Message = "View A from your Prism Module";
            _taskService = taskService;
        }

       
    }
}
