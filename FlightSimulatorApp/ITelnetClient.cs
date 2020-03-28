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
        void connect(string ip, int port);
        void disconnect();
        void write(string command);
        string read();
    }
}
