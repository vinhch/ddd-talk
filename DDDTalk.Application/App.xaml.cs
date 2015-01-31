using System;
using System.Windows;

using DDDTalk.Application.Boostrapping;

namespace DDDTalk.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var applicationBootstrapper = new ApplicationBootstrapper();
            applicationBootstrapper.Initialize();

            var window = applicationBootstrapper.CreateMainWindow();
            window.Show();
        }
    }
}