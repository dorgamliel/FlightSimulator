using Microsoft.Maps.MapControl.WPF;
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
        public event PropertyChangedEventHandler PropertyChanged;
        private IFlightSimulator simulator;

        public MapViewModel(IFlightSimulator sim)
        {
            this.simulator = sim;
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

        public string VM_Latitude
        {
            get { return simulator.Latitude; }
        }
        public string VM_Longitude
        {
            get { return simulator.Longitude; }
        }

        public Location VM_Location
        {
            get
            {
                try
                {
                    return new Location(Double.Parse(simulator.Latitude), Double.Parse(simulator.Longitude));
                } catch (Exception)
                {
                    return new Location(0, 0);
                }
            }
        }
    }
}
