using Microsoft.Maps.MapControl.WPF;
using System;
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
        IFlightSimulator fs;
        public MainWindow1()
        {
            InitializeComponent();
            MyTelnetClient client = new MyTelnetClient();
            client.connect("localhost", 5402);
            fs = new MyFlightSimulator(client);
            dash = new DashboardViewModel(fs);
            ctrls = new MyControlsViewModel(fs);
            mapVM = new MapViewModel(fs);
            DataContext = dash;
            map.DataContext = mapVM;
            fs.start();
            controllers.DataContext = ctrls;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (checkbox.IsChecked == false)
            {
                Settings lw = new Settings();
                lw.ShowDialog();
            }
            else
            {
                if ((String)Connect.Content == "Disconnect")
                    Connect.Content = "Connect";
                else
                    Connect.Content = "Disconnect";
            }
        }
    }
}
