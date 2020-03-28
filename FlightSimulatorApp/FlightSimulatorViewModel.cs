using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class FlightSimulatorViewModel : INotifyPropertyChanged
    {
        private IFlightSimulator simulator;
        public FlightSimulatorViewModel(IFlightSimulator fs)
        {
            this.simulator = fs;
            simulator.propertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            
        }
        public event PropertyChangedEventHandler propertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public double VM_throttle {
            get { return simulator.throttle; }
            set
            {
                simulator.throttle = value;
                Console.WriteLine(simulator.throttle);
            }
        }
        public double VM_aileron { get { return simulator.aileron; } }
        public double VM_elevator { get { return simulator.elevator; } }
        public double VM_rudder { get { return simulator.rudder; } }
        public double VM_latitude { get { return simulator.latitude; } }
        public double VM_longitude { get { return simulator.longitude; } }
         public double VM_airSpeed { get { return simulator.airSpeed; } }
         public double VM_altitude { get { return simulator.altitude; } }
        public double VM_roll { get { return simulator.roll; } }
        public double VM_pitch { get { return simulator.pitch; } }
        public double VM_altimeter { get { return simulator.altimeter; } }
        public double VM_heading { get { return simulator.heading; } }
        public double VM_groundSpeed { get { return simulator.groundSpeed; } }
        public double VM_verticalSpeed { get { return simulator.verticalSpeed; } }
       
    }
}
