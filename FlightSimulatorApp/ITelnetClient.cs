using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface ITelnetClient
    {
        //connection to simulator.
        void Connect(string ip, int port);
        void Disconnect();
        void Write(string command);
        string Read();
    }
}
