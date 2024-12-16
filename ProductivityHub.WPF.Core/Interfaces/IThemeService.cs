using AvalonDock.Themes;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductivityHub.WPF.Core.Interfaces
{
    public interface IThemeService
    {
        AvalonDock.Themes.Theme CurrentAvalonTheme { get; }
        BaseTheme CurrentMaterialTheme { get; }
        event Action ThemeChanged;
        void SetLightTheme();
        void SetDarkTheme();

    }
}
