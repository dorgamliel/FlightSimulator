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
        FlightSimulatorViewModel vm;
        MapViewModel mapVM;
        IFlightSimulator fs;
        public MainWindow1()
        {
            InitializeComponent();
            MyTelnetClient client = new MyTelnetClient();
            client.connect("localhost", 5402);
            fs = new MyFlightSimulator(client);
            vm = new FlightSimulatorViewModel(fs);
            DataContext = vm;
            mapVM = new MapViewModel(fs);
            map.Center = mapVM.VM_Location;
            ctrls = new MyControlsViewModel(fs);
            ctrls.VM_rudder = controllers.JoyStick.knobPosition.X / (controllers.JoyStick.innerCircle.ActualHeight / 2);
            ctrls.VM_elevator = controllers.JoyStick.knobPosition.Y / (controllers.JoyStick.innerCircle.ActualWidth / 2);
        }
    }
}
