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
                NotifyPropertyChanged("VM" + e.PropertyName);
            };

        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string VMHeading
        {
            get { return simulator.Heading; }
        }
        public string VMVerticalSpeed
        {
            get { return simulator.VerticalSpeed; }
        }
        public string VMGroundSpeed
        {
            get { return simulator.GroundSpeed; }
        }
        public string VMAirSpeed
        {
            get { return simulator.AirSpeed; }
        }
        public string VMGPSAlt
        {
            get { return simulator.GPSAlt; }
        }
        public string VMRoll
        {
            get { return simulator.Roll; }
        }
        public string VMPitch
        {
            get { return simulator.Pitch; }
        }
        public string VMAltimeterAlt
        {
            get { return simulator.AltimeterAlt; }
        }
        public bool VMMessageInd
        {
            get { return simulator.MessageInd; }
        }
        public string VMMessage
        {
            get { return simulator.Message; }
        }
    }
}
