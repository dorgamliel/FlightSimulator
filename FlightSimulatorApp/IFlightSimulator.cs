using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        double Heading { get; set; }
        double VerticalSpeed { get; set; }
        double GroundSpeed { get; set; }
        double AirSpeed { get; set; }
        double GPSAlt { get; set; }
        double Roll { get; set; }
        double Pitch { get; set; }
        double AltimeterAlt { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        double Throttle { get; set; }
        double Aileron { get; set; }
        double Rudder { get; set; }
        double Elevator { get; set; }


    }
}
