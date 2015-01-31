using System;
using System.Windows;

using DDDTalk.Presentation;

namespace DDDTalk.Application
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
        }
    }
}
