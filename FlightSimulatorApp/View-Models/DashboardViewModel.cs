using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        private IFlightSimulator simulator;

        public event PropertyChangedEventHandler PropertyChanged;

        public DashboardViewModel(IFlightSimulator fs)
        {
            this.simulator = fs;
            simulator.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double VM_heading
        {
            get { return simulator.Heading; }
        }
        public double VM_verticalSpeed
        {
            get { return simulator.VerticalSpeed; }
        }
        public double VM_groundSpeed
        {
            get { return simulator.GroundSpeed; }
        }
        public double VM_airSpeed
        {
            get { return simulator.AirSpeed; }
        }
        public double VM_GPSAlt
        {
            get { return simulator.GPSAlt; }
        }
        public double VM_roll
        {
            get { return simulator.Roll; }
        }
        public double VM_pitch
        {
            get { return simulator.Pitch; }
        }
        public double VM_AltimeterAlt
        {
            get { return simulator.AltimeterAlt; }
        }
        public double VM_latitude
        {
            get { return simulator.Latitude; }
        }
        public double VM_longitude
        {
            get { return simulator.Longitude; }
        }
    }
}
