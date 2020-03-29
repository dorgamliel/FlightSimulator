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
        public double VM_heading
        {
            get { return simulator.heading; }
        }
        public double VM_verticalSpeed {
            get { return simulator.verticalSpeed; }
        }
        public double VM_groundSpeed
        {
            get { return simulator.groundSpeed; }
        }
        public double VM_airSpeed
        {
            get { return simulator.airSpeed; }
        }
        public double VM_GPSAlt
        {
            get { return simulator.GPSAlt; }
        }
        public double VM_roll
        {
            get { return simulator.roll; }
        }
        public double VM_pitch
        {
            get { return simulator.pitch; }
        }
        public double VM_AltimeterAlt
        {
            get { return simulator.AltimeterAlt; }
        }
        public double VM_latitude
        {
            get { return simulator.latitude; }
        }
        public double VM_longitude
        {
            get { return simulator.longitude; }
        }
        public double VM_throttle
        {
            set {
                simulator.setThrottle(value);
            }
        }
        public double VM_aileron
        {
            set
            {
                simulator.setAileron(value);
            }
        }
        public double VM_rudder
        {
            set
            {
                simulator.setRudder(value);
            }
        }
        public double VM_elevator
        {
            set
            {
                simulator.setElevator(value);
            }
        }

/*
        public void VM_setThrottle(double val)
        {
            simulator.setThrottle(val);
        }
        public void VM_setAileron(double val)
        {
            simulator.setAileron(val);
        }
        public void VM_setRudder(double val)
        {
            simulator.setRudder(val);
        }
        public void VM_setElevator(double val)
        {
            simulator.setElevator(val);
        }*/

    }
}
