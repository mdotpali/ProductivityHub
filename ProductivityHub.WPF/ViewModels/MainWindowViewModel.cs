using AvalonDock.Themes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Domain.Entities;
using ProductivityHub.WPF.Core.Interfaces;
using ProductivityHub.WPF.Modules.TaskModule.Views;
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

        private DelegateCommand _OpenTasksCommand;
        public DelegateCommand OpenTasksCommand =>
            _OpenTasksCommand ?? (_OpenTasksCommand = new DelegateCommand(ExecuteOpenTasksCommand));

        void ExecuteOpenTasksCommand()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(TasksList));
        }

        private Theme _currentAvalonTheme ;
        public Theme CurrentAvalonTheme
        {
            get { return _currentAvalonTheme; }
            set { SetProperty(ref _currentAvalonTheme, value); }
        }
        private DelegateCommand _setLightThemeCommand;
        private readonly IRegionManager _regionManager;
        private readonly IThemeService _themeService;

        public DelegateCommand SetLightThemeCommand =>
            _setLightThemeCommand ?? (_setLightThemeCommand = new DelegateCommand(ExecuteSetLightThemeCommand));

        void ExecuteSetLightThemeCommand()
        {
            _themeService.SetLightTheme();
        }

        private DelegateCommand _setDarkThemeCommand;
        public DelegateCommand SetDarkThemeCommand =>
            _setDarkThemeCommand ?? (_setDarkThemeCommand = new DelegateCommand(ExecuteSetDarkThemeCommand));

        void ExecuteSetDarkThemeCommand()
        {
            _themeService.SetDarkTheme();
        }

        public MainWindowViewModel(IThemeService themService, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _themeService = themService;
            _themeService.ThemeChanged += OnThemeChanged;
            _themeService.SetDarkTheme();
        }

        private void OnThemeChanged()
        {
            CurrentAvalonTheme = _themeService.CurrentAvalonTheme;
        }

    }
}