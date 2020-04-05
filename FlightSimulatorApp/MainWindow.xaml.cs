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
        IFlightSimulator fs;
        public MainWindow1()
        {
            InitializeComponent();
            MyTelnetClient client = new MyTelnetClient();
            fs = new MyFlightSimulator(client);
            dash = new DashboardViewModel(fs);
            ctrls = new MyControlsViewModel(fs);
            mapVM = new MapViewModel(fs);
            DataContext = dash;
            map.DataContext = mapVM;
            controllers.DataContext = ctrls;
        }
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            //If user chose not to use default settings, a dialog will open for entering port and IP.
            if (checkbox.IsChecked == false)
            {
                if ((string)Connect.Content == "Connect")
                {
                    Settings lw = new Settings();
                    lw.ShowDialog();
                    if (lw.IP == null || lw.Port == null)
                        return;
                    fs.connect(lw.IP, Int32.Parse(lw.Port));
                    if (dash.VM_Connected)
                        fs.start();
                }
                else
                { 
                    fs.disconnect();
                    fs.Message = "Disconnected from server.";
                }

            }
            //Using default settings.
            else if ((string)Connect.Content == "Connect")
            {
                int defaultPort = Int32.Parse(ConfigurationManager.AppSettings["port"].ToString());
                string defaultIP = ConfigurationManager.AppSettings["ip"].ToString();
                fs.connect(defaultIP, defaultPort);
                if (dash.VM_Connected)
                    fs.start();
            }
            else
            {
                fs.disconnect();
                fs.Message = "Disconnected from server.";
            }
        }
    }
}
