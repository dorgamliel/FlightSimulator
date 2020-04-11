using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public interface IFlightSimulator : INotifyPropertyChanged
    {
        //connection to simulator.
        void connect(string ip, int port);
        void disconnect();
        void start();
        void StartClient();
        bool MessageInd { get; set; }
        string Message { get; set; }
        bool Connected { get; set; }
        string Heading { get; set; }
        string VerticalSpeed { get; set; }
        string GroundSpeed { get; set; }
        string AirSpeed { get; set; }
        string GPSAlt { get; set; }
        string Roll { get; set; }
        string Pitch { get; set; }
        string AltimeterAlt { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        double Throttle { get; set; }
        double Aileron { get; set; }
        double Rudder { get; set; }
        double Elevator { get; set; }


    }
}
