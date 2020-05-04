using NGU_Helper.Scenarios.MainWindow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NGU_Helper
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly MainWindowPresenter _mainPresenter;

        public App()
        {
            _mainPresenter = new MainWindowPresenter();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _mainPresenter.StartApplication();
        }
    }
}
