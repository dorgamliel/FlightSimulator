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
                NotifyPropertyChanged("VM" + e.PropertyName);
            };
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string VMLatitude
        {
            get { return simulator.Latitude; }
        }
        public string VMLongitude
        {
            get { return simulator.Longitude; }
        }

        public Location VMLocation
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
