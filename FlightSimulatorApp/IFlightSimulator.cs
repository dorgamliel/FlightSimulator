﻿using System;
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

    }
}
