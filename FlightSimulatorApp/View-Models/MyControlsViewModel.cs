﻿using System;
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
        public bool VM_MessageInd
        {
            get { return simulator.MessageInd; }
        }
    }
}
