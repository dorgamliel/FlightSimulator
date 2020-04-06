using Microsoft.Maps.MapControl.WPF;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        MyControlsViewModel ctrls;
        DashboardViewModel dash;
        MapViewModel mapVM;
        ConnectionViewModel cvm;
        IFlightSimulator fs;
        public MainWindow1()
        {
            InitializeComponent();
            MyTelnetClient client = new MyTelnetClient();
            fs = new MyFlightSimulator(client);
            dash = new DashboardViewModel(fs);
            ctrls = new MyControlsViewModel(fs);
            mapVM = new MapViewModel(fs);
            cvm = new ConnectionViewModel(fs);
            DataContext = dash;
            map.DataContext = mapVM;
            controllers.DataContext = ctrls;
            connect_button.DataContext = cvm;
        }
    }
}
