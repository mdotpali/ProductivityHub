using Microsoft.EntityFrameworkCore;
using Prism.DryIoc;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Application.Services;
using ProductivityHub.Domain.Interfaces;
using ProductivityHub.Infrastructure;
using ProductivityHub.Infrastructure.Repositories;
using ProductivityHub.WPF.Modules.TaskModule;
using ProductivityHub.WPF.ViewModels;
using ProductivityHub.WPF.Views;
using System.Windows;

namespace ProductivityHub.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();

            containerRegistry.Register<IFormTypeRepository, FormTypeRepository>();
            containerRegistry.Register<IStatusRepository, StatusRepository>();
            containerRegistry.Register<AppDbContext>(c =>
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase("ProductivityHubDb")
                    .Options;
                return new AppDbContext(options);
            });

            containerRegistry.RegisterSingleton<IEventAggregator, EventAggregator>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<TaskModule>();
        }
    }
}
