﻿using Prism.DryIoc;
using Prism.Ioc;
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

        }
    }
}
