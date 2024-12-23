﻿using ProductivityHub.WPF.Modules.TaskModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Application.Services;
using Prism.Mvvm;
using ProductivityHub.WPF.Modules.TaskModule.ViewModels;
using ProductivityHub.Infrastructure.Repositories;
using ProductivityHub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProductivityHub.Infrastructure;

namespace ProductivityHub.WPF.Modules.TaskModule
{
    public class TaskModule: IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion("ToolRegion", typeof(TasksList));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<TasksList, TasksListViewModel>();
            ViewModelLocationProvider.Register<NewTask, NewTaskViewModel>();

            containerRegistry.RegisterSingleton<ITaskService, TaskService>();
            containerRegistry.RegisterSingleton<ITaskRepository, TaskRepository>();

            containerRegistry.RegisterForNavigation<TasksList>();
            containerRegistry.RegisterForNavigation<NewTask>();
            containerRegistry.RegisterForNavigation<SelectedTask>();

        }
    }
}