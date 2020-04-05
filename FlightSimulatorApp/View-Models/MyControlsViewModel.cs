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
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double VM_throttle
        {
            set
            {
                simulator.setProp(value, "throttle");
            }
        }
        public double VM_aileron
        {
            set
            {
                simulator.setProp(value, "aileron");
            }
        }
        public double VM_rudder
        {
            set
            {
                simulator.setProp(value, "rudder");
            }
        }
        public double VM_elevator
        {
            set
            {
                simulator.setProp(value, "elevator");
            }
        }
        public bool VM_MessageInd
        {
            get { return simulator.MessageInd; }
        }
    }
}
