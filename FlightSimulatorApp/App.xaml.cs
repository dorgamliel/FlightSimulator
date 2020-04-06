using FlightSimulatorApp.View_Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IFlightSimulator fs;
        public MainViewModel VM { get; internal set; }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MyTelnetClient client = new MyTelnetClient();
            fs = new MyFlightSimulator(client);
            MainWindow wnd = new MainWindow();
            VM = new MainViewModel(fs);
            wnd.DataContext = VM;
            wnd.dash.DataContext = new DashboardViewModel(fs);
            wnd.map.DataContext = new MapViewModel(fs);
            wnd.controllers.DataContext = new MyControlsViewModel(fs);
            wnd.Show();
        }

    }
}
