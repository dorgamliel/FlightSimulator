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
        public string VM_Heading
        {
            get { return simulator.Heading; }
        }
        public string VM_VerticalSpeed
        {
            get { return simulator.VerticalSpeed; }
        }
        public string VM_GroundSpeed
        {
            get { return simulator.GroundSpeed; }
        }
        public string VM_AirSpeed
        {
            get { return simulator.AirSpeed; }
        }
        public string VM_GPSAlt
        {
            get { return simulator.GPSAlt; }
        }
        public string VM_Roll
        {
            get { return simulator.Roll; }
        }
        public string VM_Pitch
        {
            get { return simulator.Pitch; }
        }
        public string VM_AltimeterAlt
        {
            get { return simulator.AltimeterAlt; }
        }
        public bool VM_MessageInd
        {
            get { return simulator.MessageInd; }
        }
        public string VM_Message
        {
            get { return simulator.Message; }
        }
    }
}
