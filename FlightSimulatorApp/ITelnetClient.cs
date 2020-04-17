using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface ITelnetClient
    {
        //Connection to simulator.
        void Connect(string ip, int port);
        //Disconnection from simulator.
        void Disconnect();
        //Writing to simulator.
        void Write(string command);
        //Reading from simulator.
        string Read();
    }
}
