using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface IFlightSimulator : INotifyPropertyChanged
    {
        //connection to simulator.
        void connect(string ip, int port);
        void disconnect();
        void start();
        void setThrottle(double val);
        void setAileron(double val);
        void setRudder(double val);
        void setElevator(double val);
        double heading { get; set; }
        double verticalSpeed { get; set; }
        double groundSpeed { get; set; }
        double airSpeed { get; set; }
        double GPSAlt { get; set; }
        double roll { get; set; }
        double pitch { get; set; }
        double AltimeterAlt { get; set; }
        double latitude { get; set; }
        double longitude { get; set; }


    }
}
