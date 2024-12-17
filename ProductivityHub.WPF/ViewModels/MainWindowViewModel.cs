using AvalonDock.Themes;
using Prism.Commands;
using Prism.Mvvm;
using ProductivityHub.Application.DTOs;
using ProductivityHub.Application.Interfaces;
using ProductivityHub.Domain.Entities;
using ProductivityHub.WPF.Core.Interfaces;
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

        private Theme _currentAvalonTheme;
        public Theme CurrentAvalonTheme
        {
            get { return _currentAvalonTheme; }
            set { SetProperty(ref _currentAvalonTheme, value); }
        }
        private DelegateCommand _setLightThemeCommand;
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

        public MainWindowViewModel(IThemeService themService)
        {
            _themeService = themService;
            _themeService.ThemeChanged += OnThemeChanged;
        }

        private void OnThemeChanged()
        {
            CurrentAvalonTheme = _themeService.CurrentAvalonTheme;
        }

    }
}