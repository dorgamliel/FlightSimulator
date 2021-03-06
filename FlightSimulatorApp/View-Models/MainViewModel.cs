﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.View_Models
{
    public class MainViewModel : INotifyPropertyChanged
    {

        private IFlightSimulator simulator;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel(IFlightSimulator fs)
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

        public bool VMMessageInd
        {
            get { return simulator.MessageInd; }
        }
        public string VMMessage
        {
            get { return simulator.Message; }
        }
        public bool VMConnected
        {
            get { return simulator.Connected; }
        }
    }
}
