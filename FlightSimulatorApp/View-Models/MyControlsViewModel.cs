using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class MyControlsViewModel : INotifyPropertyChanged
    {
        private IFlightSimulator simulator;
        public event PropertyChangedEventHandler PropertyChanged;
        public MyControlsViewModel(IFlightSimulator fs)
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
        public double VMThrottle
        {
            set
            {
                simulator.Throttle = value;
            }
        }
        public double VMAileron
        {
            set
            {
                simulator.Aileron = value;
            }
        }
        public double VMRudder
        {
            set
            {
                simulator.Rudder = value;
            }
        }
        public double VMElevator
        {
            set
            {
                simulator.Elevator = value;
            }
        }
        public bool VMMessageInd
        {
            get { return simulator.MessageInd; }
        }
    }
}
