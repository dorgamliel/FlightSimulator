using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface IFlightSimulator
    {
        //connection to simulator.
        void connect(string ip, int port);
        void disconnect();
        void start();
        //plane data properties.
        double throttle { set; get; }

    }
}
