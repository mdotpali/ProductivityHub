using ProductivityHub.WPF.Modules.TaskModule.Views;
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
            containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion("ContentRegion", typeof(TaskListManager));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<TaskListManager, TaskListManagerViewModel>();

            containerRegistry.Register<ITaskService, TaskService>();
            containerRegistry.Register<ITaskRepository, TaskRepository>();
        }
    }
}