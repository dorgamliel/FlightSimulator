using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp { 
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
    interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler propertyChanged;
    }
}
