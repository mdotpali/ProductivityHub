using AvalonDock.Themes;
using MaterialDesignThemes.Wpf;
using Prism.Mvvm;
using ProductivityHub.WPF.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductivityHub.WPF.Core.Services
{
    public class ThemeService :BindableBase, IThemeService
    {
        private AvalonDock.Themes.Theme _currentAvalonTheme;
        public AvalonDock.Themes.Theme CurrentAvalonTheme
        {
            get { return _currentAvalonTheme; }
            set { SetProperty(ref _currentAvalonTheme, value); }
        }

        private BaseTheme _currentMaterialTheme;
        public BaseTheme CurrentMaterialTheme
        {
            get { return _currentMaterialTheme; }
            set { SetProperty(ref _currentMaterialTheme, value); }
        }

        public event Action ThemeChanged;

        public void SetDarkTheme()
        {
            CurrentAvalonTheme = new Vs2013DarkTheme();
            CurrentMaterialTheme = BaseTheme.Dark;
            ApplyMaterialTheme();
            ThemeChanged?.Invoke();
        }

        private void ApplyMaterialTheme()
        {
            var palatteHelper = new PaletteHelper();
            var theme = palatteHelper.GetTheme();
            theme.SetBaseTheme(CurrentMaterialTheme);
            palatteHelper.SetTheme(theme);
        }
        public void SetLightTheme()
        {
            CurrentAvalonTheme = new Vs2013LightTheme();
            CurrentMaterialTheme = BaseTheme.Light;
            ApplyMaterialTheme();
            ThemeChanged?.Invoke();
        }
    }
}
