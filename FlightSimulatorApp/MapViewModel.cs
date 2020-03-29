using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class MapViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler propertyChanged;
        private IFlightSimulator simulator;

        public MapViewModel(IFlightSimulator sim)
        {
            this.simulator = sim;
            simulator.propertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public double VM_Latitude
        {
            get { return simulator.Latitude; }
        }
        public double VM_Longitude
        {
            get { return simulator.Longitude; }
        }

        public Tuple<double, double> VM_Location
        {
            get { return new Tuple<double, double>(simulator.Longitude, simulator.Latitude); }
        } 
    }
}
