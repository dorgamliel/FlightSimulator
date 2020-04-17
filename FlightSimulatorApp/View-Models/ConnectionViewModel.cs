using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class ConnectionViewModel : INotifyPropertyChanged
    {
        private IFlightSimulator simulator;

        public event PropertyChangedEventHandler PropertyChanged;

        public ConnectionViewModel(IFlightSimulator fs)
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
        public void VMConnect(string ip, int port)
        {
            simulator.Connect(ip, port);
            if (VMConnected)
                simulator.Start();
        }
        public void VMDisconnect()
        {
            simulator.Disconnect();
        }
        public bool VMConnected
        {
            get { return simulator.Connected; }
        }

    }
}
