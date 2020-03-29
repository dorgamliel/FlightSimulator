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
        FlightSimulatorViewModel vm;
        MapViewModel mapVM;
        IFlightSimulator fs;
        public MainWindow1()
        {
            InitializeComponent();
            fs = new MyFlightSimulator();
            fs.Heading = 10;
            fs.Longitude = 22.5;
            fs.Latitude = 20.5;
            vm = new FlightSimulatorViewModel(fs);
            DataContext = vm;
            mapVM = new MapViewModel(fs);
            map.Center = mapVM.VM_Location;
        }
    }
}
